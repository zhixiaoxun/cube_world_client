using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_Core_Lua_LuaComponent : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int RegisterLuaTable(IntPtr l) {
		try {
			Core.Lua.LuaComponent self=(Core.Lua.LuaComponent)checkSelf(l);
			SLua.LuaTable a1;
			checkType(l,2,out a1);
			self.RegisterLuaTable(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"Core.Lua.LuaComponent");
		addMember(l,RegisterLuaTable);
		createTypeMetatable(l,null, typeof(Core.Lua.LuaComponent),typeof(UnityEngine.MonoBehaviour));
	}
}
