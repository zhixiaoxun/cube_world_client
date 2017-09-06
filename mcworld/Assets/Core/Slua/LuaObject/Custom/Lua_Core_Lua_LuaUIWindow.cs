using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_Core_Lua_LuaUIWindow : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int constructor(IntPtr l) {
		try {
			Core.Lua.LuaUIWindow o;
			o=new Core.Lua.LuaUIWindow();
			pushValue(l,true);
			pushValue(l,o);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int CallLuaFunction(IntPtr l) {
		try {
			Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Object[] a2;
			checkParams(l,3,out a2);
			var ret=self.CallLuaFunction(a1,a2);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Awake(IntPtr l) {
		try {
			Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
			self.Awake();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Init(IntPtr l) {
		try {
			Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
			self.Init();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int UnInit(IntPtr l) {
		try {
			Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
			self.UnInit();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int OnShow(IntPtr l) {
		try {
			Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
			self.OnShow();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int OnHide(IntPtr l) {
		try {
			Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
			self.OnHide();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Update(IntPtr l) {
		try {
			Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
			self.Update();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int RegisterWindowElemEvent(IntPtr l) {
		try {
			Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
			self.RegisterWindowElemEvent();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int RegisterButtonClickEvent(IntPtr l) {
		try {
			Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			UnityEngine.UI.Button a2;
			checkType(l,3,out a2);
			self.RegisterButtonClickEvent(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int SetImageBackGround(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==2){
				Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
				UnityEngine.Sprite a1;
				checkType(l,2,out a1);
				self.SetImageBackGround(a1);
				pushValue(l,true);
				return 1;
			}
			else if(argc==3){
				Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				UnityEngine.Sprite a2;
				checkType(l,3,out a2);
				self.SetImageBackGround(a1,a2);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int SetText(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==3){
				Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				self.SetText(a1,a2);
				pushValue(l,true);
				return 1;
			}
			else if(argc==4){
				Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				UnityEngine.Color a3;
				checkType(l,4,out a3);
				self.SetText(a1,a2,a3);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int SetTextColor(IntPtr l) {
		try {
			Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			UnityEngine.Color a2;
			checkType(l,3,out a2);
			self.SetTextColor(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetText(IntPtr l) {
		try {
			Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetText(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int SetButtonText(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(string),typeof(string),typeof(string))){
				Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				System.String a3;
				checkType(l,4,out a3);
				self.SetButtonText(a1,a2,a3);
				pushValue(l,true);
				return 1;
			}
			else if(matchType(l,argc,2,typeof(UnityEngine.GameObject),typeof(string),typeof(string))){
				Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
				UnityEngine.GameObject a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				System.String a3;
				checkType(l,4,out a3);
				self.SetButtonText(a1,a2,a3);
				pushValue(l,true);
				return 1;
			}
			else if(argc==5){
				Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
				UnityEngine.GameObject a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				UnityEngine.Color a3;
				checkType(l,4,out a3);
				System.String a4;
				checkType(l,5,out a4);
				self.SetButtonText(a1,a2,a3,a4);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int SetButtonImage(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(UnityEngine.GameObject),typeof(UnityEngine.Sprite))){
				Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
				UnityEngine.GameObject a1;
				checkType(l,2,out a1);
				UnityEngine.Sprite a2;
				checkType(l,3,out a2);
				self.SetButtonImage(a1,a2);
				pushValue(l,true);
				return 1;
			}
			else if(matchType(l,argc,2,typeof(string),typeof(UnityEngine.Sprite))){
				Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				UnityEngine.Sprite a2;
				checkType(l,3,out a2);
				self.SetButtonImage(a1,a2);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int SetButtonColor(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(UnityEngine.GameObject),typeof(UnityEngine.Color))){
				Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
				UnityEngine.GameObject a1;
				checkType(l,2,out a1);
				UnityEngine.Color a2;
				checkType(l,3,out a2);
				self.SetButtonColor(a1,a2);
				pushValue(l,true);
				return 1;
			}
			else if(matchType(l,argc,2,typeof(string),typeof(UnityEngine.Color))){
				Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				UnityEngine.Color a2;
				checkType(l,3,out a2);
				self.SetButtonColor(a1,a2);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetButtonColor(IntPtr l) {
		try {
			Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
			UnityEngine.GameObject a1;
			checkType(l,2,out a1);
			var ret=self.GetButtonColor(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int SetElemScale(IntPtr l) {
		try {
			Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
			UnityEngine.GameObject a1;
			checkType(l,2,out a1);
			System.Single a2;
			checkType(l,3,out a2);
			self.SetElemScale(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int SetButtonEnable(IntPtr l) {
		try {
			Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.SetButtonEnable(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int OnPointerClick(IntPtr l) {
		try {
			Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
			UnityEngine.EventSystems.PointerEventData a1;
			checkType(l,2,out a1);
			self.OnPointerClick(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int OnButtonClick(IntPtr l) {
		try {
			Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.OnButtonClick(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int OnPointerUp(IntPtr l) {
		try {
			Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
			UnityEngine.EventSystems.PointerEventData a1;
			checkType(l,2,out a1);
			self.OnPointerUp(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int OnPointerDown(IntPtr l) {
		try {
			Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
			UnityEngine.EventSystems.PointerEventData a1;
			checkType(l,2,out a1);
			self.OnPointerDown(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int OnBeginDrag(IntPtr l) {
		try {
			Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
			UnityEngine.EventSystems.PointerEventData a1;
			checkType(l,2,out a1);
			self.OnBeginDrag(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int OnDrag(IntPtr l) {
		try {
			Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
			UnityEngine.EventSystems.PointerEventData a1;
			checkType(l,2,out a1);
			self.OnDrag(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int OnEndDrag(IntPtr l) {
		try {
			Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
			UnityEngine.EventSystems.PointerEventData a1;
			checkType(l,2,out a1);
			self.OnEndDrag(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int OpenWindow_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			System.Boolean a2;
			checkType(l,2,out a2);
			Core.Lua.LuaUIWindow.OpenWindow(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int CloseWindow_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			System.Boolean a2;
			checkType(l,2,out a2);
			Core.Lua.LuaUIWindow.CloseWindow(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_UIName(IntPtr l) {
		try {
			Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.UIName);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_UIName(IntPtr l) {
		try {
			Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.UIName=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_SelfTable(IntPtr l) {
		try {
			Core.Lua.LuaUIWindow self=(Core.Lua.LuaUIWindow)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.SelfTable);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"Core.Lua.LuaUIWindow");
		addMember(l,CallLuaFunction);
		addMember(l,Awake);
		addMember(l,Init);
		addMember(l,UnInit);
		addMember(l,OnShow);
		addMember(l,OnHide);
		addMember(l,Update);
		addMember(l,RegisterWindowElemEvent);
		addMember(l,RegisterButtonClickEvent);
		addMember(l,SetImageBackGround);
		addMember(l,SetText);
		addMember(l,SetTextColor);
		addMember(l,GetText);
		addMember(l,SetButtonText);
		addMember(l,SetButtonImage);
		addMember(l,SetButtonColor);
		addMember(l,GetButtonColor);
		addMember(l,SetElemScale);
		addMember(l,SetButtonEnable);
		addMember(l,OnPointerClick);
		addMember(l,OnButtonClick);
		addMember(l,OnPointerUp);
		addMember(l,OnPointerDown);
		addMember(l,OnBeginDrag);
		addMember(l,OnDrag);
		addMember(l,OnEndDrag);
		addMember(l,OpenWindow_s);
		addMember(l,CloseWindow_s);
		addMember(l,"UIName",get_UIName,set_UIName,true);
		addMember(l,"SelfTable",get_SelfTable,null,true);
		createTypeMetatable(l,constructor, typeof(Core.Lua.LuaUIWindow),typeof(Core.UI.UIWindowBase));
	}
}
