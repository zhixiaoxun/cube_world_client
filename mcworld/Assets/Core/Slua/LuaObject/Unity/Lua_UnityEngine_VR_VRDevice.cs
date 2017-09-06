﻿using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_VR_VRDevice : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetTrackingSpaceType_s(IntPtr l) {
		try {
			var ret=UnityEngine.VR.VRDevice.GetTrackingSpaceType();
			pushValue(l,true);
			pushEnum(l,(int)ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int SetTrackingSpaceType_s(IntPtr l) {
		try {
			UnityEngine.VR.TrackingSpaceType a1;
			checkEnum(l,1,out a1);
			var ret=UnityEngine.VR.VRDevice.SetTrackingSpaceType(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetNativePtr_s(IntPtr l) {
		try {
			var ret=UnityEngine.VR.VRDevice.GetNativePtr();
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int DisableAutoVRCameraTracking_s(IntPtr l) {
		try {
			UnityEngine.Camera a1;
			checkType(l,1,out a1);
			System.Boolean a2;
			checkType(l,2,out a2);
			UnityEngine.VR.VRDevice.DisableAutoVRCameraTracking(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_isPresent(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.VR.VRDevice.isPresent);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_model(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.VR.VRDevice.model);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_refreshRate(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.VR.VRDevice.refreshRate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.VR.VRDevice");
		addMember(l,GetTrackingSpaceType_s);
		addMember(l,SetTrackingSpaceType_s);
		addMember(l,GetNativePtr_s);
		addMember(l,DisableAutoVRCameraTracking_s);
		addMember(l,"isPresent",get_isPresent,null,false);
		addMember(l,"model",get_model,null,false);
		addMember(l,"refreshRate",get_refreshRate,null,false);
		createTypeMetatable(l,null, typeof(UnityEngine.VR.VRDevice));
	}
}
