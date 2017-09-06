using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_Core_Asset_ResourceManager : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int constructor(IntPtr l) {
		try {
			Core.Asset.ResourceManager o;
			o=new Core.Asset.ResourceManager();
			pushValue(l,true);
			pushValue(l,o);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int LoadTextAssetFromResource(IntPtr l) {
		try {
			Core.Asset.ResourceManager self=(Core.Asset.ResourceManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.LoadTextAssetFromResource(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int LoadPrefabAssetFromResource(IntPtr l) {
		try {
			Core.Asset.ResourceManager self=(Core.Asset.ResourceManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.LoadPrefabAssetFromResource(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetResourceName(IntPtr l) {
		try {
			Core.Asset.ResourceManager self=(Core.Asset.ResourceManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			var ret=self.GetResourceName(a1,a2);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int LoadFromResources(IntPtr l) {
		try {
			Core.Asset.ResourceManager self=(Core.Asset.ResourceManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Type a2;
			checkType(l,3,out a2);
			var ret=self.LoadFromResources(a1,a2);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int LoadXMLText(IntPtr l) {
		try {
			Core.Asset.ResourceManager self=(Core.Asset.ResourceManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.LoadXMLText(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int LoadLuaFile(IntPtr l) {
		try {
			Core.Asset.ResourceManager self=(Core.Asset.ResourceManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.LoadLuaFile(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetLuaFullPath(IntPtr l) {
		try {
			Core.Asset.ResourceManager self=(Core.Asset.ResourceManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetLuaFullPath(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int RemoveLuaFileCache(IntPtr l) {
		try {
			Core.Asset.ResourceManager self=(Core.Asset.ResourceManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.RemoveLuaFileCache(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int ClearAllLuaFileCache(IntPtr l) {
		try {
			Core.Asset.ResourceManager self=(Core.Asset.ResourceManager)checkSelf(l);
			self.ClearAllLuaFileCache();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_UseLuaCache(IntPtr l) {
		try {
			Core.Asset.ResourceManager self=(Core.Asset.ResourceManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.UseLuaCache);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_UseLuaCache(IntPtr l) {
		try {
			Core.Asset.ResourceManager self=(Core.Asset.ResourceManager)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.UseLuaCache=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_Instance(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,Core.Asset.ResourceManager.Instance);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"Core.Asset.ResourceManager");
		addMember(l,LoadTextAssetFromResource);
		addMember(l,LoadPrefabAssetFromResource);
		addMember(l,GetResourceName);
		addMember(l,LoadFromResources);
		addMember(l,LoadXMLText);
		addMember(l,LoadLuaFile);
		addMember(l,GetLuaFullPath);
		addMember(l,RemoveLuaFileCache);
		addMember(l,ClearAllLuaFileCache);
		addMember(l,"UseLuaCache",get_UseLuaCache,set_UseLuaCache,true);
		addMember(l,"Instance",get_Instance,null,false);
		createTypeMetatable(l,constructor, typeof(Core.Asset.ResourceManager),typeof(Singleton<Core.Asset.ResourceManager>));
	}
}
