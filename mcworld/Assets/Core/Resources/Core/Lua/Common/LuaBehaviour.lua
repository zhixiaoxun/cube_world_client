require "Core/Lua/Common/Lib"

LuaBehaviour = {}

function LuaBehaviour.AddBehaviour(gameObject, behaviourTable)
    local compTable = Lib:NewClass(behaviourTable)
	local comp = gameObject:AddComponent(Core.Lua.LuaComponent) --LuaComponent is C# class
	comp:RegisterLuaTable(compTable)
    return compTable
end

function LuaBehaviour.AddBehaviourTable(gameObject, behaviourTable)
	local comp = gameObject:AddComponent(Core.Lua.LuaComponent) --LuaComponent is C# class
	comp:RegisterLuaTable(behaviourTable)
end

function LuaBehaviour:Constructor()
end

-- 派生类需要由C#驱动执行哪个方法就实现哪个方法

-- function LuaBehaviour:Start()
-- end

-- function LuaBehaviour:Update()
-- end

-- function LuaBehaviour:LateUpdate()
-- end

-- function LuaBehaviour:FixedUpdate()
-- end

-- function LuaBehaviour:EndOfUpdate()
-- end

-- function LuaBehaviour:OnEnable()
-- end

-- function LuaBehaviour:OnDestroy()
-- end