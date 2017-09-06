﻿using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_VR_InputTracking : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetLocalPosition_s(IntPtr l) {
		try {
			UnityEngine.VR.VRNode a1;
			checkEnum(l,1,out a1);
			var ret=UnityEngine.VR.InputTracking.GetLocalPosition(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetLocalRotation_s(IntPtr l) {
		try {
			UnityEngine.VR.VRNode a1;
			checkEnum(l,1,out a1);
			var ret=UnityEngine.VR.InputTracking.GetLocalRotation(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Recenter_s(IntPtr l) {
		try {
			UnityEngine.VR.InputTracking.Recenter();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetNodeName_s(IntPtr l) {
		try {
			System.UInt64 a1;
			checkType(l,1,out a1);
			var ret=UnityEngine.VR.InputTracking.GetNodeName(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetNodeStates_s(IntPtr l) {
		try {
			System.Collections.Generic.List<UnityEngine.VR.VRNodeState> a1;
			checkType(l,1,out a1);
			UnityEngine.VR.InputTracking.GetNodeStates(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_disablePositionalTracking(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.VR.InputTracking.disablePositionalTracking);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_disablePositionalTracking(IntPtr l) {
		try {
			bool v;
			checkType(l,2,out v);
			UnityEngine.VR.InputTracking.disablePositionalTracking=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.VR.InputTracking");
		addMember(l,GetLocalPosition_s);
		addMember(l,GetLocalRotation_s);
		addMember(l,Recenter_s);
		addMember(l,GetNodeName_s);
		addMember(l,GetNodeStates_s);
		addMember(l,"disablePositionalTracking",get_disablePositionalTracking,set_disablePositionalTracking,false);
		createTypeMetatable(l,null, typeof(UnityEngine.VR.InputTracking));
	}
}
