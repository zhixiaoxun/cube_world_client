using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_Diagnostics_PlayerConnection : LuaObject {
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.Diagnostics.PlayerConnection");
		createTypeMetatable(l,null, typeof(UnityEngine.Diagnostics.PlayerConnection));
	}
}
