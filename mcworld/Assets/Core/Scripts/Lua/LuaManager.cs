using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLua;
using Core.Utils.Log;
using Core.Asset;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System;

namespace Core.Lua
{
    public class LuaManager : Singleton<LuaManager> // TODO 释放某个游戏的脚本环境避免不同游戏的Lua造成全局table重名
    {
        private const string _luaCoreFile = "Core.lua";
        private const string _luaCorePath = "Core/Lua/" + _luaCoreFile;
        private const string _luaCoreTable = "LuaCore";

        private const string _luaGameFile = "Game.lua";
        private const string _luaGameTable = "LuaGame";

        private const string _luaInputManagerFile = "LuaInputManager.lua";
        private const string _luaInputManager = "Core/Lua/Input/" + _luaInputManagerFile;

        private LuaState _luaState;
        private bool _isFinished = false;
        private bool _preloadAll = false; // 是否在启动时加载所有Lua
        
        // 缓存LuaTable方法 避免每次使用都查找
        //private LuaFunction _luaFunc_Core_Init = null;
        //private LuaFunction _luaFunc_Game_Init = null;

        public LuaSvr LuaService { get; private set; }

        // lua中的全局table列表 TableName => LuaTable
        public Dictionary<string, LuaTable> LuaTableList { get; private set; }

        protected override IEnumerator OnInitCoroutine()
        {
            LuaState.loaderDelegate = ResourceManager.Instance.LoadLuaFile;

            LuaService = new LuaSvr();
            LuaTableList = new Dictionary<string, LuaTable>();

#if UNITY_EDITOR
            LuaService.init(tick, complete/*, LuaSvrFlag.LSF_DEBUG*/);
#else
			LuaService.init(tick, complete);
#endif

            while (!_isFinished)
            {
                //LoggerHelper.Debug("!LuaEngine.isInitFinished");
                yield return 1;
            }
        }

        void tick(int p)
        {
            //LogHelper.DEBUG("LuaManager", "LuaManager tick {0}", p);
        }

        void complete()
        {
            LogHelper.DEBUG("LuaManager", "complete");
            _luaState = LuaService.luaState;

            LogHelper.DEBUG("LuaManager", "单独加载LuaCore");
            // 单独加载LuaCore
            LuaTable resultTable = (LuaTable)StartFile(_luaCorePath);
            //_luaFunc_Core_Init = (LuaFunction)(((LuaTable)resultTable[1])["RegisterModule"]);

            LogHelper.DEBUG("LuaManager", "单独加载_luaInputManager");
            //单独加载LuaInputManager
            StartFile(_luaInputManager);

            string luaCoreRealPath = ResourceManager.Instance.GetLuaFullPath(_luaCorePath);
            LogHelper.DEBUG("LuaManager", "luaCoreRealPath={0}", luaCoreRealPath);

            string luaRootPath = luaCoreRealPath.Replace(_luaCorePath, "");

            if (_preloadAll)
            {
                // 预加载所有lua文件
                // 1.编辑器模式下可以遍历目录去得到所有lua列表
                // 2.pc与移动平台不支持目录遍历，需要生成一个PreLoadList.lua来得到lua列表
#if UNITY_EDITOR
                // 根据LuaCore.lua所在的目录去遍历加载其它所有lua文件（仅Core和当前项目的lua文件）
                Stopwatch sw = new Stopwatch();
                sw.Start();
                LogHelper.DEBUG("LuaManager", "开始加载所有lua文件");

                LoadAllLuaScript(luaRootPath); // luaRootPath "Lua"文件夹的所在的绝对路径

                LogHelper.DEBUG("LuaManager", "加载所有lua文件完成，消耗时间{0}毫秒", sw.ElapsedMilliseconds);
                sw.Stop();

#elif UNITY_ANDROID || UNITY_IOS
                // 非编辑器模式下暂时不支持预加载所有lua文件
#endif
            }

            BuildLuaFuncCache(); // 缓存LuaTable方法 避免每次使用都查找 前提是脚本必须已经加载
            _isFinished = true;
        }

        private void LoadAllLuaScript(string luaRootPath)
        {
            string luaCorePathRoot = luaRootPath + "Core/Lua";
            LoadLuaScriptFromEditor(luaRootPath, new DirectoryInfo(luaCorePathRoot));
        }
        
