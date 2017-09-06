using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_VR_VRNodeState : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int constructor(IntPtr l) {
		try {
			UnityEngine.VR.VRNodeState o;
			o=new UnityEngine.VR.VRNodeState();
			pushValue(l,true);
			pushValue(l,o);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int TryGetPosition(IntPtr l) {
		try {
			UnityEngine.VR.VRNodeState self;
			checkValueType(l,1,out self);
			UnityEngine.Vector3 a1;
			var ret=self.TryGetPosition(out a1);
			pushValue(l,true);
			pushValue(l,ret);
			pushValue(l,a1);
			return 3;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int TryGetRotation(IntPtr l) {
		try {
			UnityEngine.VR.VRNodeState self;
			checkValueType(l,1,out self);
			UnityEngine.Quaternion a1;
			var ret=self.TryGetRotation(out a1);
			pushValue(l,true);
			pushValue(l,ret);
			pushValue(l,a1);
			return 3;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int TryGetVelocity(IntPtr l) {
		try {
			UnityEngine.VR.VRNodeState self;
			checkValueType(l,1,out self);
			UnityEngine.Vector3 a1;
			var ret=self.TryGetVelocity(out a1);
			pushValue(l,true);
			pushValue(l,ret);
			pushValue(l,a1);
			return 3;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int TryGetAcceleration(IntPtr l) {
		try {
			UnityEngine.VR.VRNodeState self;
			checkValueType(l,1,out self);
			UnityEngine.Vector3 a1;
			var ret=self.TryGetAcceleration(out a1);
			pushValue(l,true);
			pushValue(l,ret);
			pushValue(l,a1);
			return 3;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_uniqueID(IntPtr l) {
		try {
			UnityEngine.VR.VRNodeState self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.uniqueID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_uniqueID(IntPtr l) {
		try {
			UnityEngine.VR.VRNodeState self;
			checkValueType(l,1,out self);
			System.UInt64 v;
			checkType(l,2,out v);
			self.uniqueID=v;
			setBack(l,self);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_nodeType(IntPtr l) {
		try {
			UnityEngine.VR.VRNodeState self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushEnum(l,(int)self.nodeType);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_nodeType(IntPtr l) {
		try {
			UnityEngine.VR.VRNodeState self;
			checkValueType(l,1,out self);
			UnityEngine.VR.VRNode v;
			checkEnum(l,2,out v);
			self.nodeType=v;
			setBack(l,self);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_tracked(IntPtr l) {
		try {
			UnityEngine.VR.VRNodeState self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.tracked);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_tracked(IntPtr l) {
		try {
			UnityEngine.VR.VRNodeState self;
			checkValueType(l,1,out self);
			bool v;
			checkType(l,2,out v);
			self.tracked=v;
			setBack(l,self);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_position(IntPtr l) {
		try {
			UnityEngine.VR.VRNodeState self;
			checkValueType(l,1,out self);
			UnityEngine.Vector3 v;
			checkType(l,2,out v);
			self.position=v;
			setBack(l,self);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_rotation(IntPtr l) {
		try {
			UnityEngine.VR.VRNodeState self;
			checkValueType(l,1,out self);
			UnityEngine.Quaternion v;
			checkType(l,2,out v);
			self.rotation=v;
			setBack(l,self);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_velocity(IntPtr l) {
		try {
			UnityEngine.VR.VRNodeState self;
			checkValueType(l,1,out self);
			UnityEngine.Vector3 v;
			checkType(l,2,out v);
			self.velocity=v;
			setBack(l,self);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_acceleration(IntPtr l) {
		try {
			UnityEngine.VR.VRNodeState self;
			checkValueType(l,1,out self);
			UnityEngine.Vector3 v;
			checkType(l,2,out v);
			self.acceleration=v;
			setBack(l,self);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.VR.VRNodeState");
		addMember(l,TryGetPosition);
		addMember(l,TryGetRotation);
		addMember(l,TryGetVelocity);
		addMember(l,TryGetAcceleration);
		addMember(l,"uniqueID",get_uniqueID,set_uniqueID,true);
		addMember(l,"nodeType",get_nodeType,set_nodeType,true);
		addMember(l,"tracked",get_tracked,set_tracked,true);
		addMember(l,"position",null,set_position,true);
		addMember(l,"rotation",null,set_rotation,true);
		addMember(l,"velocity",null,set_velocity,true);
		addMember(l,"acceleration",null,set_acceleration,true);
		createTypeMetatable(l,constructor, typeof(UnityEngine.VR.VRNodeState),typeof(System.ValueType));
	}
}
