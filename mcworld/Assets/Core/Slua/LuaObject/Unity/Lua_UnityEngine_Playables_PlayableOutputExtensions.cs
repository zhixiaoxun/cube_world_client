using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_Playables_PlayableOutputExtensions : LuaObject {
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.Playables.PlayableOutputExtensions");
		createTypeMetatable(l,null, typeof(UnityEngine.Playables.PlayableOutputExtensions));
	}
}
