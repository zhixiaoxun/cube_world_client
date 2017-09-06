require "Core/Lua/Common/Lib"

UIWindow = {}
UIWindow.BG_NAME = "imgBG"

function UIWindow:Constructor()
	self.tbTxtMap = {}
	self.tbBtnMap = {}
	self.tbToggleMap = {}
	self.tbDropDownMap = {}
	self.isHide   = true
	self.window   = nil
	self.rectTransform = nil
	self.imgBackGround = nil
	self.uiScaleFactor = 1
	self.curWindowName = nil

	self.tbBtnRedTagMap = {}
end

function UIWindow.OpenWindow(uiName)
	Core.Lua.LuaUIWindow.OpenWindow(uiName, false)
end

function UIWindow.CloseWindow(uiName)
	Core.Lua.LuaUIWindow.CloseWindow(uiName, false)
end

function UIWindow:Init(windowClass, trans)
	self.window = windowClass
	self.rectTransform = trans

	local removeIdx = string.find( trans.name,"(Clone)", 1 , true)
	if removeIdx then
		self.curWindowName = string.sub( trans.name, 1 , removeIdx-1)
	else
		self.curWindowName = trans.name
	end

	self:RegisterWindowElemEvent()

	self:CalcUIScalerFactor()

	if self.OnEntityAttributeChanged then
        LuaEventDispatcher:RegisterEvent(LuaEvent.UI_ATTRIBUTE_CHANGED, self, self.OnEntityAttributeChanged)
	end
end

function UIWindow:UnInit()
	--Debug.Log("UIWindow UnInit......")
	self.window = nil
	self.rectTransform = nil
	self.tbTxtMap = {}
	self.tbBtnMap = {}
	self.tbToggleMap = {}
end

function UIWindow:IsHide()
	--if(self.window ~= nil) then
		return self.isHide
	--end
end

function UIWindow:OnShow()
	--Debug.Log("UIWindow OnShow.......")
	--print("UIWindow:OnShow()UIWindow:OnShow()UIWindow:OnShow()UIWindow:OnShow()UIWindow:OnShow()")
	--print(self.tbTxtMap)
	--print("UIWindow:OnShow()UIWindow:OnShow()UIWindow:OnShow()UIWindow:OnShow()UIWindow:OnShow()")
	self.isHide = false
end

function UIWindow:OnHide()
	--Debug.Log("UIWindow OnHide.......")
	self.isHide = true

	self:ClearChangedIntegerAnim()
end

function UIWindow:Update()
	--Debug.Log("UIWindow Update......")	
end

--function UIWindow:OnEntityAttributeChanged(args)
--end

function UIWindow:RegisterWindowElemEvent()
	--Debug.Log("UIWindow RegisterWindowElemEvent......")

	childrenObjects = self.rectTransform.gameObject:GetComponentsInChildren(RectTransform)
	for v in Slua.iter(childrenObjects) do
		child = v.gameObject
		textComponent = child:GetComponent("Text")
		buttonComponent = child:GetComponent("Button")
		toggleComponent = child:GetComponent("Toggle")
		dropDownComponent = child:GetComponent("Dropdown")
		
		if textComponent ~= nil then
			self.tbTxtMap[child.name] = textComponent

			local textIDComp = textComponent.gameObject:GetComponent("UITextID")
			if textIDComp ~= nil and textIDComp.id > 0 then
			textComponent.text = UIStringData:GetData(textIDComp.id).str
			end
		end
		
		if buttonComponent ~= nil then
			self:RegisterButtonClickEvent(child.name, buttonComponent)
		end

		if toggleComponent ~= nil then
			self:RegisterToggleValueChangedEvent(child.name, toggleComponent)
		end

		if dropDownComponent ~= nil then
			self:RegisterDropDownValueChangedEvent(child.name, dropDownComponent)
		end

		
		guideInfoComp = child:GetComponent("GuideInfo")
		if guideInfoComp ~= nil then
			if guideInfoComp.tag > 0 then
				table.insert( self.tbGuideTagMap, guideInfoComp)
			end
		end
	end
end

function UIWindow:RegisterButtonClickEvent(btnName, buttonComponent)
	self.tbBtnMap[btnName] = buttonComponent
	if buttonComponent ~= nil then
		buttonComponent.onClick:AddListener(
			function()
				self:OnButtonClick(btnName)
			end)
	end

	local redDotTag = buttonComponent.transform:Find("redDotTag")
	if redDotTag then
		self.tbBtnRedTagMap[btnName] = redDotTag.gameObject
	end