        private void LoadLuaScriptFromEditor(string luaRootPath, DirectoryInfo info)
        {
            var dirList = info.GetDirectories().Where(t => t.Name.StartsWith(".") == false);
            var fileList = info.GetFiles();

            // 处理子目录
            foreach (var subDir in dirList)
            {
                LoadLuaScriptFromEditor(luaRootPath, subDir);
            }

            // 遍历处理文件
            int fileListLength = fileList.Length;
            for (int i = 0; i < fileListLength; ++i)
            {
                var key = fileList[i].Name;

                string fileName = fileList[i].Name; // 仅文件名 xxx.lua
                string fileFullName = fileList[i].FullName.Replace('\\', '/'); // 全路径 c:/project/Assets/StreamingAssets/Lua/Core/xxx.lua
                if (!fileName.EndsWith(".lua", StringComparison.OrdinalIgnoreCase)) // 忽略非lua文件
                    continue;

                // 排除引擎文件
                if (fileName.IndexOf(_luaCoreFile) != -1 || fileName.IndexOf(_luaInputManagerFile) != -1) // 排除掉luaEntryFile，前面的流程已经加载
                    continue;

                // 可以加载的Lua脚本路径 格式："Lua/Core/xxx.lua" "Lua/Game/xxx.Lua"
                string luaPath = fileFullName.Replace(luaRootPath, "");
                LogHelper.DEBUG("LuaManager", "Loading Lua itemName={0} fullName={1} luaPath={2}", fileList[i].Name, fileList[i].FullName, luaPath);

                // 加载lua脚本
                StartFile(luaPath);
            }
        }

        private void BuildLuaFuncCache()
        {
            LuaTable luaTable = null;

            if (_preloadAll)
            {
#if UNITY_EDITOR
                // 在这里可以放心的缓存需要的table
#elif UNITY_ANDROID || UNITY_IOS
                // 非编辑器模式下暂时不支持预加载所有lua文件，这里只能缓存已经加载的table
#endif
            }

            //// 缓存Game的初始化方法
            //try
            //{
            //    // 如果Lua脚本没有报错的话，这个肯定是有的
            //    luaTable = GetLuaTable(_luaGameTable);
            //    _luaFunc_Game_Init = (LuaFunction)luaTable["Init"];
            //}
            //catch (Exception e)
            //{
            //    LoggerHelper.Error(string.Format("BuildLuaFuncCache {0}:Init error! msg={1}", _luaGameTable, e.Message));
            //}
        }

        public void StartGameScript(string projectName)
        {
            string luaGamePath = projectName + "/Lua/" + _luaGameFile;
            // 单独加载LuaGame
            StartFile(luaGamePath);
            LuaManager.Instance.CallLuaClassFunction("LuaGame", "Init");
        }

        public object StartFile(string path)
        {
            if (path == null)
                return null;

            object ret = LuaService.start(path);
            if (ret == null)
                return null;

            LuaTable resultTable = (LuaTable)ret;
            LuaTable luaTable = (LuaTable)resultTable[1];
            string luaTableName = (string)resultTable[2];

            RegisterLuaTable(luaTableName, luaTable);

            return ret;
        }

        public object DoFile(string path)
        {
            if (path == null)
                return null;
            return _luaState.doFile(path);
        }

        private void RegisterLuaTable(string luaTableName, LuaTable luaTable)
        {
            try
            {
                LuaTableList.Add(luaTableName, luaTable);
            }
            catch (Exception)
            {
                LogHelper.ERROR("LuaManamger", "RegisterLuaTable duplicate global luaTable {0}", luaTableName);
            }
        }
        public LuaTable GetLuaTable(string luaTableName)
        {
            if (!LuaTableList.ContainsKey(luaTableName))
            {
                LuaTable table = LuaService.luaState.getTable(luaTableName);
                if (table != null)
                {
                    RegisterLuaTable(luaTableName, table);
                }
            }
            return LuaTableList[luaTableName];
        }

        public object CallLuaClassFunction(string tableName, string functionName, params object[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            if (!LuaTableList.ContainsKey(tableName))
            {
                LuaTable table = LuaService.luaState.getTable(tableName);
                if (table == null)
                {
                    LogHelper.ERROR("LuaManager", "CallLuaClassFunction Can't find Lua function {0} table not exist {1}", functionName, tableName);
                    return null;
                }
                RegisterLuaTable(tableName, table);
            }

            LuaTable luaTable = LuaTableList[tableName];
            string memberFunction = tableName + "." + functionName;
            LuaFunction func = LuaService.luaState.getFunction(memberFunction);
            if (func == null)
            {
                LogHelper.DEBUG("LuaManager", "Can't find Lua function {0}", functionName);
                return null;
            }

            object[] newArgs = new object[args.Count() + 1];
            newArgs[0] = luaTable;
            args.CopyTo(newArgs, 1);

            object ret = func.call(newArgs);

            if (sw.ElapsedMilliseconds > 20)
                LogHelper.DEBUG("LuaManager", "执行CallLuaClassFunction[{0}.{1}]，消耗时间{2}毫秒", tableName, functionName, sw.ElapsedMilliseconds);
            sw.Stop();

            return ret;
        }

        public object GetLuaTableMember(string tableName, string memberName)
        {
            LuaTable luaTable = LuaTableList[tableName];
            return luaTable[memberName];
        }

        public int GetLuaTableMemberInt(string tableName, string memberName)
        {
            return (int)GetLuaTableMember(tableName, memberName);
        }

        public string GetLuaTableMemberString(string tableName, string memberName)
        {
            return (string)GetLuaTableMember(tableName, memberName);
        }

        public bool GetLuaTableMemberBool(string tableName, string memberName)
        {
            return (bool)GetLuaTableMember(tableName, memberName);
        }
    }
}