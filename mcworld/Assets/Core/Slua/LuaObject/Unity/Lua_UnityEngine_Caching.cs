﻿using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_Caching : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int constructor(IntPtr l) {
		try {
			UnityEngine.Caching o;
			o=new UnityEngine.Caching();
			pushValue(l,true);
			pushValue(l,o);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int ClearCache_s(IntPtr l) {
		try {
			var ret=UnityEngine.Caching.ClearCache();
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int ClearCachedVersion_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			UnityEngine.Hash128 a2;
			checkValueType(l,2,out a2);
			var ret=UnityEngine.Caching.ClearCachedVersion(a1,a2);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int ClearOtherCachedVersions_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			UnityEngine.Hash128 a2;
			checkValueType(l,2,out a2);
			var ret=UnityEngine.Caching.ClearOtherCachedVersions(a1,a2);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int ClearAllCachedVersions_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=UnityEngine.Caching.ClearAllCachedVersions(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetCachedVersions_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			System.Collections.Generic.List<UnityEngine.Hash128> a2;
			checkType(l,2,out a2);
			UnityEngine.Caching.GetCachedVersions(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int IsVersionCached_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==1){
				UnityEngine.CachedAssetBundle a1;
				checkValueType(l,1,out a1);
				var ret=UnityEngine.Caching.IsVersionCached(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==2){
				System.String a1;
				checkType(l,1,out a1);
				UnityEngine.Hash128 a2;
				checkValueType(l,2,out a2);
				var ret=UnityEngine.Caching.IsVersionCached(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
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
	static public int MarkAsUsed_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==1){
				UnityEngine.CachedAssetBundle a1;
				checkValueType(l,1,out a1);
				var ret=UnityEngine.Caching.MarkAsUsed(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==2){
				System.String a1;
				checkType(l,1,out a1);
				UnityEngine.Hash128 a2;
				checkValueType(l,2,out a2);
				var ret=UnityEngine.Caching.MarkAsUsed(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
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
	static public int AddCache_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=UnityEngine.Caching.AddCache(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetCacheAt_s(IntPtr l) {
		try {
			System.Int32 a1;
			checkType(l,1,out a1);
			var ret=UnityEngine.Caching.GetCacheAt(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetCacheByPath_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=UnityEngine.Caching.GetCacheByPath(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetAllCachePaths_s(IntPtr l) {
		try {
			System.Collections.Generic.List<System.String> a1;
			checkType(l,1,out a1);
			UnityEngine.Caching.GetAllCachePaths(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int RemoveCache_s(IntPtr l) {
		try {
			UnityEngine.Cache a1;
			checkValueType(l,1,out a1);
			var ret=UnityEngine.Caching.RemoveCache(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int MoveCacheBefore_s(IntPtr l) {
		try {
			UnityEngine.Cache a1;
			checkValueType(l,1,out a1);
			UnityEngine.Cache a2;
			checkValueType(l,2,out a2);
			UnityEngine.Caching.MoveCacheBefore(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int MoveCacheAfter_s(IntPtr l) {
		try {
			UnityEngine.Cache a1;
			checkValueType(l,1,out a1);
			UnityEngine.Cache a2;
			checkValueType(l,2,out a2);
			UnityEngine.Caching.MoveCacheAfter(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_compressionEnabled(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Caching.compressionEnabled);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_compressionEnabled(IntPtr l) {
		try {
			bool v;
			checkType(l,2,out v);
			UnityEngine.Caching.compressionEnabled=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_ready(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Caching.ready);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_cacheCount(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Caching.cacheCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_defaultCache(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Caching.defaultCache);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_currentCacheForWriting(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Caching.currentCacheForWriting);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_currentCacheForWriting(IntPtr l) {
		try {
			UnityEngine.Cache v;
			checkValueType(l,2,out v);
			UnityEngine.Caching.currentCacheForWriting=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.Caching");
		addMember(l,ClearCache_s);
		addMember(l,ClearCachedVersion_s);
		addMember(l,ClearOtherCachedVersions_s);
		addMember(l,ClearAllCachedVersions_s);
		addMember(l,GetCachedVersions_s);
		addMember(l,IsVersionCached_s);
		addMember(l,MarkAsUsed_s);
		addMember(l,AddCache_s);
		addMember(l,GetCacheAt_s);
		addMember(l,GetCacheByPath_s);
		addMember(l,GetAllCachePaths_s);
		addMember(l,RemoveCache_s);
		addMember(l,MoveCacheBefore_s);
		addMember(l,MoveCacheAfter_s);
		addMember(l,"compressionEnabled",get_compressionEnabled,set_compressionEnabled,false);
		addMember(l,"ready",get_ready,null,false);
		addMember(l,"cacheCount",get_cacheCount,null,false);
		addMember(l,"defaultCache",get_defaultCache,null,false);
		addMember(l,"currentCacheForWriting",get_currentCacheForWriting,set_currentCacheForWriting,false);
		createTypeMetatable(l,constructor, typeof(UnityEngine.Caching));
	}
}
