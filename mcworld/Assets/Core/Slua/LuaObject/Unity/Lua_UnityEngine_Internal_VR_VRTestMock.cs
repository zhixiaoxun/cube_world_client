﻿using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_Internal_VR_VRTestMock : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Reset_s(IntPtr l) {
		try {
			UnityEngine.Internal.VR.VRTestMock.Reset();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int AddTrackedDevice_s(IntPtr l) {
		try {
			UnityEngine.VR.VRNode a1;
			checkEnum(l,1,out a1);
			UnityEngine.Internal.VR.VRTestMock.AddTrackedDevice(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int UpdateTrackedDevice_s(IntPtr l) {
		try {
			UnityEngine.VR.VRNode a1;
			checkEnum(l,1,out a1);
			UnityEngine.Vector3 a2;
			checkType(l,2,out a2);
			UnityEngine.Quaternion a3;
			checkType(l,3,out a3);
			UnityEngine.Internal.VR.VRTestMock.UpdateTrackedDevice(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int UpdateLeftEye_s(IntPtr l) {
		try {
			UnityEngine.Vector3 a1;
			checkType(l,1,out a1);
			UnityEngine.Quaternion a2;
			checkType(l,2,out a2);
			UnityEngine.Internal.VR.VRTestMock.UpdateLeftEye(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int UpdateRightEye_s(IntPtr l) {
		try {
			UnityEngine.Vector3 a1;
			checkType(l,1,out a1);
			UnityEngine.Quaternion a2;
			checkType(l,2,out a2);
			UnityEngine.Internal.VR.VRTestMock.UpdateRightEye(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int UpdateCenterEye_s(IntPtr l) {
		try {
			UnityEngine.Vector3 a1;
			checkType(l,1,out a1);
			UnityEngine.Quaternion a2;
			checkType(l,2,out a2);
			UnityEngine.Internal.VR.VRTestMock.UpdateCenterEye(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int UpdateHead_s(IntPtr l) {
		try {
			UnityEngine.Vector3 a1;
			checkType(l,1,out a1);
			UnityEngine.Quaternion a2;
			checkType(l,2,out a2);
			UnityEngine.Internal.VR.VRTestMock.UpdateHead(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int UpdateLeftHand_s(IntPtr l) {
		try {
			UnityEngine.Vector3 a1;
			checkType(l,1,out a1);
			UnityEngine.Quaternion a2;
			checkType(l,2,out a2);
			UnityEngine.Internal.VR.VRTestMock.UpdateLeftHand(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int UpdateRightHand_s(IntPtr l) {
		try {
			UnityEngine.Vector3 a1;
			checkType(l,1,out a1);
			UnityEngine.Quaternion a2;
			checkType(l,2,out a2);
			UnityEngine.Internal.VR.VRTestMock.UpdateRightHand(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int AddController_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			UnityEngine.Internal.VR.VRTestMock.AddController(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int UpdateControllerAxis_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			System.Int32 a2;
			checkType(l,2,out a2);
			System.Single a3;
			checkType(l,3,out a3);
			UnityEngine.Internal.VR.VRTestMock.UpdateControllerAxis(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int UpdateControllerButton_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			System.Int32 a2;
			checkType(l,2,out a2);
			System.Boolean a3;
			checkType(l,3,out a3);
			UnityEngine.Internal.VR.VRTestMock.UpdateControllerButton(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.Internal.VR.VRTestMock");
		addMember(l,Reset_s);
		addMember(l,AddTrackedDevice_s);
		addMember(l,UpdateTrackedDevice_s);
		addMember(l,UpdateLeftEye_s);
		addMember(l,UpdateRightEye_s);
		addMember(l,UpdateCenterEye_s);
		addMember(l,UpdateHead_s);
		addMember(l,UpdateLeftHand_s);
		addMember(l,UpdateRightHand_s);
		addMember(l,AddController_s);
		addMember(l,UpdateControllerAxis_s);
		addMember(l,UpdateControllerButton_s);
		createTypeMetatable(l,null, typeof(UnityEngine.Internal.VR.VRTestMock));
	}
}
