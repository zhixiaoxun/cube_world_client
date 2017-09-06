using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_Core_GameLogic_ActiveObjects_ActiveObject : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int constructor(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.ActiveObject o;
			Core.GameLogic.World a1;
			checkType(l,2,out a1);
			o=new Core.GameLogic.ActiveObjects.ActiveObject(a1);
			pushValue(l,true);
			pushValue(l,o);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int UnInit(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.ActiveObject self=(Core.GameLogic.ActiveObjects.ActiveObject)checkSelf(l);
			self.UnInit();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Active(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.ActiveObject self=(Core.GameLogic.ActiveObjects.ActiveObject)checkSelf(l);
			self.Active();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get__IsReady(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.ActiveObject self=(Core.GameLogic.ActiveObjects.ActiveObject)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self._IsReady);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get__ID(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.ActiveObject self=(Core.GameLogic.ActiveObjects.ActiveObject)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self._ID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set__ID(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.ActiveObject self=(Core.GameLogic.ActiveObjects.ActiveObject)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self._ID=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}

	static public void reg(IntPtr l) {
		getTypeTable(l,"Core.GameLogic.ActiveObjects.ActiveObject");
		addMember(l,UnInit);
		addMember(l,Active);
		addMember(l,"_IsReady",get__IsReady,null,true);
		addMember(l,"_ID",get__ID,set__ID,true);
		createTypeMetatable(l,constructor, typeof(Core.GameLogic.ActiveObjects.ActiveObject));
	}
}
