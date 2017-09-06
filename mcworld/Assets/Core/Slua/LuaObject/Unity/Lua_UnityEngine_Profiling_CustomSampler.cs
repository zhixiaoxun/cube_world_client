﻿using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_Profiling_CustomSampler : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Begin(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==1){
				UnityEngine.Profiling.CustomSampler self=(UnityEngine.Profiling.CustomSampler)checkSelf(l);
				self.Begin();
				pushValue(l,true);
				return 1;
			}
			else if(argc==2){
				UnityEngine.Profiling.CustomSampler self=(UnityEngine.Profiling.CustomSampler)checkSelf(l);
				UnityEngine.Object a1;
				checkType(l,2,out a1);
				self.Begin(a1);
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
	static public int End(IntPtr l) {
		try {
			UnityEngine.Profiling.CustomSampler self=(UnityEngine.Profiling.CustomSampler)checkSelf(l);
			self.End();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Create_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=UnityEngine.Profiling.CustomSampler.Create(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.Profiling.CustomSampler");
		addMember(l,Begin);
		addMember(l,End);
		addMember(l,Create_s);
		createTypeMetatable(l,null, typeof(UnityEngine.Profiling.CustomSampler),typeof(UnityEngine.Profiling.Sampler));
	}
}
