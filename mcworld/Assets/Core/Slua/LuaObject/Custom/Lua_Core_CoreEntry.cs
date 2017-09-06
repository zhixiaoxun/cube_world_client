using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_Core_CoreEntry : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int StartGame(IntPtr l) {
		try {
			Core.CoreEntry self=(Core.CoreEntry)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.StartGame(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Invoke_s(IntPtr l) {
		try {
			System.Action a1;
			LuaDelegation.checkDelegate(l,1,out a1);
			Core.CoreEntry.Invoke(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_Instance(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,Core.CoreEntry.Instance);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get__LifeCycle(IntPtr l) {
		try {
			Core.CoreEntry self=(Core.CoreEntry)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self._LifeCycle);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_InitFinished(IntPtr l) {
		try {
			Core.CoreEntry self=(Core.CoreEntry)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.InitFinished);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"Core.CoreEntry");
		addMember(l,StartGame);
		addMember(l,Invoke_s);
		addMember(l,"Instance",get_Instance,null,false);
		addMember(l,"_LifeCycle",get__LifeCycle,null,true);
		addMember(l,"InitFinished",get_InitFinished,null,true);
		createTypeMetatable(l,null, typeof(Core.CoreEntry),typeof(UnityEngine.MonoBehaviour));
	}
}