end

function UIWindow:RegisterToggleValueChangedEvent(btnName, toggleComponent)
	self.tbToggleMap[btnName] = toggleComponent
	if(toggleComponent ~= nil) then
		toggleComponent.onValueChanged:AddListener(function(bSelected)
														self:OnToggleValueChanged(btnName, bSelected)
												   end)
	end
end

function UIWindow:RegisterDropDownValueChangedEvent(objName, component)
	self.tbDropDownMap[objName] = component
	if(component ~= nil) then
		component.onValueChanged:AddListener(function(value)
													self:OnDropDownValueChanged(objName, value)
												   end)
	end
end

function UIWindow:SetImageBackGround(objName, sprite)
	--Debug.Log("UIWindow SetImageBackGround......")
	
	if(objNmae == nil) then
		imgBackGround.overrideSprite = sprite
	else
		obj = GameObject:Find(objName)
		if(obj ~= nil) then
			image = obj:GetComponent("Image")
			if(image ~= nil) then
				image.overrideSprite = sprite
			end
		end
	end
end

function UIWindow:SetTextVisiable(txtName, isVisiable)
	if(self.tbTxtMap[txtName] ~= nil) then
		self.tbTxtMap[txtName].gameObject:SetActive(isVisiable)
	end
end

function UIWindow:SetText(txtName, txtContent, color)
	--Debug.Log("UIWindow SetText......")
	if type(txtContent) == "number" then
		txtContent = tostring(txtContent)
	end

	if(self.tbTxtMap[txtName] ~= nil) then
		self.tbTxtMap[txtName].text = txtContent
		if(color ~= nil) then
			self.tbTxtMap[txtName].color = color
		end
	end
end

function UIWindow:SetTextColor(txtName, color)
	--Debug.Log("UIWindow SetTextColor......")

	if(self.tbTxtMap[txtName] ~= nil) then
		self.tbTxtMap[txtName].color = color
	end
end

function UIWindow:GetTextColor(txtName)
	if self.tbTxtMap[txtName] ~= nil then
		return self.tbTxtMap[txtName].color
	end
end

function UIWindow:GetText(txtName)
	--Debug.Log("UIWindow GetText......")

	if(self.tbTxtMap[txtName] ~= nil) then
		return self.tbTxtMap[txtName].text
	end
end

function UIWindow:SetButtonText(obj, btnName, txtContent, color)
	--Debug.Log("UIWindow SetButtonText......")
	if type(txtContent) == "number" then
		txtContent = tostring(txtContent)
	end

	local txt = nil

	if(obj ~= nil) then
		txt = obj.transform:Find("Text"):GetComponent("Text")
	else
		if(btnName == nil or self.tbBtnMap[btnName] == nil) then
			return
		end

		txt = self.tbBtnMap[btnName].transform:Find("Text"):GetComponent("Text")
	end

	txt.text = txtContent
	if(color ~= nil) then
		txt.text.color = color
	end
end

function UIWindow:SetButtonImage(obj, btnName, sprite)
	--Debug.Log("UIWindow SetButtonImage......")

	if(obj ~= nil) then
		obj:GetComponent("Image").overrideSprite = sprite
	else
		if(self.tbBtnMap[btnName] ~= nil) then
			self.tbBtnMap[btnName].gameObject:GetComponent("Image").overrideSprite = sprite
		end
	end
end

function UIWindow:SetButtonNormalImage(obj, btnName)
end

function UIWindow:SetButtonDisableImage(obj, btnName)
end

function UIWindow:GetButtonImage(btnName)
	if self.tbBtnMap[btnName] ~= nil then
		return self.tbBtnMap[btnName].gameObject:GetComponent("Image").overrideSprite
	end
end

function UIWindow:SetButtonColor(obj, btnName, color)
	--Debug.Log("UIWindow SetButtonColor......")

	if(obj ~= nil) then
		obj:GetComponent("Image").color = color
	else
		if(self.tbBtnMap[btnName] ~= nil) then
			self.tbBtnMap[btnName].gameObject:GetComponent("Image").color = color
		end
	end
end

function UIWindow:GetButtonColor(obj)
	--Debug.Log("UIWindow GetButtonColor......")

	return obj:GetComponent("Image").color
end

