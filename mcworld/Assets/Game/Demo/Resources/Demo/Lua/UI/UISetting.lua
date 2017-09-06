require "Core/Lua/UI/UIWindow"
require "Core/Lua/Common/Lib"
require "Demo/Lua/Logic/World"

UISetting = Lib:NewClass(UIWindow)
UISetting.BTN_RETURNGAME = "btnReturn" --返回游戏
UISetting.BTN_RETURNSETTING = "btnSetting" --系统设置
UISetting.BTN_SAVE = "btnSave" --保存游戏
UISetting.BTN_QUITGAME = "btnQuitGame" --退出游戏

function UISetting:OpenWindow()
	UIWindow.OpenWindow("UISetting")
end

function UISetting:CloseWindow()
	UIWindow.CloseWindow("UISetting")
end

function UISetting:Init(windowClass, trans)
    UIWindow.Init(self, windowClass, trans)
end

function UISetting:OnShow()
	UIWindow.OnShow(self)
end

function UISetting:OnHide()
	UIWindow.OnHide(self)
end

function UISetting:OnButtonClick(strObjName)
	UIWindow.OnButtonClick(self, strObjName)
    if strObjName == self.BTN_RETURNGAME then
        self:CloseWindow()
    elseif strObjName == self.BTN_RETURNSETTING then
    elseif strObjName == self.BTN_SAVE then
        local path = World.world:SaveToFile()
        print("保存成功，文件路径：", path)
        self:CloseWindow()
    elseif strObjName == self.BTN_QUITGAME then
    end
end