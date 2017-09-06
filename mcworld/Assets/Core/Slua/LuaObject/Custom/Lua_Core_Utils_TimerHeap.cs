using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_Core_Utils_TimerHeap : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int AddTimer_s(IntPtr l) {
		try {
			System.UInt32 a1;
			checkType(l,1,out a1);
			System.Int32 a2;
			checkType(l,2,out a2);
			System.Action a3;
			LuaDelegation.checkDelegate(l,3,out a3);
			var ret=Core.Utils.TimerHeap.AddTimer(a1,a2,a3);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int LuaAddTimer_s(IntPtr l) {
		try {
			System.UInt32 a1;
			checkType(l,1,out a1);
			System.Int32 a2;
			checkType(l,2,out a2);
			System.Action<SLua.LuaTable> a3;
			LuaDelegation.checkDelegate(l,3,out a3);
			SLua.LuaTable a4;
			checkType(l,4,out a4);
			var ret=Core.Utils.TimerHeap.LuaAddTimer(a1,a2,a3,a4);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int DelTimer_s(IntPtr l) {
		try {
			System.UInt32 a1;
			checkType(l,1,out a1);
			Core.Utils.TimerHeap.DelTimer(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Tick_s(IntPtr l) {
		try {
			Core.Utils.TimerHeap.Tick();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Reset_s(IntPtr l) {
		try {
			Core.Utils.TimerHeap.Reset();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"Core.Utils.TimerHeap");
		addMember(l,AddTimer_s);
		addMember(l,LuaAddTimer_s);
		addMember(l,DelTimer_s);
		addMember(l,Tick_s);
		addMember(l,Reset_s);
		createTypeMetatable(l,null, typeof(Core.Utils.TimerHeap));
	}
}
