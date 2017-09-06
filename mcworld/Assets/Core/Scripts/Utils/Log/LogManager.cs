using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.Utils.Log
{
    public class LogManager : Singleton<LogManager>
    {
        const int LOG_KEEP_DAYS = 7;

        protected override IEnumerator OnInitCoroutine()
        {
            GameObject logObject = new GameObject("Log");
            GameObject.DontDestroyOnLoad(logObject);

            LogHelper.Enable = true;
            LogHelper.AddContextFilter("EventSystem");
            //LogHelper.AddContextFilter("CppCore");
#if UNITY_EDITOR
            LogHelper.FileBufferMode = false;
            LogHelper.OutputLevelFile = LogHelper.Level.Debug;
            LogHelper.OutputLevelUnity = LogHelper.Level.Debug;
#else
        if (Debug.isDebugBuild)
        {
            LogHelper.OutputLevelFile = LogHelper.Level.Debug;
            LogHelper.OutputLevelUnity = LogHelper.Level.Debug;
        }
        else
        {
            //LogHelper.OutputLevelFile = LogHelper.Level.Warn;
            //LogHelper.OutputLevelUnity = LogHelper.Level.Warn;
            LogHelper.OutputLevelFile = LogHelper.Level.Debug;
            LogHelper.OutputLevelUnity = LogHelper.Level.Debug;
        }
#endif
            // 清除过期文件
            LogHelper.CleanupOldFiles(LOG_KEEP_DAYS);

            Log2Screen log2Sceen = logObject.AddComponent<Log2Screen>();
            log2Sceen.IsVisible = false;

            LogHelper.INFO("Log", "program started, PID={0}", System.Diagnostics.Process.GetCurrentProcess().Id);

            yield return 1;
        }
    }
}