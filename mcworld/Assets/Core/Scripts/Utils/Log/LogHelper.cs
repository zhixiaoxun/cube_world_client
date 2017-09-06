using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Globalization;

namespace Core.Utils.Log
{
    public class LogHelper
    {
        public enum Level
        {
            None = 0,
            Fatal,
            Error,
            Warn,
            Info,
            Debug
        }

        private static bool m_enable = true;
        private static Level m_maxLevelToUnity = Level.Info;
        private static Level m_maxLevelToFile = Level.Debug;
        private static bool m_enableContextFilters = true;
        private static LogTrie m_contextFilters = new LogTrie();
        private static string m_filePath = "";
        private static StreamWriter m_fileWriter = null;
        private static bool m_fileBufferMode = true;

        // 全局开关
        public static bool Enable
        {
            get { return m_enable; }
            set { m_enable = value; }
        }

        // 输出到Unity控制台的Log级别
        public static Level OutputLevelUnity
        {
            get { return m_maxLevelToUnity; }
            set { m_maxLevelToUnity = value; }
        }

        // 输出到Log文件的Log级别
        public static Level OutputLevelFile
        {
            get { return m_maxLevelToFile; }
            set { m_maxLevelToFile = value; }
        }

        // 控制输出到文件时是否缓存
        public static bool FileBufferMode
        {
            get { return m_fileBufferMode; }
            set { m_fileBufferMode = value; }
        }

        // 输出一条Log
        public static bool Log(Level level, string context, string format, params Object[] values)
        {
            if (!m_enable)
                return false;
            if ((level > m_maxLevelToUnity) && (level > m_maxLevelToFile))
                return false;

            // Info/Debug级别的Log需要检查Context是否被过滤
            if (level > Level.Warn && !__CheckContext(context))
                return false;

            StringBuilder sb = new StringBuilder();
            sb.Append(__TimeStr());
            sb.Append(__LevelToString(level));
            sb.Append("[");
            sb.Append(context);
            sb.Append("] ");
            if (values.Length == 0)
            {
                sb.Append(format);
            }
            else
            {
                sb.AppendFormat(format, values);
            }

            string line = sb.ToString();

            if (level <= m_maxLevelToFile)
            {
                __WriteToFile(line);
            }
            if (level <= m_maxLevelToUnity)
            {
                if (values.Length > 0 && values[values.Length - 1] is UnityEngine.Object)
                    __UnityDebugLog(level, line, values[values.Length - 1] as UnityEngine.Object);
                else
                    __UnityDebugLog(level, line, null);
            }

            return true;
        }

        public static bool DEBUG(string context, string format, params Object[] values)
        {
            return Log(Level.Debug, context, format, values);
        }
        public static bool INFO(string context, string format, params Object[] values)
        {
            return Log(Level.Info, context, format, values);
        }
        public static bool WARN(string context, string format, params Object[] values)
        {
            return Log(Level.Warn, context, format, values);
        }
        public static bool ERROR(string context, string format, params Object[] values)
        {
            return Log(Level.Error, context, format, values);
        }
        public static bool FATAL(string context, string format, params Object[] values)
        {
            return Log(Level.Fatal, context, format, values);
        }

        // 测试某级别的Log会不会有实际输出
        public static bool Test(Level level)
        {
            return m_enable &&
                ((m_maxLevelToFile >= level) || (m_maxLevelToUnity >= level));
        }

        public static bool TestDEBUG()
        {
            return Test(Level.Debug);
        }
        public static bool TestINFO()
        {
            return Test(Level.Info);
        }
        public static bool TestWARN()
        {
            return Test(Level.Warn);
        }
        public static bool TestERROR()
        {
            return Test(Level.Error);
        }
        public static bool TestFATAL()
        {
            return Test(Level.Fatal);
        }

        public static void FlushFile()
        {
            if (m_fileWriter != null)
            {
                m_fileWriter.Flush();
            }
        }

        public static void CleanupOldFiles(int keepDays)
        {
            __cleanupOldFiles(__logDir(), keepDays);
        }

        // 控制是否开启ContextFilters
        public static bool EnableContextFilters
        {
            get { return m_enableContextFilters; }
            set { m_enableContextFilters = value; }
        }

        // 增加一条Context过滤规则
        public static bool AddContextFilter(string path)
        {
            return m_contextFilters.AddPath(path);
        }

        // 移除一条Context过滤规则
        public static bool RemoveContextFilter(string path)
        {
            return m_contextFilters.RemovePath(path);
        }

