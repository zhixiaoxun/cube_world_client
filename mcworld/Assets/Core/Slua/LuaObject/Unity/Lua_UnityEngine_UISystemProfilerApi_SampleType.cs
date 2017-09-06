using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_UISystemProfilerApi_SampleType : LuaObject {
	static public void reg(IntPtr l) {
		getEnumTable(l,"UnityEngine.UISystemProfilerApi.SampleType");
		addMember(l,0,"Layout");
		addMember(l,1,"Render");
		LuaDLL.lua_pop(l, 1);
	}
}
