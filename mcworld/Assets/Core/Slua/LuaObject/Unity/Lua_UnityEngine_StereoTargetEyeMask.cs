﻿using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_StereoTargetEyeMask : LuaObject {
	static public void reg(IntPtr l) {
		getEnumTable(l,"UnityEngine.StereoTargetEyeMask");
		addMember(l,0,"None");
		addMember(l,1,"Left");
		addMember(l,2,"Right");
		addMember(l,3,"Both");
		LuaDLL.lua_pop(l, 1);
	}
}
