using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_Video_VideoTimeReference : LuaObject {
	static public void reg(IntPtr l) {
		getEnumTable(l,"UnityEngine.Video.VideoTimeReference");
		addMember(l,0,"Freerun");
		addMember(l,1,"InternalTime");
		addMember(l,2,"ExternalTime");
		LuaDLL.lua_pop(l, 1);
	}
}
