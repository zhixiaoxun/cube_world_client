﻿using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_Analytics_PerformanceReporting : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_enabled(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Analytics.PerformanceReporting.enabled);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_enabled(IntPtr l) {
		try {
			bool v;
			checkType(l,2,out v);
			UnityEngine.Analytics.PerformanceReporting.enabled=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.Analytics.PerformanceReporting");
		addMember(l,"enabled",get_enabled,set_enabled,false);
		createTypeMetatable(l,null, typeof(UnityEngine.Analytics.PerformanceReporting));
	}
}
