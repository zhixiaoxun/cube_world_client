﻿using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_ShadowQuality : LuaObject {
	static public void reg(IntPtr l) {
		getEnumTable(l,"UnityEngine.ShadowQuality");
		addMember(l,0,"Disable");
		addMember(l,1,"HardOnly");
		addMember(l,2,"All");
		LuaDLL.lua_pop(l, 1);
	}
}
