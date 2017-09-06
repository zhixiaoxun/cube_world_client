require "Core/Lua/Event/LuaEventDispatcher"

LuaInputManager = {}
LuaInputManager.csInputManager = nil

function main()
    return {LuaInputManager, "LuaInputManager"}
end

function LuaInputManager:Init(csInputInstance)
    self.csInputManager = csInputInstance
end

function LuaInputManager:IsMobileMode()
    if self.csInputManager.MobileInput == true then
        return true
    else
        return false
    end
end

function LuaInputManager:SetJoystickVec(vecJoystick)
    self.csInputManager.JoyStickVec = vecJoystick
end

function LuaInputManager:GetJoystickVec()
    return self.csInputManager.JoyStickVec
end

function LuaInputManager:OnTouchStart()
    LuaEventDispatcher:FireEvent(LuaEvent.EVENT_INPUT_TOUCHSTART)
end

function LuaInputManager:OnTouchUp()
    LuaEventDispatcher:FireEvent(LuaEvent.EVENT_INPUT_TOUCHUP)
end

function LuaInputManager:OnSwipeStart()
    LuaEventDispatcher:FireEvent(LuaEvent.EVENT_INPUT_TAP)
end

function LuaInputManager:OnSwipe(deltaPos)
    LuaEventDispatcher:FireEvent(LuaEvent.EVENT_INPUT_LONGTAP, deltaPos)
end

function LuaInputManager:OnSwipeEnd()
    LuaEventDispatcher:FireEvent(LuaEvent.EVENT_INPUT_LONGTAPEND)
end

function LuaInputManager:OnSimpleTap()
    LuaEventDispatcher:FireEvent(LuaEvent.EVENT_INPUT_SWIPESTART)
end

function LuaInputManager:OnLongTap(deltaTime)
    LuaEventDispatcher:FireEvent(LuaEvent.EVENT_INPUT_SWIPE, deltaTime)
end

function LuaInputManager:OnLongTapEnd()
    LuaEventDispatcher:FireEvent(LuaEvent.EVENT_INPUT_SWIPEEND)
end