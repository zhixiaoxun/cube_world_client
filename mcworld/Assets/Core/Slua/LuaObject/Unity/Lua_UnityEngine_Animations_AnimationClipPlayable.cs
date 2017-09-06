﻿using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_Animations_AnimationClipPlayable : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int constructor(IntPtr l) {
		try {
			UnityEngine.Animations.AnimationClipPlayable o;
			o=new UnityEngine.Animations.AnimationClipPlayable();
			pushValue(l,true);
			pushValue(l,o);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetHandle(IntPtr l) {
		try {
			UnityEngine.Animations.AnimationClipPlayable self;
			checkValueType(l,1,out self);
			var ret=self.GetHandle();
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetAnimationClip(IntPtr l) {
		try {
			UnityEngine.Animations.AnimationClipPlayable self;
			checkValueType(l,1,out self);
			var ret=self.GetAnimationClip();
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetApplyFootIK(IntPtr l) {
		try {
			UnityEngine.Animations.AnimationClipPlayable self;
			checkValueType(l,1,out self);
			var ret=self.GetApplyFootIK();
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int SetApplyFootIK(IntPtr l) {
		try {
			UnityEngine.Animations.AnimationClipPlayable self;
			checkValueType(l,1,out self);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.SetApplyFootIK(a1);
			pushValue(l,true);
			setBack(l,self);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Create_s(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableGraph a1;
			checkValueType(l,1,out a1);
			UnityEngine.AnimationClip a2;
			checkType(l,2,out a2);
			var ret=UnityEngine.Animations.AnimationClipPlayable.Create(a1,a2);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.Animations.AnimationClipPlayable");
		addMember(l,GetHandle);
		addMember(l,GetAnimationClip);
		addMember(l,GetApplyFootIK);
		addMember(l,SetApplyFootIK);
		addMember(l,Create_s);
		createTypeMetatable(l,constructor, typeof(UnityEngine.Animations.AnimationClipPlayable),typeof(System.ValueType));
	}
}