function UIWindow:SetElemScaleByObject(obj, scale)
	--Debug.Log("UIWindow SetElemScale......")

	obj.transform.localScale = Vector3(scale,scale,1)
end

function UIWindow:SetElemScaleByName(btnName, scale)
	--Debug.Log("UIWindow SetElemScale......")
	if self.tbBtnMap[btnName] ~= nil then
		self:SetElemScaleByObject(self.tbBtnMap[btnName].gameObject, scale)
	end
end

function UIWindow:SetButtonVisiable(btnName, isVisiable)
	--Debug.Log("UIWindow SetButtonVisiable......")
	if self.tbBtnMap[btnName] ~= nil then
		self.tbBtnMap[btnName].gameObject:SetActive(isVisiable)
	end
end

function UIWindow:SetButtonEnable(btnName, isEnable)
	if self.tbBtnMap[btnName] ~= nil then
		self.tbBtnMap[btnName].interactable = isEnable
		local colorComp = self.tbBtnMap[btnName].transform:GetComponent("UIBtnTxtColor")
		if colorComp then
			if isEnable then
				for graphicInfo in Slua.iter(colorComp.graphicColorList) do
					graphicInfo.graphic.color = graphicInfo.unselected
				end
			else
				for graphicInfo in Slua.iter(colorComp.graphicColorList) do
					graphicInfo.graphic.color = graphicInfo.selected
				end
			end
		end
	end
end

function UIWindow:SetToggleState(toggleName, isToggleOn)
	self.tbToggleMap[toggleName].isOn = isToggleOn
end

function UIWindow:SetDropDownValue(strName, value)
	if self.tbDropDownMap[strName] ~= nil then
		self.tbDropDownMap[strName].value = value
	end
end

function UIWindow:GetToggleState(toggleName)
	return self.tbToggleMap[toggleName].isOn
end
function UIWindow:OnPointerClick(eventData)
	--Debug.Log("UIWindow OnPointerClick......")
end

function UIWindow:OnButtonClick(strObjName)
end

function UIWindow:OnToggleValueChanged(strObjName, bSelected)
end

function UIWindow:OnDropDownValueChanged(strObjName, value)
end

function UIWindow:OnPointerUp(eventData)
	--Debug.Log("UIWindow OnPointerUp......")
end

function UIWindow:OnPointerDown(eventData)
	--Debug.Log("UIWindow OnPointerDown......")
end

function UIWindow:OnBeginDrag(eventData)
	--Debug.Log("UIWindow OnBeginDrag......")
end

function UIWindow:OnDrag(eventData)
	--Debug.Log("UIWindow OnDrag......")
end

function UIWindow:OnEndDrag(eventData)
	--Debug.Log("UIWindow OnEndDrag......")
end

function UIWindow:GetUIScalerFactor()
	local scaler = GameObject.Find("Canvas"):GetComponent("CanvasScaler")
	local resolutionX = scaler.referenceResolution.x
	local resolutionY = scaler.referenceResolution.y

	return (Screen.width / resolutionX) * (1-scaler.matchWidthOrHeight) + (Screen.height/resolutionY) * scaler.matchWidthOrHeight
end

function UIWindow:CalcUIScalerFactor()
	local scaler = GameObject.Find("Canvas"):GetComponent("CanvasScaler")
	local resolutionX = scaler.referenceResolution.x
	local resolutionY = scaler.referenceResolution.y
	self.uiScaleFactor = (Screen.width / resolutionX) * (1-scaler.matchWidthOrHeight) + (Screen.height/resolutionY) * scaler.matchWidthOrHeight
end

function UIWindow:WorldToUIPoint(worldPos)
	if Camera.main == nil then
		return
	end

	local screenPos = Camera.main:WorldToScreenPoint(worldPos)
	screenPos = screenPos / self.uiScaleFactor

	return Vector2(screenPos.x, screenPos.y)
end

function UIWindow:WorldToUIPoint3D(worldPos)
	if Camera.main == nil then
		return
	end

	local screenPos = Camera.main:WorldToScreenPoint(worldPos)
	screenPos = screenPos / self.uiScaleFactor

	return Vector3(screenPos.x, screenPos.y, 0)
end

--将UI置顶
function UIWindow:BringToTop()
	self.rectTransform:SetAsLastSibling()
end

--将UI置底
function UIWindow:BringToBottom()
	self.rectTransform:SetAsFirstSibling()
