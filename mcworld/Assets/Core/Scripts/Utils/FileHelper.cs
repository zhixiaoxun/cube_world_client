using System;
using System.IO;
using UnityEngine;
using Core.Utils.Log;

namespace Core.Utils
{
    public static class FileHelper
    {
        public static bool LoadFileByFileStream(string path, out byte[] bytes)
        {
            bytes = null;
            FileStream fs = null;
            try
            {
                fs = File.OpenRead(path);
                bytes = new byte[fs.Length];
                fs.Read(bytes, 0, (int)fs.Length);
                fs.Close();
                fs.Dispose();
                return true;
            }
            catch (Exception e)
            {
                //LoggerHelper.Info(string.Format("LoadByFileStream Exception:{0}", e.StackTrace));
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                return false;
            }
        }

        public static byte[] LoadByteFile(String fileName)
        {
            if (File.Exists(fileName))
                return File.ReadAllBytes(fileName);
            else
                return null;
        }
        public static string GetFileNameWithoutExtention(string fileName, char separator = '/')
        {
            var name = GetFileName(fileName, separator);
            return GetFilePathWithoutExtention(name);
        }

        public static string GetFilePathWithoutExtention(string fileName)
        {
            return fileName.Substring(0, fileName.LastIndexOf('.'));
        }

        public static string GetDirectoryName(string fileName)
        {
            return fileName.Substring(0, fileName.LastIndexOf('/'));
        }

        public static string GetFileName(string path, char separator = '/')
        {
            return path.Substring(path.LastIndexOf(separator) + 1);
        }

        public static string PathNormalize(this string str)
        {
            return str.Replace("\\", "/").ToLower();
        }

		public static void SaveToFile(byte[] data)
		{
			string exePath = System.IO.Directory.GetParent(Application.dataPath).FullName;
			TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
			string path = System.IO.Path.Combine(exePath, "Assets/Temp/File_" + Convert.ToInt64(ts.TotalSeconds).ToString() + ".txt");
			File.WriteAllBytes(path, data);
			LogHelper.DEBUG("FileHelper", "Write to file {0}", path);
		}
    }

}
