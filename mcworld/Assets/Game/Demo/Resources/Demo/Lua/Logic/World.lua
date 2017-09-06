require "Core/Lua/Common/LuaBehaviour"
require "Core/Lua/UI/UIWaitingTip"
require "Demo/Lua/UI/UIStartLoading"
require "Demo/Lua/UI/UINormal"
require "Demo/Lua/UI/UIController"

World = Lib:NewClass(LuaBehaviour)
World._World = nil
World._LocalPlayer = nil
World._ServerIP = "127.0.0.1"
World._ServerPort = 40001
World._WorldInitFlag = false
World._LoginFlag = false

function World:Start()
    local funcOnWorldInited = function(result)
        if not result then
            return
        end

        World._WorldInitFlag = true
        UIStartLoading:SetLoadingInfo("正在初始化场景", 50)
    end

    local funcLoginCallback = function(result, code, info)
        World._LoginFlag = result

        if World._LoginFlag == false then
            UIStartLoading:SetLoadingInfo(string.format("登录失败 错误代码：%d 错误信息：%s", code, info), 100)
            return
        end

        UIStartLoading:SetLoadingInfo("登录成功", 50)
    end

    local funcLocalPlayerCreated = function(localPlayer)
        World._LocalPlayer = localPlayer
        UIStartLoading:SetLoadingInfo("创建主角成功", 100)
        UIWaitingTip:OpenWindow("成功进入游戏。。。")
        Core.Utils.TimerHeap.LuaAddTimer(100, 0, 
            function (arg)
                UIStartLoading:CloseWindow()
                UINormal:OpenWindow()
                UIController:OpenWindow()
                UIWaitingTip:CloseWindow()
            end, 
            {}
        )
    end

    World._World = Core.GameLogic.World()
    World._World:Init(funcOnWorldInited, funcLocalPlayerCreated)
    World._World:ConnectServer(World._ServerIP, World._ServerPort)
    World._World:LoginServer(38364804, "aaaaa", 1, 77777, 1, "xxxxx", funcLoginCallback)

end

function World:Update()
    -- if not World.world._LoadingFinished then
    --     UIStartLoading:SetLoadingInfo(World.world._LoadingInfo, World.world._LoadingProgress)
    -- end
    if World._WorldInitFlag == true then
        World._World:Active()
    end
end

