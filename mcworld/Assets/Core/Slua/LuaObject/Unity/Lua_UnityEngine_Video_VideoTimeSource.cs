﻿using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_Video_VideoTimeSource : LuaObject {
	static public void reg(IntPtr l) {
		getEnumTable(l,"UnityEngine.Video.VideoTimeSource");
		addMember(l,0,"AudioDSPTimeSource");
		addMember(l,1,"GameTimeSource");
		LuaDLL.lua_pop(l, 1);
	}
}
