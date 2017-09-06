require "Core/Lua/UI/UIWindow"
require "Core/Lua/Common/Lib"

UIStartLoading = Lib:NewClass(UIWindow)
UIStartLoading.Info = ""
UIStartLoading.Progress = 0

function UIStartLoading:OpenWindow()
	UIWindow.OpenWindow("UIStartLoading")
end

function UIStartLoading:CloseWindow()
	UIWindow.CloseWindow("UIStartLoading")
end

function UIStartLoading:Init(windowClass, trans)
    UIWindow.Init(self, windowClass, trans)
    self.txtLoadingInfo = trans:Find("imgBG/txtLoadingInfo").gameObject:GetComponent("Text")
    self.objLoadingProgressFG = trans:Find("imgBG/pbLoading/imgForceground").gameObject:GetComponent("Image")
end

function UIStartLoading:OnShow()
	UIWindow.OnShow(self)
end

function UIStartLoading:OnHide()
	UIWindow.OnHide(self)
end

function UIStartLoading:SetLoadingInfo(info, progress)
    if info then
        self.Info = info
    end
    if progress then
        self.Progress = progress
    end

    self.txtLoadingInfo.text = string.format("%s...%d%%", self.Info, self.Progress)

    local fScale = progress / 100;
    if fScale > 1 then
        fScale = 1
    end
    self.objLoadingProgressFG.fillAmount = fScale
end