using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Core.UI;
using SLua;

namespace Core.Lua
{
    [CustomLuaClass]
    public class LuaUIWindow : UIWindowBase
    {
        static private Action<object, string> NullObjectChecker = (object obj, string msg) =>
        {
            if (obj == null)
            {
                Debug.Log(msg);
                return;
            }
        };

        private string uiName = null;
        public string UIName
        {
            set
            {
                uiName = value;
                InitLuaScript();
            }
            get
            {
                return uiName;
            }
        }
        private LuaTable selfTable = null;
        public LuaTable SelfTable { get { return selfTable; } }

        private LuaSvr luaService = null;
        private LuaFunction luaFuncUpdate = null;
        private LuaFunction luaFuncInit = null;
        private LuaFunction luaFuncUnInit = null;
        private LuaFunction luaFuncOnShow = null;
        private LuaFunction luaFuncOnHide = null;
        private LuaFunction luaFuncOnPointerClick = null;
        private LuaFunction luaFuncOnButtonClick = null;
        private LuaFunction luaFuncOnPointerUp = null;
        private LuaFunction luaFuncOnPointerDown = null;
        private LuaFunction luaFuncOnBeginDrag = null;
        private LuaFunction luaFuncOnDrag = null;
        private LuaFunction luaFuncOnEndDrag = null;

        private LuaFunction luaFuncRegisterWindowElemEvent = null;
        private LuaFunction luaFuncRegisterButtonClickEvent = null;

        static public void OpenWindow(string uiName, bool bExclusive = false)
        {
            UIManager.Instance.OpenWindow(uiName, null, false, true);
        }

        static public void CloseWindow(string uiName, bool bDestory = false)
        {
            UIManager.Instance.CloseWindow(uiName);
        }

        [DoNotToLua]
        static public LuaUIWindow GetWindow(string uiName)
        {
            UIWindowBase window = UIManager.Instance.GetWindow(uiName);
            if (window is LuaUIWindow)
            {
                return window as LuaUIWindow;
            }
            else
            {
                return null;
            }
        }

        public object CallLuaFunction(string functionName, params object[] args)
        {
            NullObjectChecker(luaService, "Error! The luaService is null!");

            string uiMemberFunction = uiName + "." + functionName;
            LuaFunction func = luaService.luaState.getFunction(uiMemberFunction);
            if (func != null)
            {
                return func.call(args);
            }
            else
            {
                Debug.Log("Can't find Lua function " + functionName);
                return null;
            }
        }

        public override void Awake()
        {
            //Awake调用时还没有指定负责操纵这个UI的LUA脚本，所以先将脚本禁用。
            enabled = false;
            luaService = LuaManager.Instance.LuaService;
        }

        public override void Init()
        {
            rectTransform.offsetMin = new Vector2(0.0f, 0.0f);
            rectTransform.offsetMax = new Vector2(0.0f, 0.0f);
            rectTransform.localScale = new Vector3(1, 1, 1);
            //CallLuaFunction("Init", selfTable, this, rectTransform);
            if (luaFuncInit != null)
            {
                luaFuncInit.call(selfTable, this, rectTransform);
            }
        }

        public override void UnInit()
        {
            //CallLuaFunction("UnInit", selfTable);
            if (luaFuncUnInit != null)
            {
                luaFuncUnInit.call(selfTable);
            }
        }

        public override void OnShow()
        {
            //CallLuaFunction("OnShow", selfTable);
            if (luaFuncOnShow != null)
            {
                luaFuncOnShow.call(selfTable);
            }
        }

        public override void OnHide()
        {
            //CallLuaFunction("OnHide", selfTable);
            if (luaFuncOnHide != null)
            {
                luaFuncOnHide.call(selfTable);
            }
        }

        public override void Update()
        {
            //CallLuaFunction("Update", selfTable);
            if (luaFuncUpdate != null)
            {
                luaFuncUpdate.call(selfTable);
            }
        }

        public override void RegisterWindowElemEvent()
        {
            //CallLuaFunction("RegisterWindowElemEvent", selfTable);
            if (luaFuncRegisterWindowElemEvent != null)
            {
                luaFuncRegisterWindowElemEvent.call(selfTable);
            }
        }

        public override void RegisterButtonClickEvent(string btnName, Button buttonComponent)
        {
            //CallLuaFunction("RegisterButtonClickEvent", selfTable, btnName, buttonComponent);
            if (luaFuncRegisterButtonClickEvent != null)
            {
                luaFuncRegisterButtonClickEvent.call(selfTable, btnName, buttonComponent);
            }
        }

        public override void SetImageBackGround(Sprite sprite)
        {
            CallLuaFunction("SetImageBackGround", selfTable, null, sprite);
        }

        public override void SetImageBackGround(string objName, Sprite sprite)
        {
            CallLuaFunction("SetImageBackGround", selfTable, objName, sprite);
        }

