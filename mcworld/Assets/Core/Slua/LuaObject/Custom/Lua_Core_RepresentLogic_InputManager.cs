using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_Core_RepresentLogic_InputManager : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_MobileInput(IntPtr l) {
		try {
			Core.RepresentLogic.InputManager self=(Core.RepresentLogic.InputManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MobileInput);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_MobileInput(IntPtr l) {
		try {
			Core.RepresentLogic.InputManager self=(Core.RepresentLogic.InputManager)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.MobileInput=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_SwipeVec(IntPtr l) {
		try {
			Core.RepresentLogic.InputManager self=(Core.RepresentLogic.InputManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.SwipeVec);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_JoyStickVec(IntPtr l) {
		try {
			Core.RepresentLogic.InputManager self=(Core.RepresentLogic.InputManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.JoyStickVec);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_JoyStickVec(IntPtr l) {
		try {
			Core.RepresentLogic.InputManager self=(Core.RepresentLogic.InputManager)checkSelf(l);
			UnityEngine.Vector2 v;
			checkType(l,2,out v);
			self.JoyStickVec=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"Core.RepresentLogic.InputManager");
		addMember(l,"MobileInput",get_MobileInput,set_MobileInput,true);
		addMember(l,"SwipeVec",get_SwipeVec,null,true);
		addMember(l,"JoyStickVec",get_JoyStickVec,set_JoyStickVec,true);
		createTypeMetatable(l,null, typeof(Core.RepresentLogic.InputManager),typeof(UnityEngine.MonoBehaviour));
	}
}
