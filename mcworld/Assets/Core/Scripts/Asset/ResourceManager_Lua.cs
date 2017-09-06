// 加载lua文件 由到Unity无法将.lua后缀文件识别成资源，同时方便将Core和Game的lua文件可以统一打开浏览修改
// 将所有lua文件放入StreamingAssets/Lua目录下
// 编辑器模式下（一般是win或mac）：可以直接用文件系统来加载
// 移动平台上：Lua/Core/*.lua 这些文件不打到ab包中，在构建的时候改名为.txt放入Resources目录下
//           Lua/Game/xxx/*.lua 会打进ab包中，走ab包加载策略

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Core.Config;
using Core.Utils;
using Core.Utils.Log;

// 由于slua加载器需求一个同步接口，所以加载lua文件无法异步
// 如果在ab包中，需要先预先把包解开，再同步加载
namespace Core.Asset
{
    public partial class ResourceManager
    {
#if UNITY_EDITOR
        public bool UseLuaCache = false;//调试时可以关闭
#else
        public bool UseLuaCache = true;
#endif
        Dictionary<string, byte[]> _CachedLuaBytes = new Dictionary<string, byte[]>(MemBufSizes.LuaCacheDictSize);
        HashSet<string> _TouchedLuaLargeFile = new HashSet<string>();

        // path是相对路径，其格式为 "Core/Lua/Core.lua" 或 "Core/Lua/Core"
        public byte[] LoadLuaFile(string path)
        {
            byte[] content = null;
#if UNITY_EDITOR
            content = LoadLuaFileInEditor(path);
#else
            content = LoadLuaFileInMobile(path);
#endif
            if (content != null)
            {
                // 满足尺寸条件的脚本触发缓存机制（第二次访问时缓存）
                if (content.Length < MemBufSizes.LuaCacheBytesThreshold)
                {
                    if (content.Length < MemBufSizes.LuaCacheBytesImmediately || _TouchedLuaLargeFile.Contains(path))
                    {
                        SaveLuaFileToCache(path, content);
                    }
                    else
                    {
                        _TouchedLuaLargeFile.Add(path);
                    }
                }
            }

            return content;
        }

        // 编辑器模式下加载lua文件接口
        private byte[] LoadLuaFileInEditor(string path)
        {
            if (!path.EndsWith(".lua"))
                path += ".lua";

            byte[] content = LoadLuaFileFromCache(path);
            if (content != null)
                return content;

            if (FileHelper.LoadFileByFileStream(GetLuaFullPath(path), out content))
            {
                return content;
            }
            return null;
        }

        // 移动平台加载lua文件接口
        private byte[] LoadLuaFileInMobile(string path)
        {
            if (path.EndsWith(".lua"))
                path = path.Replace(".lua", "");

            LogHelper.DEBUG("ResourceManager", "LoadTextAssetFromResource {0}", path);
            return LoadTextAssetFromResource(path);
        }

        // path as "Core/Lua/Core.lua" or "Core/Lua/Core"
        public string GetLuaFullPath(string path)
        {
            if (!path.EndsWith(".lua"))
                path += ".lua";

            if (path.StartsWith("Core")) // Core Lua
            {
                return ResourceDefine.GetCoreResourcePathInEditor() + path;
            }
            return ResourceDefine.GetGameResourcePathInEditor(path.Substring(0, path.IndexOf("/"))) + path;
        }

        byte[] LoadLuaFileFromCache(string path)
        {
            byte[] content = null;
            if (UseLuaCache)
            {
                _CachedLuaBytes.TryGetValue(path, out content);
            }
            return content;
        }

        void SaveLuaFileToCache(string path, byte[] content)
        {
            _CachedLuaBytes[path] = content;//多覆盖没什么影响。就这样干了。
        }

        public void RemoveLuaFileCache(string path)
        {
            if (_CachedLuaBytes == null)
                return;

            _CachedLuaBytes.Remove(path);
        }

        public void ClearAllLuaFileCache()
        {
            if (_CachedLuaBytes != null)
                _CachedLuaBytes.Clear();
        }
    }
}
