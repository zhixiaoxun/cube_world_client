﻿using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_RigidbodyType2D : LuaObject {
	static public void reg(IntPtr l) {
		getEnumTable(l,"UnityEngine.RigidbodyType2D");
		addMember(l,0,"Dynamic");
		addMember(l,1,"Kinematic");
		addMember(l,2,"Static");
		LuaDLL.lua_pop(l, 1);
	}
}
