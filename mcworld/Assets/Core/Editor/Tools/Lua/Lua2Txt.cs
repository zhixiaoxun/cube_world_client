using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System;
using Core.Utils;

namespace Assets.Editor.Core
{
    class Lua2Txt : EditorWindow
    {
        // lua主目录
        static public string LuaPath = Application.dataPath + "/Resources/lua/";
        [MenuItem("扩展工具/Core/Lua脚本/lua与txt转换/lua转txt")]
        static void LuaToTxt()
        {
            //ChangeFilePrefix(new DirectoryInfo(LuaPath), "lua", "txt");
        }
        [MenuItem("扩展工具/Core/Lua脚本/lua与txt转换/txt转lua")]
        static void TxtToLua()
        {
            //ChangeFilePrefix(new DirectoryInfo(LuaPath), "txt", "lua");
        }

        // 修改文件后缀
        static void ChangeFilePrefix(DirectoryInfo dir, string from, string to)
        {
            var dirList = dir.GetDirectories().Where(t => t.Name.StartsWith(".") == false);
            var fileList = dir.GetFiles();

            foreach (var item in dirList)
            {
                ChangeFilePrefix(item, from, to);
            }
            foreach (var item in fileList)
            {
                if (item.Name.EndsWith(from, StringComparison.OrdinalIgnoreCase))
                {
                    string itemNameWithNoSurfix = item.FullName.Replace('\\', '/');
                    Debug.Log(item.FullName);
                    string destFullPath = FileHelper.GetFilePathWithoutExtention(item.FullName) + "." + to;
                    File.Delete(destFullPath);
                    File.Copy(item.FullName, destFullPath);
                    File.Delete(item.FullName);
                }
            }
        }
    }
}