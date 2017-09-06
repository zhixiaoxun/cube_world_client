using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_VR_VRNode : LuaObject {
	static public void reg(IntPtr l) {
		getEnumTable(l,"UnityEngine.VR.VRNode");
		addMember(l,0,"LeftEye");
		addMember(l,1,"RightEye");
		addMember(l,2,"CenterEye");
		addMember(l,3,"Head");
		addMember(l,4,"LeftHand");
		addMember(l,5,"RightHand");
		addMember(l,6,"GameController");
		addMember(l,7,"TrackingReference");
		addMember(l,8,"HardwareTracker");
		LuaDLL.lua_pop(l, 1);
	}
}
