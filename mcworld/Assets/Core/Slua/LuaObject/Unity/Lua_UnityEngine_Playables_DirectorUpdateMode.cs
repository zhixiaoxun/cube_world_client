using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_Playables_DirectorUpdateMode : LuaObject {
	static public void reg(IntPtr l) {
		getEnumTable(l,"UnityEngine.Playables.DirectorUpdateMode");
		addMember(l,0,"DSPClock");
		addMember(l,1,"GameTime");
		addMember(l,2,"UnscaledGameTime");
		addMember(l,3,"Manual");
		LuaDLL.lua_pop(l, 1);
	}
}
