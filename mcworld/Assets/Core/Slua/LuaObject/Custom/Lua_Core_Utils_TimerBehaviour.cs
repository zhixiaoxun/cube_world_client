using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_Core_Utils_TimerBehaviour : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int StartTimer(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==1){
				Core.Utils.TimerBehaviour self=(Core.Utils.TimerBehaviour)checkSelf(l);
				self.StartTimer();
				pushValue(l,true);
				return 1;
			}
			else if(argc==4){
				Core.Utils.TimerBehaviour self=(Core.Utils.TimerBehaviour)checkSelf(l);
				System.UInt32 a1;
				checkType(l,2,out a1);
				System.Action<System.UInt32> a2;
				LuaDelegation.checkDelegate(l,3,out a2);
				System.Action a3;
				LuaDelegation.checkDelegate(l,4,out a3);
				self.StartTimer(a1,a2,a3);
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
	static public int GetSeconds(IntPtr l) {
		try {
			Core.Utils.TimerBehaviour self=(Core.Utils.TimerBehaviour)checkSelf(l);
			var ret=self.GetSeconds();
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int StopTimer(IntPtr l) {
		try {
			Core.Utils.TimerBehaviour self=(Core.Utils.TimerBehaviour)checkSelf(l);
			self.StopTimer();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetCurrentTime(IntPtr l) {
		try {
			Core.Utils.TimerBehaviour self=(Core.Utils.TimerBehaviour)checkSelf(l);
			var ret=self.GetCurrentTime();
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetCurrentTimeString(IntPtr l) {
		try {
			Core.Utils.TimerBehaviour self=(Core.Utils.TimerBehaviour)checkSelf(l);
			var ret=self.GetCurrentTimeString();
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int IsTimerRunning(IntPtr l) {
		try {
			Core.Utils.TimerBehaviour self=(Core.Utils.TimerBehaviour)checkSelf(l);
			var ret=self.IsTimerRunning();
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int OnDisable(IntPtr l) {
		try {
			Core.Utils.TimerBehaviour self=(Core.Utils.TimerBehaviour)checkSelf(l);
			self.OnDisable();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"Core.Utils.TimerBehaviour");
		addMember(l,StartTimer);
		addMember(l,GetSeconds);
		addMember(l,StopTimer);
		addMember(l,GetCurrentTime);
		addMember(l,GetCurrentTimeString);
		addMember(l,IsTimerRunning);
		addMember(l,OnDisable);
		createTypeMetatable(l,null, typeof(Core.Utils.TimerBehaviour),typeof(UnityEngine.MonoBehaviour));
	}
}
