using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_ParticleSystemEmitterVelocityMode : LuaObject {
	static public void reg(IntPtr l) {
		getEnumTable(l,"UnityEngine.ParticleSystemEmitterVelocityMode");
		addMember(l,0,"Transform");
		addMember(l,1,"Rigidbody");
		LuaDLL.lua_pop(l, 1);
	}
}
