require "Core/Lua/UI/UIWindow"
require "Core/Lua/Common/Lib"

UIWaitingTip = Lib:NewClass(UIWindow)
UIWaitingTip.TXT_MSG = "txtWaitingInfo"

function UIWaitingTip:OpenWindow(txtMsg)
	UIWindow.OpenWindow("UIWaitingTip")

    if not txtMsg then
        txtMsg = "未提供提示信息"
    end
	self:SetText(self.TXT_MSG, txtMsg)
end

function UIWaitingTip:CloseWindow()
	UIWindow.CloseWindow("UIWaitingTip")
end
