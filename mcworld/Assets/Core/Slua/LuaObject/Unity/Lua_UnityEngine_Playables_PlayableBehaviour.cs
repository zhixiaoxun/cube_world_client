using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_Playables_PlayableBehaviour : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int OnGraphStart(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableBehaviour self=(UnityEngine.Playables.PlayableBehaviour)checkSelf(l);
			UnityEngine.Playables.Playable a1;
			checkValueType(l,2,out a1);
			self.OnGraphStart(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int OnGraphStop(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableBehaviour self=(UnityEngine.Playables.PlayableBehaviour)checkSelf(l);
			UnityEngine.Playables.Playable a1;
			checkValueType(l,2,out a1);
			self.OnGraphStop(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int OnPlayableCreate(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableBehaviour self=(UnityEngine.Playables.PlayableBehaviour)checkSelf(l);
			UnityEngine.Playables.Playable a1;
			checkValueType(l,2,out a1);
			self.OnPlayableCreate(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int OnPlayableDestroy(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableBehaviour self=(UnityEngine.Playables.PlayableBehaviour)checkSelf(l);
			UnityEngine.Playables.Playable a1;
			checkValueType(l,2,out a1);
			self.OnPlayableDestroy(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int OnBehaviourPlay(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableBehaviour self=(UnityEngine.Playables.PlayableBehaviour)checkSelf(l);
			UnityEngine.Playables.Playable a1;
			checkValueType(l,2,out a1);
			UnityEngine.Playables.FrameData a2;
			checkValueType(l,3,out a2);
			self.OnBehaviourPlay(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int OnBehaviourPause(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableBehaviour self=(UnityEngine.Playables.PlayableBehaviour)checkSelf(l);
			UnityEngine.Playables.Playable a1;
			checkValueType(l,2,out a1);
			UnityEngine.Playables.FrameData a2;
			checkValueType(l,3,out a2);
			self.OnBehaviourPause(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int PrepareFrame(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableBehaviour self=(UnityEngine.Playables.PlayableBehaviour)checkSelf(l);
			UnityEngine.Playables.Playable a1;
			checkValueType(l,2,out a1);
			UnityEngine.Playables.FrameData a2;
			checkValueType(l,3,out a2);
			self.PrepareFrame(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int ProcessFrame(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableBehaviour self=(UnityEngine.Playables.PlayableBehaviour)checkSelf(l);
			UnityEngine.Playables.Playable a1;
			checkValueType(l,2,out a1);
			UnityEngine.Playables.FrameData a2;
			checkValueType(l,3,out a2);
			System.Object a3;
			checkType(l,4,out a3);
			self.ProcessFrame(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.Playables.PlayableBehaviour");
		addMember(l,OnGraphStart);
		addMember(l,OnGraphStop);
		addMember(l,OnPlayableCreate);
		addMember(l,OnPlayableDestroy);
		addMember(l,OnBehaviourPlay);
		addMember(l,OnBehaviourPause);
		addMember(l,PrepareFrame);
		addMember(l,ProcessFrame);
		createTypeMetatable(l,null, typeof(UnityEngine.Playables.PlayableBehaviour));
	}
}
