using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_SingletonBase : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int InitCoroutine(IntPtr l) {
		try {
			SingletonBase self=(SingletonBase)checkSelf(l);
			var ret=self.InitCoroutine();
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Init(IntPtr l) {
		try {
			SingletonBase self=(SingletonBase)checkSelf(l);
			var ret=self.Init();
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Exit(IntPtr l) {
		try {
			SingletonBase self=(SingletonBase)checkSelf(l);
			self.Exit();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Login(IntPtr l) {
		try {
			SingletonBase self=(SingletonBase)checkSelf(l);
			var ret=self.Login();
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Logout(IntPtr l) {
		try {
			SingletonBase self=(SingletonBase)checkSelf(l);
			self.Logout();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Update(IntPtr l) {
		try {
			SingletonBase self=(SingletonBase)checkSelf(l);
			self.Update();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int LateUpdate(IntPtr l) {
		try {
			SingletonBase self=(SingletonBase)checkSelf(l);
			self.LateUpdate();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int FixedUpdate(IntPtr l) {
		try {
			SingletonBase self=(SingletonBase)checkSelf(l);
			self.FixedUpdate();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int EndOfFrame(IntPtr l) {
		try {
			SingletonBase self=(SingletonBase)checkSelf(l);
			self.EndOfFrame();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"SingletonBase");
		addMember(l,InitCoroutine);
		addMember(l,Init);
		addMember(l,Exit);
		addMember(l,Login);
		addMember(l,Logout);
		addMember(l,Update);
		addMember(l,LateUpdate);
		addMember(l,FixedUpdate);
		addMember(l,EndOfFrame);
		createTypeMetatable(l,null, typeof(SingletonBase));
	}
}
