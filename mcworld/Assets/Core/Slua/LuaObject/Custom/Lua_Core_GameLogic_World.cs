using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_Core_GameLogic_World : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int constructor(IntPtr l) {
		try {
			Core.GameLogic.World o;
			o=new Core.GameLogic.World();
			pushValue(l,true);
			pushValue(l,o);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Init(IntPtr l) {
		try {
			Core.GameLogic.World self=(Core.GameLogic.World)checkSelf(l);
			System.Action<System.Boolean> a1;
			LuaDelegation.checkDelegate(l,2,out a1);
			System.Action<Core.GameLogic.ActiveObjects.LocalPlayer> a2;
			LuaDelegation.checkDelegate(l,3,out a2);
			self.Init(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int UnInit(IntPtr l) {
		try {
			Core.GameLogic.World self=(Core.GameLogic.World)checkSelf(l);
			self.UnInit();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Active(IntPtr l) {
		try {
			Core.GameLogic.World self=(Core.GameLogic.World)checkSelf(l);
			self.Active();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int ConnectServer(IntPtr l) {
		try {
			Core.GameLogic.World self=(Core.GameLogic.World)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			var ret=self.ConnectServer(a1,a2);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int LoginServer(IntPtr l) {
		try {
			Core.GameLogic.World self=(Core.GameLogic.World)checkSelf(l);
			System.UInt32 a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			System.UInt32 a3;
			checkType(l,4,out a3);
			System.UInt64 a4;
			checkType(l,5,out a4);
			System.Int32 a5;
			checkType(l,6,out a5);
			System.String a6;
			checkType(l,7,out a6);
			System.Action<System.Boolean,System.Int32,System.String> a7;
			LuaDelegation.checkDelegate(l,8,out a7);
			var ret=self.LoginServer(a1,a2,a3,a4,a5,a6,a7);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int UpdatePosition(IntPtr l) {
		try {
			Core.GameLogic.World self=(Core.GameLogic.World)checkSelf(l);
			UnityEngine.Vector3 a1;
			checkType(l,2,out a1);
			self.UpdatePosition(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int OnAccessDenied(IntPtr l) {
		try {
			Core.GameLogic.World self=(Core.GameLogic.World)checkSelf(l);
			System.UInt32 a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			self.OnAccessDenied(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set__OnLocalPlayerCreatedCallback(IntPtr l) {
		try {
			Core.GameLogic.World self=(Core.GameLogic.World)checkSelf(l);
			System.Action<Core.GameLogic.ActiveObjects.LocalPlayer> v;
			int op=LuaDelegation.checkDelegate(l,2,out v);
			if(op==0) self._OnLocalPlayerCreatedCallback=v;
			else if(op==1) self._OnLocalPlayerCreatedCallback+=v;
			else if(op==2) self._OnLocalPlayerCreatedCallback-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get__Instance(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,Core.GameLogic.World._Instance);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set__Instance(IntPtr l) {
		try {
			Core.GameLogic.World v;
			checkType(l,2,out v);
			Core.GameLogic.World._Instance=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get__LoadingProgress(IntPtr l) {
		try {
			Core.GameLogic.World self=(Core.GameLogic.World)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self._LoadingProgress);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get__LoadingInfo(IntPtr l) {
		try {
			Core.GameLogic.World self=(Core.GameLogic.World)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self._LoadingInfo);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get__LoadingFinished(IntPtr l) {
		try {
			Core.GameLogic.World self=(Core.GameLogic.World)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self._LoadingFinished);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get__CppCore(IntPtr l) {
		try {
			Core.GameLogic.World self=(Core.GameLogic.World)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self._CppCore);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get__NetworkManager(IntPtr l) {
		try {
			Core.GameLogic.World self=(Core.GameLogic.World)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self._NetworkManager);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get__ActiveObjectManager(IntPtr l) {
		try {
			Core.GameLogic.World self=(Core.GameLogic.World)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self._ActiveObjectManager);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get__BlockManager(IntPtr l) {
		try {
			Core.GameLogic.World self=(Core.GameLogic.World)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self._BlockManager);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get__LocalPlayer(IntPtr l) {
		try {
			Core.GameLogic.World self=(Core.GameLogic.World)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self._LocalPlayer);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"Core.GameLogic.World");
		addMember(l,Init);
		addMember(l,UnInit);
		addMember(l,Active);
		addMember(l,ConnectServer);
		addMember(l,LoginServer);
		addMember(l,UpdatePosition);
		addMember(l,OnAccessDenied);
		addMember(l,"_OnLocalPlayerCreatedCallback",null,set__OnLocalPlayerCreatedCallback,true);
		addMember(l,"_Instance",get__Instance,set__Instance,false);
		addMember(l,"_LoadingProgress",get__LoadingProgress,null,true);
		addMember(l,"_LoadingInfo",get__LoadingInfo,null,true);
		addMember(l,"_LoadingFinished",get__LoadingFinished,null,true);
		addMember(l,"_CppCore",get__CppCore,null,true);
		addMember(l,"_NetworkManager",get__NetworkManager,null,true);
		addMember(l,"_ActiveObjectManager",get__ActiveObjectManager,null,true);
		addMember(l,"_BlockManager",get__BlockManager,null,true);
		addMember(l,"_LocalPlayer",get__LocalPlayer,null,true);
		createTypeMetatable(l,constructor, typeof(Core.GameLogic.World));
	}
}
