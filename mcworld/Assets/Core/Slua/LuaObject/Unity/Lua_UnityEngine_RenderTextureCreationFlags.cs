using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_RenderTextureCreationFlags : LuaObject {
	static public void reg(IntPtr l) {
		getEnumTable(l,"UnityEngine.RenderTextureCreationFlags");
		addMember(l,1,"MipMap");
		addMember(l,2,"AutoGenerateMips");
		addMember(l,4,"SRGB");
		addMember(l,8,"EyeTexture");
		addMember(l,16,"EnableRandomWrite");
		addMember(l,32,"CreatedFromScript");
		addMember(l,128,"AllowVerticalFlip");
		LuaDLL.lua_pop(l, 1);
	}
}
