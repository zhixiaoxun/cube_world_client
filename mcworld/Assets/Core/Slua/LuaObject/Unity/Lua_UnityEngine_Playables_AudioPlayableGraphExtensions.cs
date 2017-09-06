using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_Playables_AudioPlayableGraphExtensions : LuaObject {
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.Playables.AudioPlayableGraphExtensions");
		createTypeMetatable(l,null, typeof(UnityEngine.Playables.AudioPlayableGraphExtensions));
	}
}
