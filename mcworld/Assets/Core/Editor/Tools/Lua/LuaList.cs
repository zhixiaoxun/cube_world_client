using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System;
using Core.Utils;

namespace Assets.Editor.Core
{
    class LuaList : EditorWindow
    {
        // lua主目录
        static public string ResourcesPath = Application.dataPath + "/Resources/";
        static public string LuaPath = Application.dataPath + "/Resources/lua/";
        [MenuItem("扩展工具/Core/Lua脚本/登记所有lua文件")]
        static void LuaToTxt()
        {
            //string luaListFile = ResourcesPath + "lualist.txt";
            //File.Delete(luaListFile);
            //StreamWriter fs = new StreamWriter(luaListFile);
            //GetLuaFile(new DirectoryInfo(LuaPath), fs);
            //fs.Close();
        }

        // 修改文件后缀
        static void GetLuaFile(DirectoryInfo dir, StreamWriter fs)
        {
            var dirList = dir.GetDirectories().Where(t => t.Name.StartsWith(".") == false);
            var fileList = dir.GetFiles();

            foreach (var item in dirList)
            {
                GetLuaFile(item, fs);
            }
            foreach (var item in fileList)
            {
                if (item.Name.EndsWith("lua", StringComparison.OrdinalIgnoreCase))
                {
                    // item.FullName = c:/xxx/Assets/Resources/lua/xxx.lua
                    string relativePath = item.FullName.Replace('\\', '/').Replace(ResourcesPath, ""); // lua/xxx.lua
                    string luaResourceKey = FileHelper.GetFilePathWithoutExtention(relativePath); // lua/abc/def/name
                    Debug.Log(luaResourceKey);
                    fs.WriteLine(luaResourceKey);
                }
            }
        }
    }
}