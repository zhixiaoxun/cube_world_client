using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_Animations_AnimationPlayableExtensions : LuaObject {
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.Animations.AnimationPlayableExtensions");
		createTypeMetatable(l,null, typeof(UnityEngine.Animations.AnimationPlayableExtensions));
	}
}