        public override void SetText(string txtName, string txtContent)
        {
            CallLuaFunction("SetText", selfTable, txtName, txtContent);
        }
        public override void SetText(string txtName, string txtContent, Color color)
        {
            CallLuaFunction("SetText", selfTable, txtName, txtContent, color);
        }
        public override void SetTextColor(string txtName, Color color)
        {
            CallLuaFunction("SetTextColor", selfTable, txtName, color);
        }
        public override string GetText(string txtName)
        {
            return CallLuaFunction("GetText", selfTable, txtName) as string;
        }
        public override void SetButtonText(GameObject obj, string txtContent, Color color, string btnTextName = "Text")
        {
            CallLuaFunction("SetButtonText", selfTable, obj, null, txtContent, color, btnTextName);
        }

        public override void SetButtonText(string btnName, string txtContent, string btnTextName = "Text")
        {
            CallLuaFunction("SetButtonText", selfTable, null, btnName, txtContent, null, btnTextName);
        }

        public override void SetButtonText(GameObject obj, string txtContent, string btnTextName = "Text")
        {
            CallLuaFunction("SetButtonText", selfTable, obj, null, txtContent, null, btnTextName);
        }
        public override void SetButtonImage(GameObject obj, Sprite sprite)
        {
            CallLuaFunction("SetButtonImage", selfTable, obj, null, sprite);
        }
        public override void SetButtonImage(string btnName, Sprite sprite)
        {
            CallLuaFunction("SetButtonImage", selfTable, null, btnName, sprite);
        }
        public override void SetButtonColor(GameObject obj, Color color)
        {
            CallLuaFunction("SetButtonColor", selfTable, obj, null, color);
        }
        public override void SetButtonColor(string btnName, Color color)
        {
            CallLuaFunction("SetButtonColor", selfTable, null, btnName, color);
        }
        public override Color GetButtonColor(GameObject obj)
        {
            return (Color)CallLuaFunction("GetButtonColor", selfTable, obj);
        }
        public override void SetElemScale(GameObject obj, float scale)
        {
            CallLuaFunction("SetElemScale", selfTable, obj, scale);
        }
        public override void SetButtonEnable(string btnName, bool isEnable)
        {
            CallLuaFunction("SetButtonEnable", selfTable, btnName, isEnable);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            //CallLuaFunction("OnPointerClick", selfTable, eventData);
            if (luaFuncOnPointerClick != null)
            {
                luaFuncOnPointerClick.call(selfTable, eventData);
            }
        }

        public override void OnButtonClick(string strObjName)
        {
            //CallLuaFunction("OnButtonClick", selfTable, strObjName);
            if (luaFuncOnButtonClick != null)
            {
                luaFuncOnButtonClick.call(selfTable, strObjName);
            }
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            //CallLuaFunction("OnPointerUp", selfTable, eventData);
            if (luaFuncOnPointerUp != null)
            {
                luaFuncOnPointerUp.call(selfTable, eventData);
            }
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            //CallLuaFunction("OnPointerDown", selfTable, eventData);
            if (luaFuncOnPointerDown != null)
            {
                luaFuncOnPointerDown.call(selfTable, eventData);
            }
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            //CallLuaFunction("OnBeginDrag", selfTable, eventData);
            if (luaFuncOnBeginDrag != null)
            {
                luaFuncOnBeginDrag.call(selfTable, eventData);
            }
        }

        public override void OnDrag(PointerEventData eventData)
        {
            //CallLuaFunction("OnDrag", selfTable, eventData);
            if (luaFuncOnDrag != null)
            {
                luaFuncOnDrag.call(selfTable, eventData);
            }
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            //CallLuaFunction("OnEndDrag", selfTable, eventData);
            if (luaFuncOnEndDrag != null)
            {
                luaFuncOnEndDrag.call(selfTable, eventData);
            }
        }

        private void InitLuaScript()
        {
            NullObjectChecker(uiName, "Error! The scriptName is null!");

            enabled = true;
            selfTable = LuaManager.Instance.GetLuaTable(uiName);

            string uiMemberFunction = uiName + ".Update";
            luaFuncUpdate = (LuaFunction)selfTable["Update"];
            luaFuncInit = (LuaFunction)selfTable["Init"];
            luaFuncUnInit = (LuaFunction)selfTable["UnInit"];
            luaFuncOnShow = (LuaFunction)selfTable["OnShow"];
            luaFuncOnHide = (LuaFunction)selfTable["OnHide"];
            luaFuncOnPointerClick = (LuaFunction)selfTable["OnPointerClick"];
            luaFuncOnButtonClick = (LuaFunction)selfTable["OnButtonClick"];
            luaFuncOnPointerUp = (LuaFunction)selfTable["OnPointerUp"];
            luaFuncOnPointerDown = (LuaFunction)selfTable["OnPointerDown"];
            luaFuncOnBeginDrag = (LuaFunction)selfTable["OnBeginDrag"];
            luaFuncOnDrag = (LuaFunction)selfTable["OnDrag"];
            luaFuncOnEndDrag = (LuaFunction)selfTable["OnEndDrag"];
        }
    }
}
