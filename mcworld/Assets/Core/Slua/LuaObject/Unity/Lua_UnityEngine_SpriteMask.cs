using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_SpriteMask : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int constructor(IntPtr l) {
		try {
			UnityEngine.SpriteMask o;
			o=new UnityEngine.SpriteMask();
			pushValue(l,true);
			pushValue(l,o);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_sprite(IntPtr l) {
		try {
			UnityEngine.SpriteMask self=(UnityEngine.SpriteMask)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.sprite);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_sprite(IntPtr l) {
		try {
			UnityEngine.SpriteMask self=(UnityEngine.SpriteMask)checkSelf(l);
			UnityEngine.Sprite v;
			checkType(l,2,out v);
			self.sprite=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_alphaCutoff(IntPtr l) {
		try {
			UnityEngine.SpriteMask self=(UnityEngine.SpriteMask)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.alphaCutoff);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_alphaCutoff(IntPtr l) {
		try {
			UnityEngine.SpriteMask self=(UnityEngine.SpriteMask)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.alphaCutoff=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_isCustomRangeActive(IntPtr l) {
		try {
			UnityEngine.SpriteMask self=(UnityEngine.SpriteMask)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.isCustomRangeActive);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_isCustomRangeActive(IntPtr l) {
		try {
			UnityEngine.SpriteMask self=(UnityEngine.SpriteMask)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.isCustomRangeActive=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_frontSortingLayerID(IntPtr l) {
		try {
			UnityEngine.SpriteMask self=(UnityEngine.SpriteMask)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.frontSortingLayerID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_frontSortingLayerID(IntPtr l) {
		try {
			UnityEngine.SpriteMask self=(UnityEngine.SpriteMask)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.frontSortingLayerID=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_frontSortingOrder(IntPtr l) {
		try {
			UnityEngine.SpriteMask self=(UnityEngine.SpriteMask)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.frontSortingOrder);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_frontSortingOrder(IntPtr l) {
		try {
			UnityEngine.SpriteMask self=(UnityEngine.SpriteMask)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.frontSortingOrder=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_backSortingLayerID(IntPtr l) {
		try {
			UnityEngine.SpriteMask self=(UnityEngine.SpriteMask)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.backSortingLayerID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_backSortingLayerID(IntPtr l) {
		try {
			UnityEngine.SpriteMask self=(UnityEngine.SpriteMask)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.backSortingLayerID=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_backSortingOrder(IntPtr l) {
		try {
			UnityEngine.SpriteMask self=(UnityEngine.SpriteMask)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.backSortingOrder);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_backSortingOrder(IntPtr l) {
		try {
			UnityEngine.SpriteMask self=(UnityEngine.SpriteMask)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.backSortingOrder=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.SpriteMask");
		addMember(l,"sprite",get_sprite,set_sprite,true);
		addMember(l,"alphaCutoff",get_alphaCutoff,set_alphaCutoff,true);
		addMember(l,"isCustomRangeActive",get_isCustomRangeActive,set_isCustomRangeActive,true);
		addMember(l,"frontSortingLayerID",get_frontSortingLayerID,set_frontSortingLayerID,true);
		addMember(l,"frontSortingOrder",get_frontSortingOrder,set_frontSortingOrder,true);
		addMember(l,"backSortingLayerID",get_backSortingLayerID,set_backSortingLayerID,true);
		addMember(l,"backSortingOrder",get_backSortingOrder,set_backSortingOrder,true);
		createTypeMetatable(l,constructor, typeof(UnityEngine.SpriteMask),typeof(UnityEngine.Renderer));
	}
}
