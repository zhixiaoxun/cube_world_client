using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_ShadowmaskMode : LuaObject {
	static public void reg(IntPtr l) {
		getEnumTable(l,"UnityEngine.ShadowmaskMode");
		addMember(l,0,"Shadowmask");
		addMember(l,1,"DistanceShadowmask");
		LuaDLL.lua_pop(l, 1);
	}
}
