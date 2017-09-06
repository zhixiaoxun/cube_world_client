using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Core.Lua;
using Core.Asset;
using Core.Config;

namespace Core.UI
{
    public class UIManager : Singleton<UIManager>
    {
        public GameObject _eventSystemObject = null;
        public Canvas _canvas = null;// 画布
        public GameObject _exclusiveObject = null;// 独占对象
        
        public Dictionary<string, UIWindowBase> _windowList = new Dictionary<string, UIWindowBase>();// uiName => Window
        public Dictionary<string, Type> _uiName2ClassType = new Dictionary<string, Type>();// uiName => ClassType
        public Dictionary<Type, string> _classType2UiName = new Dictionary<Type, string>();// ClassType => uiName

        private UILoading _uiLoadingWindow = null;
        public UILoading UILoadingWindow
        {
            get { return _uiLoadingWindow; }
        }

        protected override IEnumerator OnInitCoroutine()
        {
            GameObject uiObject = new GameObject("UI");
            GameObject.DontDestroyOnLoad(uiObject);
            uiObject.layer = LayerMask.NameToLayer("UI");

            // Canvas
            GameObject canvasObject = new GameObject("Canvas");
            canvasObject.transform.parent = uiObject.transform;
            canvasObject.layer = LayerMask.NameToLayer("UI");
            _canvas = canvasObject.AddComponent<Canvas>();
            _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            _canvas.pixelPerfect = false;
            _canvas.sortingOrder = 0;
            _canvas.targetDisplay = 0;
            _canvas.additionalShaderChannels = AdditionalCanvasShaderChannels.None;

            CanvasScaler scaler = canvasObject.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1280, 720);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0;

            GraphicRaycaster caster = canvasObject.AddComponent<GraphicRaycaster>();
            caster.ignoreReversedGraphics = true;
            caster.blockingObjects = GraphicRaycaster.BlockingObjects.None;

            // 为了实现窗口独占效果
            _exclusiveObject = new GameObject("ExclusiveObject");
            _exclusiveObject.transform.parent = _canvas.transform;
            _exclusiveObject.SetActive(false); // 禁用
            _exclusiveObject.layer = LayerMask.NameToLayer("UI");
            Image i = _exclusiveObject.AddComponent<Image>();
            RectTransform rt = _exclusiveObject.transform as RectTransform;
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(1, 1);
            rt.offsetMin = new Vector2(0, 0);
            rt.offsetMax = new Vector2(0, 0);
            i.color = new Color(0, 0, 0, 0); // 透明

            // EventSystem
            _eventSystemObject = new GameObject("EventSystem");
            _eventSystemObject.transform.parent = uiObject.transform;
            _eventSystemObject.layer = LayerMask.NameToLayer("UI");
            EventSystem eventSystem = _eventSystemObject.AddComponent<EventSystem>();
            eventSystem.firstSelectedGameObject = null;
            eventSystem.sendNavigationEvents = true;
            eventSystem.pixelDragThreshold = 5;
            StandaloneInputModule inputModule = _eventSystemObject.AddComponent<StandaloneInputModule>();
            inputModule.forceModuleActive = false;

            // loading window
            _uiLoadingWindow = OpenWindow("UILoading", typeof(UILoading), false, false) as UILoading;

            yield return 1;
        }

        //static public void RegisterWindow()
        //{
        //    foreach (string key in UIInfoList.Keys)
        //    {
        //        Type ClassType = Type.GetType("Core.UI." + UIInfoList[key].ClassName);
        //        UiName2ClassType[UIInfoList[key].Name] = ClassType;
        //        ClassType2UiName[ClassType] = UIInfoList[key].Name;
        //    }
        //}

