using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_Animations_AnimationPlayableGraphExtensions : LuaObject {
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.Animations.AnimationPlayableGraphExtensions");
		createTypeMetatable(l,null, typeof(UnityEngine.Animations.AnimationPlayableGraphExtensions));
	}
}
