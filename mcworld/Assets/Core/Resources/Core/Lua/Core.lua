import "UnityEngine"

require "Core/Lua/PreLoadList"
require "Core/Lua/UI/UISelectGame"

function main()
	print("PreLoadList TestVar", TEST_VAR)
	return {LuaCore, "LuaCore"}
end

LuaCore = {}

function LuaCore:Init()
	UISelectGame:OpenWindow()
end


--防止循环require
LuaCore.tbModule = {}
local oldRequire = require
rawset(_G, "require", function (moduleName)
	local module = LuaCore:GetModule(moduleName)
	if module ~= nil then
		return module
	end

	LuaCore:RegistModule(moduleName)
	return oldRequire(moduleName)
end)

function LuaCore:GetModule(moduleName)
	return self.tbModule[moduleName]
end

function LuaCore:RegistModule(moduleName)
	if self.tbModule[moduleName] ~= nil then
		return self.tbModule[moduleName]
	end

	local t = {}
	t.name = moduleName
	self.tbModule[moduleName] = t
	return t
end

function LuaCore:UnRegistModule(moduleName)
	self.tbModule[moduleName] = nil
end