end

function UIWindow:SetImageGray(img)
    if img == nil or img.material == nil then
        return
    end

    if self.grayMaterail == nil then
        local shader = AssetLoader.LoadShader("uigray")
        self.grayMaterail = Material(shader)
        self.grayMaterail:SetFloat("_rPercent",0.37)
        self.grayMaterail:SetFloat("_gPercent",0.55)
        self.grayMaterail:SetFloat("_bPercent",0.05)
    end

    img.material = self.grayMaterail
end


----------------------------------------------------------------
-------------数值增减动画-------------
UIWindow.ChangedIntegerQueueList = {}
UIWindow.ChangedIntegerTimerList = {}
function UIWindow:ChangedIntegerAnim(txtComp, animComp, newValue)
	if tonumber(newValue) == nil then
		return
	end

	local instanceId = txtComp.gameObject:GetInstanceID()
	if self.ChangedIntegerInfoQueue[instanceId] == nil then
		self.ChangedIntegerInfoQueue[instanceId] = {}
	end

	if self.ChangedIntegerInfoQueue[instanceId]["timer"] == nil then
		--可以直接播放
		
	else
		--缓存，等待播放
		return
	end

	local instanceId = txtComp.gameObject:GetInstanceID()
	if self.ChangedIntegerQueueList[instanceId] == nil then
		self.ChangedIntegerQueueList[instanceId] = {}
	end

	if #self.ChangedIntegerQueueList[instanceId] >= 1 then
		--插入到最后，等待播放
		table.insert(self.ChangedIntegerQueueList[instanceId], {txtComp, animComp, newValue})
		return
	else
		self.ChangedIntegerQueueList[instanceId] = {}
		self.ChangedIntegerQueueList[instanceId][1] = {txtComp, animComp, newValue}
		self:PlayChangedIntegerAnim(instanceId)
	end
end

function UIWindow:PlayChangedIntegerAnim(instanceId)
	function StopPlay(instanceId)
		local playInfoTb = self.ChangedIntegerQueueList[instanceId][1]

		if playInfoTb[5] then
			TimerHeap.DelTimer(playInfoTb[5])
		end
		
		playInfoTb[1].text = playInfoTb[3]
		if playInfoTb[2] then
			playInfoTb[2]:SetBool("play",false)
		end

		table.remove( self.ChangedIntegerQueueList[instanceId], 1)
		if #self.ChangedIntegerQueueList[instanceId] >= 1 then
			self:PlayChangedIntegerAnim(instanceId)
		end
	end

	--立即播放
	local cachedInfoTb = self.ChangedIntegerQueueList[instanceId][1]
	local oldValue = tonumber(cachedInfoTb[1].text)
	local newValue = cachedInfoTb[3]
	local DValue = math.modf(newValue - oldValue)

	if DValue == 0 then
		StopPlay(instanceId)
		return
	end

	--[[
	local offsetRank = string.len( tostring( math.abs(DValue) ) )
	offsetRank = math.ceil(offsetRank)]]
	
	--判断增减数值
	local negative = 1
	if DValue >= 0 then
		negative = 1
	else
		negative = -1
	end

	local offsetValue = math.ceil( DValue / 8)
	local lastValueNumber = tonumber( string.sub(offsetValue , -1, -1) )
	offsetValue = tonumber(offsetValue)
	if lastValueNumber == 0 then
		offsetValue = offsetValue + 3 * negative
	end
	
	--[[
	if offsetRank > 1 then
		offsetValue = 0
		for i=1 , offsetRank - 1 do
			offsetValue = negative * (10 ^ (i-1)) + offsetValue
		end
	else
		offsetValue = offsetRank * 9
	end]]

	cachedInfoTb[4] = offsetValue

	if cachedInfoTb[2] then
		cachedInfoTb[2]:SetBool("play",true)
	end
	
	cachedInfoTb[5] = TimerHeap.LuaAddTimer(0,GameDefine.changedNumberDuration,
		function(args)
			local playInfoTb = self.ChangedIntegerQueueList[ args[1] ][1]
			local curValue = tonumber(playInfoTb[1].text)
			if math.abs( curValue - playInfoTb[3] ) <= math.abs( playInfoTb[4] ) then
				StopPlay(args[1])
			else
				curValue = math.modf( curValue + playInfoTb[4] )			
				playInfoTb[1].text = curValue
			end
		end,
		{instanceId} )
