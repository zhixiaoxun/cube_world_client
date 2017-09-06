﻿using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_AI_NavMeshDataInstance : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int constructor(IntPtr l) {
		try {
			UnityEngine.AI.NavMeshDataInstance o;
			o=new UnityEngine.AI.NavMeshDataInstance();
			pushValue(l,true);
			pushValue(l,o);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Remove(IntPtr l) {
		try {
			UnityEngine.AI.NavMeshDataInstance self;
			checkValueType(l,1,out self);
			self.Remove();
			pushValue(l,true);
			setBack(l,self);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_valid(IntPtr l) {
		try {
			UnityEngine.AI.NavMeshDataInstance self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.valid);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_owner(IntPtr l) {
		try {
			UnityEngine.AI.NavMeshDataInstance self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.owner);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_owner(IntPtr l) {
		try {
			UnityEngine.AI.NavMeshDataInstance self;
			checkValueType(l,1,out self);
			UnityEngine.Object v;
			checkType(l,2,out v);
			self.owner=v;
			setBack(l,self);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.AI.NavMeshDataInstance");
		addMember(l,Remove);
		addMember(l,"valid",get_valid,null,true);
		addMember(l,"owner",get_owner,set_owner,true);
		createTypeMetatable(l,constructor, typeof(UnityEngine.AI.NavMeshDataInstance),typeof(System.ValueType));
	}
}
