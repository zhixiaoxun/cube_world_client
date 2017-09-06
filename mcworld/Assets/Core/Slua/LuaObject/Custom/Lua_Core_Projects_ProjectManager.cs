using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_Core_Projects_ProjectManager : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int constructor(IntPtr l) {
		try {
			Core.Projects.ProjectManager o;
			o=new Core.Projects.ProjectManager();
			pushValue(l,true);
			pushValue(l,o);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_ProjectNameList(IntPtr l) {
		try {
			Core.Projects.ProjectManager self=(Core.Projects.ProjectManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ProjectNameList);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_Instance(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,Core.Projects.ProjectManager.Instance);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"Core.Projects.ProjectManager");
		addMember(l,"ProjectNameList",get_ProjectNameList,null,true);
		addMember(l,"Instance",get_Instance,null,false);
		createTypeMetatable(l,constructor, typeof(Core.Projects.ProjectManager),typeof(Singleton<Core.Projects.ProjectManager>));
	}
}
