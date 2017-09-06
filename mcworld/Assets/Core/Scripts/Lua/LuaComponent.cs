using UnityEngine;
using System.Collections;
using SLua;

namespace Core.Lua
{
    [CustomLuaClass]
    public class LuaComponent : MonoBehaviour
    {
        private LuaTable _luaTable = null;
        private LuaFunction _luaFuncStart = null;
        private LuaFunction _luaFuncUpdate = null;
        private LuaFunction _luaFuncOnDestroy = null;

        public void RegisterLuaTable(LuaTable luaTable)
        {
            _luaTable = luaTable;
            _luaFuncStart = (LuaFunction)_luaTable["Start"];
            _luaFuncUpdate = (LuaFunction)_luaTable["Update"];
            _luaFuncOnDestroy = (LuaFunction)_luaTable["OnDestroy"];
        }

        void Start()
        {
            if (_luaFuncStart != null)
            {
                _luaFuncStart.call(_luaTable);
            }
        }

        void Update()
        {
            if (_luaFuncUpdate != null)
            {
                _luaFuncUpdate.call(_luaTable);
            }
        }

        void OnDestroy()
        {
            if (_luaFuncOnDestroy != null)
            {
                _luaFuncOnDestroy.call(_luaTable);
            }
        }
    }
}