using System;
using System.Collections.Generic;
namespace SLua {
	[LuaBinder(3)]
	public class BindCustom {
		public static Action<IntPtr>[] GetBindList() {
			Action<IntPtr>[] list= {
				Lua_Core_Asset_ResourceManager.reg,
				Lua_Core_CoreEntry.reg,
				Lua_Core_GameLogic_ActiveObjects_ActiveObject.reg,
				Lua_Core_GameLogic_ActiveObjects_Player.reg,
				Lua_Core_GameLogic_ActiveObjects_LocalPlayer.reg,
				Lua_Core_GameLogic_World.reg,
				Lua_Core_Lua_LuaComponent.reg,
				Lua_Core_Lua_LuaUIWindow.reg,
				Lua_Core_Projects_ProjectManager.reg,
				Lua_Core_RepresentLogic_InputManager.reg,
				Lua_SingletonBase.reg,
				Lua_Core_Utils_TimerBehaviour.reg,
				Lua_Core_Utils_TimerHeap.reg,
				Lua_Core_Utils_TimerManager.reg,
				Lua_ProtoTest.reg,
				Lua_SprotoTest.reg,
				Lua_System_Collections_Generic_List_1_int.reg,
				Lua_System_Collections_Generic_Dictionary_2_int_string.reg,
				Lua_System_String.reg,
			};
			return list;
		}
	}
}
