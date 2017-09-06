﻿using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_UISystemProfilerApi : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int constructor(IntPtr l) {
		try {
			UnityEngine.UISystemProfilerApi o;
			o=new UnityEngine.UISystemProfilerApi();
			pushValue(l,true);
			pushValue(l,o);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int BeginSample_s(IntPtr l) {
		try {
			UnityEngine.UISystemProfilerApi.SampleType a1;
			checkEnum(l,1,out a1);
			UnityEngine.UISystemProfilerApi.BeginSample(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int EndSample_s(IntPtr l) {
		try {
			UnityEngine.UISystemProfilerApi.SampleType a1;
			checkEnum(l,1,out a1);
			UnityEngine.UISystemProfilerApi.EndSample(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int AddMarker_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			UnityEngine.Object a2;
			checkType(l,2,out a2);
			UnityEngine.UISystemProfilerApi.AddMarker(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.UISystemProfilerApi");
		addMember(l,BeginSample_s);
		addMember(l,EndSample_s);
		addMember(l,AddMarker_s);
		createTypeMetatable(l,constructor, typeof(UnityEngine.UISystemProfilerApi));
	}
}
