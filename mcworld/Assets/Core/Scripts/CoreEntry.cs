using System;
using System.Collections;
using UnityEngine;
using SLua;
using Core.Utils.Log;
using Core.Utils;
using Core.Lua;
using Core.RepresentLogic;
using Core.Performance;
using Core.UI;
using Core.Projects;
using Core.Asset;
using Core.Config;
using Core.GameLogic;

namespace Core
{
    [CustomLuaClass]
    public class CoreEntry : MonoBehaviour
    {
        public static CoreEntry Instance { get; private set; } = null;
        public LifeCycle _LifeCycle { get; private set; } = null;
        public bool InitFinished { get; private set; } = false;

        private IEnumerator InitCore()
        {
            //启动驱动逻辑
            InvokeRepeating("Tick", 1, 0.02f);

            // 分4批初始化所有管理器
            yield return _LifeCycle.Singletons.InitCoroutine(0, RefreshProgress);
            yield return _LifeCycle.Singletons.InitCoroutine(1, RefreshProgress);
            yield return _LifeCycle.Singletons.InitCoroutine(2, RefreshProgress);
            yield return _LifeCycle.Singletons.InitCoroutine(3, RefreshProgress);

            UIManager.Instance.UILoadingWindow.SetLoadingInfo("所有核心模块初始化完成");
            UIManager.Instance.UILoadingWindow.SetLoadingStatus(100);
            yield return 1;

            UIManager.Instance.UILoadingWindow.Hide();
            // 调用Lua初始化方法
            LuaManager.Instance.CallLuaClassFunction("LuaCore", "Init");

            InitFinished = true;
        }
        IEnumerator RefreshProgress(string stepName, int step, int count)
        {
            UIManager.Instance.UILoadingWindow.SetLoadingInfo("初始化" + stepName);
            UIManager.Instance.UILoadingWindow.SetLoadingStatus(step * 100 / count);

            LogHelper.DEBUG("CoreEntry", "RefreshProgress stepName={0} step={1} count={2}", stepName, step, count);
            yield return 1;
        }

        void Awake()
        {
            CoreEnv.CoreDriver = this;
            Instance = this;
            _LifeCycle = null;
            InitFinished = false;
            DontDestroyOnLoad(gameObject);

            Screen.sleepTimeout = (int)SleepTimeout.NeverSleep;
            Application.targetFrameRate = 60;

            WrapHelper.InitLibrary();
        }
        void Start()
        {
            // 分4批注册所有管理器 注意依赖关系
            _LifeCycle = gameObject.AddComponent<LifeCycle>();
            _LifeCycle.Add(0, new UIManager()); // UI
            _LifeCycle.Add(0, new LogManager()); // Log
            _LifeCycle.Add(0, new ResourceManager()); // 资源
            _LifeCycle.Add(0, new RepresentManager()); // 表现
            _LifeCycle.Add(1, new PerformanceManager()); // 性能监控
            _LifeCycle.Add(1, new ProjectManager()); // 工程管理
            _LifeCycle.Add(1, new LuaManager()); // Lua引擎
            _LifeCycle.Add(2, new InteractionManager()); // 交互
            _LifeCycle.Add(3, new RepresentMap()); // 地图资源

            StartCoroutine(InitCore());
        }
        void Update()
        {
            
        }
        void LateUpdate()
        {
        }
        void FixedUpdate()
        {
        }
        void EndOfFrame()
        {

        }

        void OnApplicationQuit()
        {
            _LifeCycle.Singletons.Exit();

			if (CoreEnv._World != null)
                CoreEnv._World.UnInit();

			WrapHelper.FreeLibrary();

            LogHelper.DEBUG("CoreEntry", "Core Quit");
        }

        public bool StartGame(string gameName)
        {
            LogHelper.DEBUG("CoreEntry", $"StartGame {gameName}");
            if (!ProjectManager.Instance.ProjectNameList.Contains(gameName))
                return false;

            try
            {
                CoreEnv._CurGameName = gameName;
                LuaManager.Instance.StartGameScript(gameName);
                return true;
            }
            catch (Exception e)
            {
                LogHelper.DEBUG("CoreEntry", $"StartGame {gameName} failed! Msg={e.Message}");
                return false;
            }
        }
        void Tick()
        {
            if (!InitFinished)
                return;
            TimerHeap.Tick();
            FrameTimerHeap.Tick();
        }
        public static void Invoke(Action action)
        {
            TimerHeap.AddTimer(0, 0, action);
        }
    }
}
