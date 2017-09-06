require "Core/Lua/Event/LuaEvent.lua"

LuaEventDispatcher = {}
LuaEventDispatcher.tbCallback = {}

function LuaEventDispatcher:RegisterEvent(event, tb, func)
    if not self.tbCallback[event] then
        self.tbCallback[event] = {}
    else
        for _, v in pairs(self.tbCallback[event]) do
            if v.tb == tb and v.func == func then
                return 0
            end
        end
    end

    table.insert(self.tbCallback[event], {tb = tb, func = func})

    return 1
end

function LuaEventDispatcher:UnRegisterEvent(event, tb, func)
    if not self.tbCallback[event] then
        return
    end

    for i,v in pairs(self.tbCallback[event]) do
        if  v.tb == tb and v.func == func then
            self.tbCallback[event][i] = nil
        end
    end

    if Lib:CountTB(self.tbCallback[event]) < 1 then
        self.tbCallback[event] = nil
    end
end

function LuaEventDispatcher:FireEvent(event, args)
    if not self.tbCallback[event] then
        return
    end

    for _, tbEvent in pairs(self.tbCallback[event]) do
        if tbEvent.tb and tbEvent.func then
            tbEvent.func(tbEvent.tb, args)
        end
    end
end