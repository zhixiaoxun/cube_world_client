using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_Playables_PlayableExtensions : LuaObject {
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.Playables.PlayableExtensions");
		createTypeMetatable(l,null, typeof(UnityEngine.Playables.PlayableExtensions));
	}
}
