require "Core/Lua/Common/LuaBehaviour"
require "Demo/Lua/UI/UINormal"
require "Demo/Lua/UI/UIController"
require "Demo/Lua/UI/UIStartLoading"
require "Demo/Lua/Logic/World"

function main()
	return {LuaGame, "LuaGame"}
end

LuaGame = {}

function LuaGame:Init()
	math.randomseed(os.time()) 

	UIStartLoading:OpenWindow()

	local obj = GameObject("DemoWorld")
	LuaBehaviour.AddBehaviour(obj, World)
end
