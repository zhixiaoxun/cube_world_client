using System;
using System.IO;
using UnityEngine;

namespace Core.Utils
{
    public class DirectoryHelper
    {
        public static void CopyFile(string dstFile, string srcFile)
        {
            if (!File.Exists(srcFile))
                return;

            if (File.Exists(dstFile))
            {
                var srcCreateTime = File.GetLastWriteTime(srcFile);
                var dstCreateTime = File.GetLastWriteTime(dstFile);
                if (dstCreateTime >= srcCreateTime)
                {
                    return; // 源没有更新，不Copy了，降低Copy失败概率
                }
            }

            Debug.Log(string.Format("Copy {0} {1}", srcFile, dstFile));
            File.Copy(srcFile, dstFile, true);
        }

		public static void CopyFiles(string dstDir, FileInfo[] files)
        {
            for (int i = 0, c = files.Length; i < c; ++i)
            {
                var file = files[i];

                CopyFile(dstDir + "/" + file.Name, file.FullName);
            }
        }

        public static void CopyDirectory(string dstDir, string srcDir)
        {
            try
            {
                if (!Directory.Exists(dstDir))
                    Directory.CreateDirectory(dstDir);

#if UNITY_EDITOR
                var dirInfo = new DirectoryInfo(srcDir);

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
                CopyFiles(dstDir, dirInfo.GetFiles("*.pdb"));
                CopyFiles(dstDir, dirInfo.GetFiles("*.dll"));
#endif

#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
            CopyFiles(dstDir, dirInfo.GetFiles("*.dylib"));
#endif
#endif
            }
            catch (Exception ex)
            {
                Debug.LogError("CopyDirectory() failed : " + ex.Message);
            }
        }
    }
}
