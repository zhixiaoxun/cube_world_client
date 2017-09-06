﻿using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_LineRenderer : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int constructor(IntPtr l) {
		try {
			UnityEngine.LineRenderer o;
			o=new UnityEngine.LineRenderer();
			pushValue(l,true);
			pushValue(l,o);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int SetPosition(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			UnityEngine.Vector3 a2;
			checkType(l,3,out a2);
			self.SetPosition(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetPosition(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetPosition(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int SetPositions(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			UnityEngine.Vector3[] a1;
			checkArray(l,2,out a1);
			self.SetPositions(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetPositions(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			UnityEngine.Vector3[] a1;
			checkArray(l,2,out a1);
			var ret=self.GetPositions(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Simplify(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			self.Simplify(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_startWidth(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.startWidth);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_startWidth(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.startWidth=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_endWidth(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.endWidth);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_endWidth(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.endWidth=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_widthCurve(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.widthCurve);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_widthCurve(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			UnityEngine.AnimationCurve v;
			checkType(l,2,out v);
			self.widthCurve=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_widthMultiplier(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.widthMultiplier);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_widthMultiplier(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.widthMultiplier=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_startColor(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.startColor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_startColor(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			UnityEngine.Color v;
			checkType(l,2,out v);
			self.startColor=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_endColor(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.endColor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_endColor(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			UnityEngine.Color v;
			checkType(l,2,out v);
			self.endColor=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_colorGradient(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.colorGradient);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_colorGradient(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			UnityEngine.Gradient v;
			checkType(l,2,out v);
			self.colorGradient=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_positionCount(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.positionCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_positionCount(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.positionCount=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_useWorldSpace(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.useWorldSpace);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_useWorldSpace(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.useWorldSpace=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_loop(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.loop);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_loop(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.loop=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_numCornerVertices(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.numCornerVertices);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_numCornerVertices(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.numCornerVertices=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_numCapVertices(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.numCapVertices);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_numCapVertices(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.numCapVertices=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_textureMode(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			pushValue(l,true);
			pushEnum(l,(int)self.textureMode);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_textureMode(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			UnityEngine.LineTextureMode v;
			checkEnum(l,2,out v);
			self.textureMode=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_alignment(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			pushValue(l,true);
			pushEnum(l,(int)self.alignment);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_alignment(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			UnityEngine.LineAlignment v;
			checkEnum(l,2,out v);
			self.alignment=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_generateLightingData(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.generateLightingData);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_generateLightingData(IntPtr l) {
		try {
			UnityEngine.LineRenderer self=(UnityEngine.LineRenderer)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.generateLightingData=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.LineRenderer");
		addMember(l,SetPosition);
		addMember(l,GetPosition);
		addMember(l,SetPositions);
		addMember(l,GetPositions);
		addMember(l,Simplify);
		addMember(l,"startWidth",get_startWidth,set_startWidth,true);
		addMember(l,"endWidth",get_endWidth,set_endWidth,true);
		addMember(l,"widthCurve",get_widthCurve,set_widthCurve,true);
		addMember(l,"widthMultiplier",get_widthMultiplier,set_widthMultiplier,true);
		addMember(l,"startColor",get_startColor,set_startColor,true);
		addMember(l,"endColor",get_endColor,set_endColor,true);
		addMember(l,"colorGradient",get_colorGradient,set_colorGradient,true);
		addMember(l,"positionCount",get_positionCount,set_positionCount,true);
		addMember(l,"useWorldSpace",get_useWorldSpace,set_useWorldSpace,true);
		addMember(l,"loop",get_loop,set_loop,true);
		addMember(l,"numCornerVertices",get_numCornerVertices,set_numCornerVertices,true);
		addMember(l,"numCapVertices",get_numCapVertices,set_numCapVertices,true);
		addMember(l,"textureMode",get_textureMode,set_textureMode,true);
		addMember(l,"alignment",get_alignment,set_alignment,true);
		addMember(l,"generateLightingData",get_generateLightingData,set_generateLightingData,true);
		createTypeMetatable(l,constructor, typeof(UnityEngine.LineRenderer),typeof(UnityEngine.Renderer));
	}
}