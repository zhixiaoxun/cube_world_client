using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_Playables_FrameData_EvaluationType : LuaObject {
	static public void reg(IntPtr l) {
		getEnumTable(l,"UnityEngine.Playables.FrameData.EvaluationType");
		addMember(l,0,"Evaluate");
		addMember(l,1,"Playback");
		LuaDLL.lua_pop(l, 1);
	}
}
