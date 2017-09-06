require "Core/Lua/Common/Lib"

LuaNetClient = {}

function LuaNetClient:Connect(addr, port)
	Game.GameEnv.GameNet:Connect(addr, port)
end

function LuaNetClient:AfterConnHook(result)
	-- if result == 0 then
	-- 	--网络连接失败
	-- 	--print("Lua连接服务器失败")
	-- 	UIWaitingTip:CloseWindow()
	-- else
	-- 	--网络连接成功
	-- 	--print("Lua连接服务器成功")
	-- 	if not UILogin.isFacebook then
	-- 		Game.GameEnv.GameNet:GuestUserLogin(GameConfig.localSetting.Passport, GameConfig.localSetting.Password)
	-- 	else
	-- 		Game.GameEnv.GameNet:FBUserLogin(UILogin.facebookUserId)
	-- 	end
	-- end
end

--网络断开事件
function LuaNetClient:OnServerDisconnect()
	-- UIMessageBox:OpenWindowOk(GameStringData:GetData(21507).str, 
	-- 	function () 
	-- 		--LoginProcess:ShowLoginScene()
	-- 		Application.Quit()
	-- 	end
	-- )
end

--用户认证回调 user_id用字符串表示
function LuaNetClient:AfterUserAuth(result, user_id)
	-- UIWaitingTip:CloseWindow()
	-- if result == 0 then
	-- 	--用户认证失败
	-- 	--print("Lua用户认证失败 用户ID=", user_id)
	-- 	--UILogin:CloseWindow()
	-- 	Game.GameEnv.GameNet:Close()
	-- 	UIMessageBox:OpenWindowOk("user auth failed")
	-- else
	-- 	--用户认证成功
	-- 	--print("Lua用户认证成功 用户ID=", user_id)
	-- 	UserInfo.Id = user_id
	-- 	--跳转到选区服界面
	-- 	UILoginGroup:OpenWindow()
	-- end
end

function LuaNetClient:Send(protocol, lua_table)
	-- --Lib:ShowTB(lua_table)
	-- --print("=================================")
	-- local json_str = LuaEngine.Json.encode(lua_table)
	-- --print("LuaNetClient:Send protocol=", protocol, "json_str=", json_str)
	-- --print("=================================")
	-- --local req_table = json.decode(str)
	-- --Lib:ShowTB(req_table)

	-- Game.GameEnv.GameNet:CommonJsonReq(protocol, json_str)
end

function LuaNetClient:Receive(protocol, json_str)
	-- --print("=================LuaNetClient:Receive begin================")
	-- local ack_table = LuaEngine.Json.decode(json_str)
	-- --print("LuaNetClient:Receive protocol=", protocol, "jsonstr=", json_str)
	-- --print("LuaNetClient:Receive ack_table")
	-- --Lib:ShowTB(ack_table)

	-- --Lib:ShowTB(self.proto_process)
	-- local func_name = self.proto_process[protocol]
	-- if not func_name then
	-- 	--print("LuaNetClient:Receive can't find func_name=", func_name)
	-- else
	-- 	local func = self[func_name]
	-- 	if func then
	-- 		func(self, ack_table)
	-- 	else
	-- 		--print("LuaNetClient:Receive can't find proto=", protocol)
	-- 	end
	-- end

	-- --print("=================LuaNetClient:Receive end================")
end
