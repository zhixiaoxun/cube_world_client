using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_U2D_SpriteAtlasManager : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int constructor(IntPtr l) {
		try {
			UnityEngine.U2D.SpriteAtlasManager o;
			o=new UnityEngine.U2D.SpriteAtlasManager();
			pushValue(l,true);
			pushValue(l,o);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.U2D.SpriteAtlasManager");
		createTypeMetatable(l,constructor, typeof(UnityEngine.U2D.SpriteAtlasManager));
	}
}