        // 返回当前的Context过滤列表
        public static List<string> ContextFilters()
        {
            return m_contextFilters.PathList();
        }

        #region "Internal helper functions"
        private static void __UnityDebugLog(Level level, string msg, UnityEngine.Object obj)
        {
            switch (level)
            {
                case Level.Fatal:
                case Level.Error:
                    UnityEngine.Debug.LogError(msg, obj);
                    break;
                case Level.Warn:
                    UnityEngine.Debug.LogWarning(msg, obj);
                    break;
                default:
                    UnityEngine.Debug.Log(msg, obj);
                    break;
            }
        }

        private static string __TimeStr()
        {
            //return "2015/04/28_11:04:54,984 ";
            return DateTime.Now.ToString("yyyy/MM/dd_HH:mm:ss,fff ");
        }

        private static string __LevelToString(Level level)
        {
            switch (level)
            {
                case Level.Fatal:
                    return "FATAL ";
                case Level.Error:
                    return "ERROR ";
                case Level.Warn:
                    return "WARN  ";
                case Level.Info:
                    return "INFO  ";
                case Level.Debug:
                    return "DEBUG ";
                default:
                    return "_____ ";
            }
        }

        private static void __WriteToFile(string line)
        {
            if (m_filePath.Length == 0)
            {
                m_filePath = Path.Combine(__logDir(), __logFileName());
                try
                {
                    m_fileWriter = new StreamWriter(m_filePath);
                    m_fileWriter.WriteLine("[[TIME,LEVEL,LOGGER]]");  // 第一行输出kgsl列定义
                    m_fileWriter.Flush();
                }
                catch (Exception e)
                {
                    m_fileWriter = null;
                    UnityEngine.Debug.Log("Can't create logFile, error=" + e.ToString());
                }
            }

            if (m_fileWriter != null)
            {
                try
                {
                    m_fileWriter.WriteLine(line);
                    if (!m_fileBufferMode)
                    {
                        m_fileWriter.Flush();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private static bool __CheckContext(string context)
        {
            return !(m_enableContextFilters && m_contextFilters.Find(context));
        }

        private static string __logDir()
        {
            string dir;
            if (UnityEngine.Application.isEditor ||
                UnityEngine.Application.platform == UnityEngine.RuntimePlatform.WindowsPlayer ||
                UnityEngine.Application.platform == UnityEngine.RuntimePlatform.OSXPlayer)
            {
                // Application.dataPath
                // Unity Editor: <path to project folder>/Assets
                // Mac player: <path to player app bundle>/Contents
                // Win player: <path to executablename_Data folder>
                dir = UnityEngine.Application.dataPath.Replace("Assets", "Logs");
            }
            else
            {
                // Application.persistentDataPath
                // iOS: <app_sandbox>/document
                // Android: <app_sandbox>/files
                dir = Path.Combine(UnityEngine.Application.persistentDataPath, "Logs");
            }

            try
            {
                Directory.CreateDirectory(dir);
            }
            catch (Exception)
            {
            }

            return dir;
        }

        private static string __logFileName()
        {
            DateTime now = DateTime.Now;
            string filename = string.Format(
                "{0:D4}{1:D2}{2:D2}_{3:D2}{4:D2}{5:D2}.txt",
                now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second
            );
            return filename;
        }

        private static void __cleanupOldFiles(string dir, int keepDays)
        {
            try
            {
                CultureInfo enUS = new CultureInfo("en-US");
                DateTime delStartTime = DateTime.Now.AddDays(-keepDays);

                string[] fileEntries = Directory.GetFiles(dir);
                foreach (string path in fileEntries)
                {
                    if (Directory.Exists(path))
                        continue;
                    string fileName = Path.GetFileNameWithoutExtension(path);

                    DateTime fileTime;
                    if (DateTime.TryParseExact(fileName, "yyyyMMdd_hhmmss", enUS,
                        DateTimeStyles.None, out fileTime))
                    {
                        if (DateTime.Compare(fileTime, delStartTime) < 0)
                        {
                            bool ok = __deleteFile(path);
                            string msg = "SimpleLogger: __cleanupOldFiles Delete " + path + " " + ok;
                            __UnityDebugLog(Level.Debug, msg, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = "SimpleLogger: __cleanupOldFiles caught exception " + ex;
                __UnityDebugLog(Level.Error, msg, null);
            }
        }

        private static bool __deleteFile(string fileName)
        {
            try
            {
                File.Delete(fileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }

}