        public void RegisterOneWindow(string uiName, Type uiType)
        {
            //UIConfigData uiData = UIConfigData.dataMap.First(t => t.Value.name == uiName).Value;
            //Type uiType = Type.GetType("Core.UI." + uiType.Name);

            // 已经注册过
            if (_uiName2ClassType.ContainsKey(uiName) && _classType2UiName.ContainsKey(uiType))
                return;

            // 注册窗口类
            _uiName2ClassType[uiName] = uiType;
            _classType2UiName[uiType] = uiName;
        }

        public UIWindowBase OpenWindow(string uiName, Type uiType, bool bExclusive = false, bool bUseLua = false)
        {
            if (!bUseLua)
            {
                RegisterOneWindow(uiName, uiType);
            }

            UIWindowBase window = GetWindow(uiName);
            if (window == null)
            {
                GameObject wndObject = null;
                UnityEngine.Object o = Resources.Load("Core/UI/" + uiName);
                if(o == null)
                {
                    o = Resources.Load(ResourceManager.Instance.GetResourceName(CoreEnv._CurGameName, "UI/" + uiName));
                }

                wndObject = GameObject.Instantiate(o) as GameObject;
                wndObject.layer = LayerMask.NameToLayer("UI");

                if (!bUseLua)
                {
                    window = (UIWindowBase)wndObject.AddComponent(GetUIClassTypeByUIName(uiName));
                }
                else
                {
                    window = (UIWindowBase)wndObject.AddComponent(typeof(LuaUIWindow));
                }
                window.rectTransform = wndObject.GetComponent<RectTransform>();

                if (bExclusive) // 独占
                {
                    window.isExclusive = bExclusive;
                    _exclusiveObject.SetActive(true);
                    window.rectTransform.SetParent(_exclusiveObject.transform);
                    _exclusiveObject.transform.SetAsLastSibling();
                }
                else
                {
                    window.rectTransform.SetParent(_canvas.gameObject.GetComponent<RectTransform>());
                }
                window.rectTransform.anchoredPosition3D = new Vector3(0.0f, 0.0f, 0.0f);    //将UI相对锚点的偏移置为空

                if (window is LuaUIWindow)   //如果打开的UI选用LUA实现的版本，则需要在寻常的Init流程前先指定LUA脚本路径，以便初始化SLUA相关的组件。否则Init函数无法调用到对应Lua脚本的Init方法。
                {
                    (window as LuaUIWindow).UIName = uiName;
                }
                window.Init();
                AddWindow(uiName, window);
            }

            window.transform.SetAsLastSibling();
            //UIStartLoading.Instance.transform.SetAsLastSibling();
            window.Show();

            return window;
        }

        public void CloseWindow(string uiName, bool bDestroy = false)
        {
            UIWindowBase wnd = GetWindow(uiName);
            if (wnd != null)
            {
                if (wnd.isExclusive)
                {
                    wnd.isExclusive = false;
                    _exclusiveObject.SetActive(false);
                }

                if (bDestroy)
                {
                    wnd.UnInit();
                    DelWindow(uiName);
                    GameObject.Destroy(wnd.gameObject);
                }
                else
                {
                    wnd.Hide();
                }
            }
        }

        public void AddWindow(string uiName, UIWindowBase window)
        {
            _windowList[uiName] = window;
        }

        public void DelWindow(string uiName)
        {
            _windowList.Remove(uiName);
        }

        public UIWindowBase GetWindow(string uiName)
        {
            if (_windowList.ContainsKey(uiName))
            {
                return _windowList[uiName];
            }

            return null;
        }

        public UIWindowBase GetWindow(Type T)
        {
            string uiName = GetUINameByUIClassType(T);

            if (uiName != null)
                return GetWindow(uiName);

            return null;
        }

        public string GetUINameByUIClassType(Type T)
        {
            if (_classType2UiName.ContainsKey(T))
                return _classType2UiName[T];
            return null;
        }

        public Type GetUIClassTypeByUIName(string uiName)
        {
            if (_uiName2ClassType.ContainsKey(uiName))
                return _uiName2ClassType[uiName];
            return null;
        }
    }

}
