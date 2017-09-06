using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_Playables_PlayableDirector : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int constructor(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableDirector o;
			o=new UnityEngine.Playables.PlayableDirector();
			pushValue(l,true);
			pushValue(l,o);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Evaluate(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableDirector self=(UnityEngine.Playables.PlayableDirector)checkSelf(l);
			self.Evaluate();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int DeferredEvaluate(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableDirector self=(UnityEngine.Playables.PlayableDirector)checkSelf(l);
			self.DeferredEvaluate();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Play(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==1){
				UnityEngine.Playables.PlayableDirector self=(UnityEngine.Playables.PlayableDirector)checkSelf(l);
				self.Play();
				pushValue(l,true);
				return 1;
			}
			else if(argc==2){
				UnityEngine.Playables.PlayableDirector self=(UnityEngine.Playables.PlayableDirector)checkSelf(l);
				UnityEngine.Playables.PlayableAsset a1;
				checkType(l,2,out a1);
				self.Play(a1);
				pushValue(l,true);
				return 1;
			}
			else if(argc==3){
				UnityEngine.Playables.PlayableDirector self=(UnityEngine.Playables.PlayableDirector)checkSelf(l);
				UnityEngine.Playables.PlayableAsset a1;
				checkType(l,2,out a1);
				UnityEngine.Playables.DirectorWrapMode a2;
				checkEnum(l,3,out a2);
				self.Play(a1,a2);
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
	static public int Stop(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableDirector self=(UnityEngine.Playables.PlayableDirector)checkSelf(l);
			self.Stop();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Pause(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableDirector self=(UnityEngine.Playables.PlayableDirector)checkSelf(l);
			self.Pause();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Resume(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableDirector self=(UnityEngine.Playables.PlayableDirector)checkSelf(l);
			self.Resume();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int SetReferenceValue(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableDirector self=(UnityEngine.Playables.PlayableDirector)checkSelf(l);
			UnityEngine.PropertyName a1;
			checkValueType(l,2,out a1);
			UnityEngine.Object a2;
			checkType(l,3,out a2);
			self.SetReferenceValue(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetReferenceValue(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableDirector self=(UnityEngine.Playables.PlayableDirector)checkSelf(l);
			UnityEngine.PropertyName a1;
			checkValueType(l,2,out a1);
			System.Boolean a2;
			var ret=self.GetReferenceValue(a1,out a2);
			pushValue(l,true);
			pushValue(l,ret);
			pushValue(l,a2);
			return 3;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int ClearReferenceValue(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableDirector self=(UnityEngine.Playables.PlayableDirector)checkSelf(l);
			UnityEngine.PropertyName a1;
			checkValueType(l,2,out a1);
			self.ClearReferenceValue(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int SetGenericBinding(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableDirector self=(UnityEngine.Playables.PlayableDirector)checkSelf(l);
			UnityEngine.Object a1;
			checkType(l,2,out a1);
			UnityEngine.Object a2;
			checkType(l,3,out a2);
			self.SetGenericBinding(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetGenericBinding(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableDirector self=(UnityEngine.Playables.PlayableDirector)checkSelf(l);
			UnityEngine.Object a1;
			checkType(l,2,out a1);
			var ret=self.GetGenericBinding(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_state(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableDirector self=(UnityEngine.Playables.PlayableDirector)checkSelf(l);
			pushValue(l,true);
			pushEnum(l,(int)self.state);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_playableAsset(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableDirector self=(UnityEngine.Playables.PlayableDirector)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.playableAsset);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_playableAsset(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableDirector self=(UnityEngine.Playables.PlayableDirector)checkSelf(l);
			UnityEngine.Playables.PlayableAsset v;
			checkType(l,2,out v);
			self.playableAsset=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_extrapolationMode(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableDirector self=(UnityEngine.Playables.PlayableDirector)checkSelf(l);
			pushValue(l,true);
			pushEnum(l,(int)self.extrapolationMode);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_extrapolationMode(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableDirector self=(UnityEngine.Playables.PlayableDirector)checkSelf(l);
			UnityEngine.Playables.DirectorWrapMode v;
			checkEnum(l,2,out v);
			self.extrapolationMode=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_timeUpdateMode(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableDirector self=(UnityEngine.Playables.PlayableDirector)checkSelf(l);
			pushValue(l,true);
			pushEnum(l,(int)self.timeUpdateMode);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_timeUpdateMode(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableDirector self=(UnityEngine.Playables.PlayableDirector)checkSelf(l);
			UnityEngine.Playables.DirectorUpdateMode v;
			checkEnum(l,2,out v);
			self.timeUpdateMode=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_time(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableDirector self=(UnityEngine.Playables.PlayableDirector)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.time);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_time(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableDirector self=(UnityEngine.Playables.PlayableDirector)checkSelf(l);
			double v;
			checkType(l,2,out v);
			self.time=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_initialTime(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableDirector self=(UnityEngine.Playables.PlayableDirector)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.initialTime);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_initialTime(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableDirector self=(UnityEngine.Playables.PlayableDirector)checkSelf(l);
			double v;
			checkType(l,2,out v);
			self.initialTime=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_duration(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableDirector self=(UnityEngine.Playables.PlayableDirector)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.duration);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_playableGraph(IntPtr l) {
		try {
			UnityEngine.Playables.PlayableDirector self=(UnityEngine.Playables.PlayableDirector)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.playableGraph);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.Playables.PlayableDirector");
		addMember(l,Evaluate);
		addMember(l,DeferredEvaluate);
		addMember(l,Play);
		addMember(l,Stop);
		addMember(l,Pause);
		addMember(l,Resume);
		addMember(l,SetReferenceValue);
		addMember(l,GetReferenceValue);
		addMember(l,ClearReferenceValue);
		addMember(l,SetGenericBinding);
		addMember(l,GetGenericBinding);
		addMember(l,"state",get_state,null,true);
		addMember(l,"playableAsset",get_playableAsset,set_playableAsset,true);
		addMember(l,"extrapolationMode",get_extrapolationMode,set_extrapolationMode,true);
		addMember(l,"timeUpdateMode",get_timeUpdateMode,set_timeUpdateMode,true);
		addMember(l,"time",get_time,set_time,true);
		addMember(l,"initialTime",get_initialTime,set_initialTime,true);
		addMember(l,"duration",get_duration,null,true);
		addMember(l,"playableGraph",get_playableGraph,null,true);
		createTypeMetatable(l,constructor, typeof(UnityEngine.Playables.PlayableDirector),typeof(UnityEngine.Behaviour));
	}
}
