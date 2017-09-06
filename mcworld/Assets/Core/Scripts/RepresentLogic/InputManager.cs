using System;
using System.Collections;
using UnityEngine;
using Core.Utils;
using SLua;
using Core.Lua;

namespace Core.RepresentLogic
{
    [CustomLuaClass]
    public class InputManager : MonoBehaviour
    {
#if UNITY_IOS || UNITY_ANDROID
        public bool MobileInput = true; // 启用移动平台输入
#else
        public bool MobileInput = true; // 启用移动平台输入
#endif
        private Vector2 _LastMouseVec = Vector2.zero; // 上一帧鼠标位置
        private float _MouseX = 0;
        private float _MouseY = 0;
        private Vector2 _SwipeVec = Vector2.zero;   //滑屏方向和距离
        public Vector2 SwipeVec
        {
            get { return _SwipeVec; }
        }

        private Vector2 joystickVec;    //摇杆滑动方向和距离
        public Vector2 JoyStickVec
        {
            set { joystickVec = value; }
            get { return joystickVec; }
        }

        //LuaInputManager回调缓存
        private LuaTable _luaInputManager;
        private LuaFunction _luaInit;
        private LuaFunction _luaOnTouchStart;
        private LuaFunction _luaOnTouchUp;
        private LuaFunction _luaOnSwipeStart;
        private LuaFunction _luaOnSwipe;
        private LuaFunction _luaOnSwipeEnd;
        private LuaFunction _luaOnSimpleTap;
        private LuaFunction _luaOnLongTap;
        private LuaFunction _luaOnLongTapEnd;

        void Start()
        {
            if (!MobileInput)
            {
                CursorHandler.LockCursor();
            }

            //初始化LuaInputManager
            _luaInputManager = LuaManager.Instance.GetLuaTable("LuaInputManager");
            _luaInit = (LuaFunction)_luaInputManager["Init"];
            _luaOnTouchStart = (LuaFunction)_luaInputManager["OnTouchStart"];
            _luaOnTouchUp = (LuaFunction)_luaInputManager["OnTouchUp"];
            _luaOnSwipeStart = (LuaFunction)_luaInputManager["OnSwipeStart"];
            _luaOnSwipe = (LuaFunction)_luaInputManager["OnSwipe"];
            _luaOnSwipeEnd = (LuaFunction)_luaInputManager["OnSwipeEnd"];
            _luaOnSimpleTap = (LuaFunction)_luaInputManager["OnSimpleTap"];
            _luaOnLongTap = (LuaFunction)_luaInputManager["OnLongTap"];
            _luaOnLongTapEnd = (LuaFunction)_luaInputManager["OnLongTapEnd"];

            _luaInit.call(_luaInputManager, this);
        }

        void Update()
        {
            if (!MobileInput)
            {
                if (CursorHandler.IsCursorLocked())
                {
                    if (Input.GetKeyUp(KeyCode.Escape))
                    {
                        CursorHandler.ReleaseCursor();
                        return;
                    }

                    _MouseX = Input.GetAxis("Mouse X");
                    _MouseY = Input.GetAxis("Mouse Y");
                    Vector2 mouseVec = new Vector2(_MouseX, _MouseY);
                    if (Math.Abs((_LastMouseVec - mouseVec).magnitude) <= 0.001f)
                    {
                        // 距离上次没有滑动
                        _SwipeVec = Vector2.zero;
                    }
                    else
                    {
                        _SwipeVec = mouseVec - _LastMouseVec;
                    }
                    //Debug.Log(string.Format("_SwipeVector={0}", _SwipeVec));
                }
            }
        }

        #region EasyTouch事件注册和处理
        void OnEnable()
        {
            RegisterEvent();
        }

        void OnDisable()
        {
            UnRegisterEvent();
        }

        void OnDestroy()
        {
            UnRegisterEvent();
        }

        private void RegisterEvent()
        {
            //单次按下
            EasyTouch.On_TouchStart += OnTouchStart;
            //单次松开
            EasyTouch.On_TouchUp += OnTouchUp;

            //滑屏
            EasyTouch.On_SwipeStart += OnSwipeStart;
            EasyTouch.On_Swipe += OnSwipe;
            EasyTouch.On_SwipeEnd += OnSwipeEnd;

            //单次点击
            EasyTouch.On_SimpleTap += OnSimpleTap;

            //长按
            EasyTouch.On_LongTap += OnLongTap;
            EasyTouch.On_LongTapEnd += OnLongTapEnd;
        }

        private void UnRegisterEvent()
        {
            //单次按下
            EasyTouch.On_TouchStart -= OnTouchStart;
            //单次松开
            EasyTouch.On_TouchUp -= OnTouchUp;

            //滑屏
            EasyTouch.On_SwipeStart -= OnSwipeStart;
            EasyTouch.On_Swipe -= OnSwipe;
            EasyTouch.On_SwipeEnd -= OnSwipeEnd;

            //单次点击
            EasyTouch.On_SimpleTap -= OnSimpleTap;

            //长按
            EasyTouch.On_LongTap -= OnLongTap;
            EasyTouch.On_LongTapEnd -= OnLongTapEnd;
        }

        private void OnTouchStart(Gesture gesture)
        {
            //Debug.Log("Touch Start" + gesture.position);
            _luaOnTouchStart.call(_luaInputManager);
        }

        private void OnTouchUp(Gesture gesture)
        {
            //Debug.Log("Touch Up" + gesture.position);
            _luaOnTouchUp.call(_luaInputManager);
        }

        private void OnSwipeStart(Gesture gesture)
        {
            //Debug.Log("OnSwipeStart");
            _luaOnSwipeStart.call(_luaInputManager);
        }

        private void OnSwipe(Gesture gesture)
        {
            _SwipeVec = gesture.deltaPosition;
            _luaOnSwipe.call(_luaInputManager, gesture.deltaPosition);
        }

        private void OnSwipeEnd(Gesture gesture)
        {
            //Debug.Log("OnSwipeEnd");
            _SwipeVec = Vector2.zero;
            _luaOnSwipeEnd.call(_luaInputManager);
        }

        void OnSimpleTap(Gesture gesture)
        {
            //Debug.Log("Simple Tap" + gesture.position);
            _luaOnSimpleTap.call(_luaInputManager);
        }

        void OnLongTap(Gesture gesture)
        {
            //Debug.Log("Long Tap" + gesture.position);
            //Debug.Log("Long Tap DeltaTime" + gesture.deltaTime);
            _luaOnLongTap.call(_luaInputManager, gesture.deltaTime);
        }

        void OnLongTapEnd(Gesture gesture)
        {
            //Debug.Log("Long Tap End" + gesture.position);
            _luaOnLongTapEnd.call(_luaInputManager);
        }
        #endregion
    }
}