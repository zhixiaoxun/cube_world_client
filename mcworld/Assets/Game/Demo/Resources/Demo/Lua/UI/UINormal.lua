require "Core/Lua/UI/UIWindow"
require "Core/Lua/Common/Lib"
require "Demo/Lua/UI/UISetting"
require "Demo/Lua/Logic/World"

UINormal = Lib:NewClass(UIWindow)
UINormal.BTN_SETTING = "btnSetting"
UINormal.BTN_SHOW_MENU = "btnShowMenu"
UINormal.BTN_SKILL1 = "btnSkill1"
UINormal.BTN_SKILL2 = "btnSkill2"
UINormal.BTN_JUMP = "btnJump"
UINormal.BTN_CONTROL_SETTING = "btnControlSetting"
UINormal.BtnShowMenuObjectTrans = nil
UINormal.ImgMenuObjectTrans = nil
UINormal.ImgTarget = nil
UINormal.IsMenuShow = false

function UINormal:OpenWindow()
	UIWindow.OpenWindow("UINormal")
end

function UINormal:CloseWindow()
	UIWindow.CloseWindow("UINormal")
end

function UINormal:Init(windowClass, trans)
    UIWindow.Init(self, windowClass, trans)

    self.ImgMenuObjectTrans = trans:Find("centerTop/imgMenu")
    self.BtnShowMenuObjectTrans = trans:Find("centerTop/btnShowMenu")
    self.ImgTarget = trans:Find("target")
    self:ShowOrHideMenu(false)
end

function UINormal:OnShow()
	UIWindow.OnShow(self)
    
    if LuaInputManager:IsMobileMode() then
        self.ImgTarget.gameObject:SetActive(false)
    else
        self.ImgTarget.gameObject:SetActive(true)
    end
end

function UINormal:OnHide()
	UIWindow.OnHide(self)
end

function UINormal:OnButtonClick(strObjName)
	UIWindow.OnButtonClick(self, strObjName)
    if strObjName == self.BTN_SETTING then
        UISetting:OpenWindow()
    elseif strObjName == self.BTN_ASK then
    elseif strObjName == self.BTN_SKILL1 then
        World._LocalPlayer:Attack1()
    elseif strObjName == self.BTN_SKILL2 then
        World._LocalPlayer:Attack2()
    elseif strObjName == self.BTN_JUMP then
        World._LocalPlayer:Jump()
    elseif strObjName == self.BTN_CONTROL_SETTING then
        UISetting:OpenWindow()
    elseif strObjName == self.BTN_SHOW_MENU then
        self:ShowOrHideMenu(not self.IsMenuShow)
    end
end

function UINormal:ShowOrHideMenu(isShow)
    if isShow == true then
        self.ImgMenuObjectTrans.gameObject:SetActive(true)
        self.BtnShowMenuObjectTrans.localRotation = Quaternion.Euler(0, 0, 270)
	else
        self.ImgMenuObjectTrans.gameObject:SetActive(false)
	    self.BtnShowMenuObjectTrans.localRotation = Quaternion.Euler(0, 0, 90)
    end
    self.IsMenuShow = isShow
end