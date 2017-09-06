using System;
using System.Net.NetworkInformation;
using UnityEngine;
using SLua;
using Core.GameLogic.ActiveObjects;
using Core.GameLogic.Network;
using Core.GameLogic.Block;
using Core.Utils.Log;
using Core.Config;
using Core.Utils;

namespace Core.GameLogic
{
    [CustomLuaClass]
    public class World
    {
        public int _LoadingProgress { get; private set; } = 0;
		public string _LoadingInfo { get; private set; } = null;
		public bool _LoadingFinished { get; private set; } = false;

        public CppCore _CppCore { get; private set; } = null;
        public NetworkManager _NetworkManager { get; private set; } = null;
        public ActiveObjectManager _ActiveObjectManager { get; private set; } = null;
        public BlockManager _BlockManager { get; private set; } = null;

        private Action<bool> _OnWorldInitCallback  = null;
        private Action<bool, int, string> _OnLoginServerCallback = null;
        public Action<LocalPlayer> _OnLocalPlayerCreatedCallback = null;

        private bool _ServerReadyFlag = false;

        public LocalPlayer _LocalPlayer
        {
            get
            {
                return _ActiveObjectManager._LocalPlayer;
            }
        }
        public string _LocalPlayerName = null;
        public int _LocalPlayerUID = 0;
        public int _LocalPlayerServerID = 0;

        public static World _Instance = null;
        public World()
        {
            _Instance = this;
            CoreEnv._World = this;
        }

        public void Init(Action<bool> OnWorldInitCallback, Action<LocalPlayer> OnLocalPlayerCreatedCallback)
        {
            _CppCore = new CppCore();
            _CppCore.Init(this);

            _NetworkManager = new NetworkManager();
            _NetworkManager.Init(this);

            _ActiveObjectManager = new ActiveObjectManager();
            _ActiveObjectManager.Init(this);

            _BlockManager = new BlockManager();
            _BlockManager.Init(this);

            CppCore._OnBlockReceivedDelegate += _BlockManager.OnBlockReceived;
            CppCore._OnMesageReceivedDelegate += _NetworkManager.OnMessageReceived;
            CppCore._OnServerReadyDelegate += OnServerReady;

            _OnWorldInitCallback = OnWorldInitCallback;
            _OnLocalPlayerCreatedCallback = OnLocalPlayerCreatedCallback;

            if (_OnWorldInitCallback != null)
				_OnWorldInitCallback.Invoke(true);
		}

        public void UnInit()
        {
            Logout();

            if (_BlockManager != null)
                _BlockManager.UnInit();

            if (_ActiveObjectManager != null)
                _ActiveObjectManager.UnInit();

            if (_NetworkManager != null)
                _NetworkManager.UnInit();

            if (_CppCore != null)
				_CppCore.UnInit();
		}

        public void Active()
        {
            _CppCore.Active();

            _NetworkManager.Active();

            _ActiveObjectManager.Active();

            _BlockManager.Active();

            if (_LoadingFinished)
            {
                //float t = Time.deltaTime;
            }
        }

        public bool ConnectServer(string host, int port)
        {
#if !UNITY_EDITOR
            host = "172.20.55.16"; // for test
#endif
            return _CppCore.ConnectServer(host, port);
        }

        public bool LoginServer(uint uid, string name, uint roleID, ulong loginSession, int wid, string token, Action<bool, int, string> callback)
        {
            // for test
            uid = BitConverter.ToUInt32(NetworkInterface.GetAllNetworkInterfaces()[0].GetPhysicalAddress().GetAddressBytes(), 0) % 100000;
            name = "player" + uid.ToString();

            _LocalPlayerUID = (int)uid;
            _LocalPlayerName = name;
            _OnLoginServerCallback = callback;

            LogHelper.DEBUG("World", "LoginServer Request uid={0} name={1}", uid, name);

            return _CppCore.LoginServer(uid, name, roleID, loginSession, wid, token);
        }

        public void Logout()
        {
            if (_NetworkManager != null && _NetworkManager._MessageSender != null)
                _NetworkManager._MessageSender.SendLogout();
        }

        public void UpdatePosition(Vector3 nodePos)
        {
            _CppCore.UpdatePosition(nodePos);
            LogHelper.DEBUG("UpdatePosition", "Request Blocks Center NodePos={0}, BlockPos={1}", nodePos, nodePos / 16);
        }

        private void OnServerReady(bool ready)
        {
            if (ready)
            {
                _NetworkManager._MessageSender.SendClientReady();
            }

            _ServerReadyFlag = ready;
            if (_OnLoginServerCallback != null)
                _OnLoginServerCallback(_ServerReadyFlag, 0, null);
        }

        public void OnAccessDenied(uint code, string info)
        {
            _ServerReadyFlag = false;
            if (_OnLoginServerCallback != null)
                _OnLoginServerCallback(_ServerReadyFlag, (int)code, info);
        }
    }
}
