using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_Core_GameLogic_ActiveObjects_Player : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int constructor(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player o;
			Core.GameLogic.World a1;
			checkType(l,2,out a1);
			o=new Core.GameLogic.ActiveObjects.Player(a1);
			pushValue(l,true);
			pushValue(l,o);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetNumberExtField(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetNumberExtField(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int SetNumberExtField(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			self.SetNumberExtField(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetStringExtField(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetStringExtField(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int SetStringExtField(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			self.SetStringExtField(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int ChangeState(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			Core.GameLogic.ActiveObjects.FSM.LogicStateDef a1;
			checkEnum(l,2,out a1);
			System.Object[] a2;
			checkParams(l,3,out a2);
			self.ChangeState(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int CanMove(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			var ret=self.CanMove();
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Idle(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			self.Idle();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Run(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			self.Run();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Jump(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			self.Jump();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Attack1(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			self.Attack1();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Attack2(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			self.Attack2();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get__Component(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self._Component);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get__PlayerAnimator(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self._PlayerAnimator);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get__PlayerRigidbody(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self._PlayerRigidbody);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get__LogicState(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			pushValue(l,true);
			pushEnum(l,(int)self._LogicState);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set__LogicState(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			Core.GameLogic.ActiveObjects.FSM.LogicStateDef v;
			checkEnum(l,2,out v);
			self._LogicState=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get__RepresentState(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			pushValue(l,true);
			pushEnum(l,(int)self._RepresentState);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set__RepresentState(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			Core.GameLogic.ActiveObjects.FSM.RepresentStateDef v;
			checkEnum(l,2,out v);
			self._RepresentState=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get__NumberExtFields(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self._NumberExtFields);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get__StringExtFields(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self._StringExtFields);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get__CurrentBlockPos(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self._CurrentBlockPos);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set__CurrentBlockPos(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			UnityEngine.Vector3 v;
			checkType(l,2,out v);
			self._CurrentBlockPos=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get__LastBlockPos(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self._LastBlockPos);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set__LastBlockPos(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			UnityEngine.Vector3 v;
			checkType(l,2,out v);
			self._LastBlockPos=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_IsBlockPosChanged(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsBlockPosChanged);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get__CurrentNodePos(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self._CurrentNodePos);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set__CurrentNodePos(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			UnityEngine.Vector3 v;
			checkType(l,2,out v);
			self._CurrentNodePos=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get__CurrentPos(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self._CurrentPos);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set__CurrentPos(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			UnityEngine.Vector3 v;
			checkType(l,2,out v);
			self._CurrentPos=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get__LastPos(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self._LastPos);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set__LastPos(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			UnityEngine.Vector3 v;
			checkType(l,2,out v);
			self._LastPos=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_IsPosChanged(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsPosChanged);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get__CurrentSpeed(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self._CurrentSpeed);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get__LastSpeed(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self._LastSpeed);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_IsSpeedChanged(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsSpeedChanged);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get__IsDead(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self._IsDead);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get__CurrentSkill(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self._CurrentSkill);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get__PlayerCharaContrl(IntPtr l) {
		try {
			Core.GameLogic.ActiveObjects.Player self=(Core.GameLogic.ActiveObjects.Player)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self._PlayerCharaContrl);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"Core.GameLogic.ActiveObjects.Player");
		addMember(l,GetNumberExtField);
		addMember(l,SetNumberExtField);
		addMember(l,GetStringExtField);
		addMember(l,SetStringExtField);
		addMember(l,ChangeState);
		addMember(l,CanMove);
		addMember(l,Idle);
		addMember(l,Run);
		addMember(l,Jump);
		addMember(l,Attack1);
		addMember(l,Attack2);
		addMember(l,"_Component",get__Component,null,true);
		addMember(l,"_PlayerAnimator",get__PlayerAnimator,null,true);
		addMember(l,"_PlayerRigidbody",get__PlayerRigidbody,null,true);
		addMember(l,"_LogicState",get__LogicState,set__LogicState,true);
		addMember(l,"_RepresentState",get__RepresentState,set__RepresentState,true);
		addMember(l,"_NumberExtFields",get__NumberExtFields,null,true);
		addMember(l,"_StringExtFields",get__StringExtFields,null,true);
		addMember(l,"_CurrentBlockPos",get__CurrentBlockPos,set__CurrentBlockPos,true);
		addMember(l,"_LastBlockPos",get__LastBlockPos,set__LastBlockPos,true);
		addMember(l,"IsBlockPosChanged",get_IsBlockPosChanged,null,true);
		addMember(l,"_CurrentNodePos",get__CurrentNodePos,set__CurrentNodePos,true);
		addMember(l,"_CurrentPos",get__CurrentPos,set__CurrentPos,true);
		addMember(l,"_LastPos",get__LastPos,set__LastPos,true);
		addMember(l,"IsPosChanged",get_IsPosChanged,null,true);
		addMember(l,"_CurrentSpeed",get__CurrentSpeed,null,true);
		addMember(l,"_LastSpeed",get__LastSpeed,null,true);
		addMember(l,"IsSpeedChanged",get_IsSpeedChanged,null,true);
		addMember(l,"_IsDead",get__IsDead,null,true);
		addMember(l,"_CurrentSkill",get__CurrentSkill,null,true);
		addMember(l,"_PlayerCharaContrl",get__PlayerCharaContrl,null,true);
		createTypeMetatable(l,constructor, typeof(Core.GameLogic.ActiveObjects.Player),typeof(Core.GameLogic.ActiveObjects.ActiveObject));
	}
}