end


----设置红点提示
function UIWindow:SetRedDotTag(btnStr, isShow)
	if self.tbBtnRedTagMap[btnStr] then
		self.tbBtnRedTagMap[btnStr]:SetActive(isShow)
	end
end

-------------整形数值 增减动画-------------
UIWindow.ChangedIntegerInfoQueue = {}
function UIWindow:AddChangedIntegerAnim(txtComp, animComp, newValue)
	if tonumber(newValue) == nil then
		return
	end

	local instanceId = txtComp.gameObject:GetInstanceID()
	if self.ChangedIntegerInfoQueue[instanceId] == nil then
		self.ChangedIntegerInfoQueue[instanceId] = {}
	end

	local animInfoTb = {}
	animInfoTb[1] = txtComp
	animInfoTb[2] = animComp
	animInfoTb[3] = newValue

	table.insert( self.ChangedIntegerInfoQueue[instanceId], animInfoTb)
	if self.ChangedIntegerInfoQueue[instanceId]["timer"] == nil then
		--可以直接播放
		self:PlayChangedIntegerAnim(instanceId)
	else
		--缓存，等待播放
		return
	end
end

function UIWindow:PlayChangedIntegerAnim(instanceId)
	self.ChangedIntegerInfoQueue[instanceId]["timer"] = TimerHeap.LuaAddTimer(0, 80, 
		function(args)
			local instanceId = args[1]
			local animInfoTb = self.ChangedIntegerInfoQueue[instanceId][1]
			if animInfoTb == nil then
				local timerId = self.ChangedIntegerInfoQueue[instanceId]["timer"]
				self.ChangedIntegerInfoQueue[instanceId]["timer"] = nil
				TimerHeap.DelTimer(timerId)
			else
				if animInfoTb[6] == nil then
					--是否已初始化
					animInfoTb = self:CalIntegerAnimInfo(animInfoTb)
					self.ChangedIntegerInfoQueue[instanceId][1] = animInfoTb

					if animInfoTb[2] ~= nil then
						animInfoTb[2]:SetBool("play",true)
					end
				end

				local preparedShowValue = tonumber(animInfoTb[1].text) + animInfoTb[5]
				if animInfoTb[4] * (preparedShowValue - animInfoTb[3]) >= 0 then
					--结束本次数值变化
					animInfoTb[1].text = animInfoTb[3]
					if animInfoTb[2] ~= nil then
						animInfoTb[2]:SetBool("play",false)
					end
					
					table.remove( self.ChangedIntegerInfoQueue[instanceId], 1)
				else
					--继续数值变化
					animInfoTb[1].text = preparedShowValue
				end
			end
		end, 
		{instanceId}
	)
end
function UIWindow:CalIntegerAnimInfo(animInfoTb)
	local calInfoTb = {}
	calInfoTb[1] = animInfoTb[1]	--txt组件
	calInfoTb[2] = animInfoTb[2]	--animator组件
	calInfoTb[3] = animInfoTb[3]	--最终值

	local curTxtValue = tonumber(animInfoTb[1].text)
	local diffValue = animInfoTb[3] - curTxtValue
	local sign = 1
	if diffValue == 0 then
		--检测
		sign = 0
	elseif diffValue > 0 then
		sign = 1
	elseif diffValue < 0 then
		sign = -1
	end
	calInfoTb[4] = sign

	local eachChangedValue = math.ceil( diffValue / 8 )
	local lastValueNumber = tonumber( string.sub(eachChangedValue , -1, -1) )
	eachChangedValue = tonumber(eachChangedValue)
	if lastValueNumber == 0 then
		eachChangedValue = eachChangedValue + 1*sign
	end
	calInfoTb[5] = eachChangedValue

	calInfoTb[6] = true

	return calInfoTb
end

function UIWindow:ClearChangedIntegerAnim()
	--退出UI时，清理动画，直接把最新数值赋予
	for k,vInfoTbQueue in pairs(self.ChangedIntegerInfoQueue) do
		if vInfoTbQueue.timer ~= nil  then
			TimerHeap.DelTimer(vInfoTbQueue.timer)
			local lastInfoTb = vInfoTbQueue[#vInfoTbQueue]
			if lastInfoTb then
				lastInfoTb[1].text = lastInfoTb[3]
			end

			self.ChangedIntegerInfoQueue[k] = {}
		end
	end
end

