﻿using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_ShadowResolution : LuaObject {
	static public void reg(IntPtr l) {
		getEnumTable(l,"UnityEngine.ShadowResolution");
		addMember(l,0,"Low");
		addMember(l,1,"Medium");
		addMember(l,2,"High");
		addMember(l,3,"VeryHigh");
		LuaDLL.lua_pop(l, 1);
	}
}
