require "Core/Lua/UI/UIWindow"
require "Core/Lua/Common/Lib"

UISelectGame = Lib:NewClass(UIWindow)
UISelectGame.BTN_SETTING = "btnSetting"
UISelectGame.BTN_SHOW_MENU = "btnShowMenu"
UISelectGame.BTN_SKILL1 = "btnSkill1"
UISelectGame.BTN_SKILL2 = "btnSkill2"
UISelectGame.BTN_JUMP = "btnJump"
UISelectGame.BTN_CONTROL_SETTING = "btnControlSetting"
UISelectGame.BtnShowMenuObjectTrans = nil
UISelectGame.ImgMenuObjectTrans = nil
UISelectGame.IsMenuShow = false
UISelectGame.ProjectList = {}

function UISelectGame:OpenWindow()
	UIWindow.OpenWindow("UISelectGame")
end

function UISelectGame:CloseWindow()
	UIWindow.CloseWindow("UISelectGame")
end

function UISelectGame:Init(windowClass, trans)
    UIWindow.Init(self, windowClass, trans)

    local btnTemplate = trans:Find("imgBG/GameList/List/GameButtonTemplate").gameObject
    self.ProjectList = Core.Projects.ProjectManager.Instance.ProjectNameList
    for i = 1, self.ProjectList.Count do
		local go = GameObject.Instantiate(btnTemplate).gameObject
		go.transform:SetParent(btnTemplate.transform.parent)
		go.transform.position = btnTemplate.transform.position
		go.transform.localScale = btnTemplate.transform.localScale

        local button = go.transform:Find("btnGame").gameObject
        button.name = self.ProjectList[i-1]
        self:SetButtonText(button, nil, self.ProjectList[i-1])

        --注册事件
		self:RegisterButtonClickEvent(button.name, button:GetComponent("Button"))
	end
	btnTemplate:SetActive(false)
end

function UISelectGame:OnShow()
	UIWindow.OnShow(self)
end

function UISelectGame:OnHide()
	UIWindow.OnHide(self)
end

function UISelectGame:OnButtonClick(strObjName)
	UIWindow.OnButtonClick(self, strObjName)
    for i = 1, self.ProjectList.Count do
        if strObjName == self.ProjectList[i-1] then
            if Core.CoreEntry.Instance:StartGame(strObjName) == true then
                self:CloseWindow()
            end
        end
    end
end
