using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_Core_GameLogic_ActiveObjects_LocalPlayer : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int constructor(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.LocalPlayer o;
			Core.GameLogic.World a1;
			checkType(l,2,out a1);
			o=new Core.GameLogic.ActiveObjects.LocalPlayer(a1);
			pushValue(l,true);
			pushValue(l,o);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int UpdatePosition(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.LocalPlayer self=(Core.GameLogic.ActiveObjects.LocalPlayer)checkSelf(l);
			self.UpdatePosition();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int OnLocalPlayerChangeBlock(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.LocalPlayer self=(Core.GameLogic.ActiveObjects.LocalPlayer)checkSelf(l);
			UnityEngine.Vector3 a1;
			checkType(l,2,out a1);
			self.OnLocalPlayerChangeBlock(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"Core.GameLogic.ActiveObjects.LocalPlayer");
		addMember(l,UpdatePosition);
		addMember(l,OnLocalPlayerChangeBlock);
		createTypeMetatable(l,constructor, typeof(Core.GameLogic.ActiveObjects.LocalPlayer),typeof(Core.GameLogic.ActiveObjects.Player));
	}
}
