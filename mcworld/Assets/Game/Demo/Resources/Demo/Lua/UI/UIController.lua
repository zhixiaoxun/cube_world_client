require "Core/Lua/UI/UIWindow"
require "Core/Lua/Common/Lib"
require "Demo/Lua/Define"

UIController = Lib:NewClass(UIWindow)
UIController.pressChecker = "presschecker"
UIController.BG_1 = "controllerbg1"
UIController.BG_2 = "controllerbg2"
UIController.uiScaleFactor = 1
UIController.originBG1Pos = GameDefine.VECTOR2_0
UIController.transBG1 = nil
UIController.transBG2 = nil
UIController.IsDraging = false
UIController.direction = GameDefine.VECTOR2_0
UIController.IsMoving = false
UIController.NeedMove = false

function UIController:OpenWindow()
	UIWindow.OpenWindow("UIController");
end

function UIController:CloseWindow()
	UIWindow.CloseWindow("UIController");
end

function UIController:Init(windowClass, trans)
	UIWindow.Init(self, windowClass, trans)
	self.transBG1 = trans:Find(self.pressChecker):Find(self.BG_1)
	self.originBG1Pos = self.transBG1.anchoredPosition
	if self.transBG1 ~= nil then
		self.transBG2 = self.transBG1:Find(self.BG_2)
	end
end

function UIController:OnShow()
	UIWindow.OnShow(self)
end

function UIController:OnHide()
	UIWindow.OnHide(self)
end

function UIController:Reset()
	self.IsDraging = false
	self.direction = Vector2.zero
	Game.GameEnv.IsDraging = false
	Game.GameEnv.ControllerDirection = GameDefine.VECTOR2_0
end

function UIController:OnPointerUp(eventData)
    if self.transBG1 ~= nil then
        self.transBG1.anchoredPosition = self.originBG1Pos
    end

    if self.transBG2 ~= nil then
        self.transBG2.gameObject:SetActive(false)
	end

	--print("UIController:OnPointerUp")
end

function UIController:OnPointerDown(eventData)
	if self.transBG2 ~= nil then
		self.transBG2.gameObject:SetActive(true)
	end

    if self.transBG1 ~= nil then
		local pos = eventData.pressPosition / self.uiScaleFactor
        self.transBG1.anchoredPosition = pos
    end
end

function UIController:OnBeginDrag(eventData)
	self.IsDraging = true
	--Game.GameEnv.IsDraging = true
--	self.NeedMove = true
--	GameWorld.thePlayer:ClearCmdCache()

--	if GameWorld.thePlayer:IsStiff() == true then
--		return
--	end

--	self.IsMoving = true
--	GameWorld.thePlayer:Move()
end

function UIController:OnEndDrag(eventData)
	self.IsDraging = false
	--Game.GameEnv.IsDraging = false
	if self.transBG2 ~= nil then
		self.transBG2.anchoredPosition = Vector2(0, 0)
	end

	LuaInputManager:SetJoystickVec(GameDefine.VECTOR2_0)

--	if self.IsMoving == true then
--		self.IsMoving = false
--		GameWorld.thePlayer:Stand();
--	end
end

function UIController:OnDrag(eventData)
    if self.transBG2 ~= nil then
        local posVec = self.transBG2.anchoredPosition + eventData.delta

        if posVec.x > (self.transBG1.sizeDelta.x / 2) then
            posVec.x = self.transBG1.sizeDelta.x / 2
        elseif posVec.x < -(self.transBG1.sizeDelta.x / 2) then
            posVec.x = -(self.transBG1.sizeDelta.x / 2)
        end

        if posVec.y > (self.transBG1.sizeDelta.y / 2) then
            posVec.y = self.transBG1.sizeDelta.y / 2
        elseif posVec.y < -(self.transBG1.sizeDelta.y / 2) then
            posVec.y = -(self.transBG1.sizeDelta.y / 2)
        end

        self.transBG2.anchoredPosition = posVec
		self.direction = self.transBG2.anchoredPosition.normalized
		LuaInputManager:SetJoystickVec(self.direction)
        --Game.GameEnv.ControllerDirection = self.direction
    end
	
--	if GameWorld.thePlayer.motor.enableStick == true and self.NeedMove == true then
--		if GameWorld.thePlayer:IsStiff() == true then
--			return
--		end

--		self.IsMoving = true
--		GameWorld.thePlayer:Move()
--		self.NeedMove = false
--	end
end
