using System.Collections.Generic;
using UnityEngine;
using Core.Config;

namespace Core.Utils.Log
{
    struct LogMessage
    {
        public string message;
        public LogType type;
    }

    public class Log2Screen : MonoBehaviour
    {
        public bool IsVisible = false;

        private LogMessage _msg = new LogMessage();
        private Queue<LogMessage> _msgQueue = new Queue<LogMessage>(MemBufSizes.Log2ScreenBufSize);

        private Vector2 scrollPos = Vector2.zero;
        private GUIStyle scrollstyle = new GUIStyle();
        private GUIStyle btnStyle = new GUIStyle();
        private GUILayoutOption[] layoutopt = new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true) };

        void Awake()
        {
            scrollstyle.fontSize = 45;
            Application.logMessageReceived += HandleLog;
            //useGUILayout = GlobalSwitches.EnableUnity_useGUILayout;
            useGUILayout = true;
        }

        void OnGUI()
        {
            if (GUI.Button(new Rect(0, 50, 300, 80), "显示/隐藏 日志", btnStyle))
            {
                IsVisible = !IsVisible;
            }

            if (IsVisible)
            {
                scrollPos = GUILayout.BeginScrollView(scrollPos, scrollstyle, layoutopt);
                foreach (var msg in _msgQueue)
                {
                    switch (msg.type)
                    {
                        case LogType.Log:
                            GUI.color = Color.white;
                            break;
                        case LogType.Warning:
                            GUI.color = Color.yellow;
                            break;
                        default: // Error/Assert/Exception
                            GUI.color = Color.red;
                            break;
                    }

                    GUILayout.Label(msg.message);
                }

                GUILayout.EndScrollView();
            }

        }

        void HandleLog(string message, string stackTrace, LogType type)
        {
            // always dequeue before enqueue (no further reallocation)
            while (_msgQueue.Count + 1 >= MemBufSizes.Log2ScreenBufSize)
                _msgQueue.Dequeue();

            _msg.message = string.Format("{0}\n{1}", message, stackTrace);
            _msg.type = type;
            _msgQueue.Enqueue(_msg);
        }
    }
}
