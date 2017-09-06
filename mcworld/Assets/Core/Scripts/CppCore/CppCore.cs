using System;
using UnityEngine;
using Core.Utils.Log;
using Core.GameLogic;
using Core.Utils;
using LuaInterface;

public class CppCore
{
    public IGameDef _GameDef { get; private set; } = null;

    // delegates for cpp dll
    public delegate void CppLogCallback(int priority, string message);
    public delegate void CppBlockReceiveCallback(short x, short y, short z);
    public delegate void CppProtocolCallback(int protocol, global::System.IntPtr data, int size);
    public delegate void CppServerReadyCallback(bool ready);

    // delegates for logic
    public static Action<Vector3> _OnBlockReceivedDelegate = null;
    public static Action<int, IntPtr, int> _OnMesageReceivedDelegate = null;
    public static Action<bool> _OnServerReadyDelegate = null;

    private World _World = null;

    public void Init(World world)
    {
        _World = world;

        var initParam = new GameInitParam();
        initParam.bLogtoStd = false;
        initParam.logCallback = OnCppLogCallback;
        initParam.blockCallback = OnCppBlockReceiveCallback;
        initParam.messageCallback = OnCppProtocolCallback;
        initParam.serverReadyCallback = OnCppServerReadyCallback;

        _GameDef = mcworld_client_core.CreateGameDef();

        bool initResult = _GameDef.init(initParam);
        LogHelper.DEBUG("CppCore", "Init(InitParam) result={0}", initResult);
    }

    public void UnInit()
    {
        if (_GameDef != null)
        {
            bool result = _GameDef.unInit();
            LogHelper.DEBUG("CppCore", "UnInit result={0}", result);
        }
    }

    [MonoPInvokeCallback(typeof(CppBlockReceiveCallback))]
    private static void OnCppBlockReceiveCallback(short x, short y, short z)
    {
        if (CppCore._OnBlockReceivedDelegate != null)
            CppCore._OnBlockReceivedDelegate(new Vector3(x, y, z));
    }

    private enum CppLogLevel
    {
        Trace = 0,
        Debug = 1,
        Info = 2,
        Warn = 3,
        Error = 4
    }
    [MonoPInvokeCallback(typeof(CppLogCallback))]
    public static void OnCppLogCallback(int priority, string message)
    {
        switch (priority)
        {
            case (int)CppLogLevel.Trace:
            case (int)CppLogLevel.Debug:
                LogHelper.DEBUG("CppCore", message);
                break;
            case (int)CppLogLevel.Info:
                LogHelper.INFO("CppCore", message);
                break;
            case (int)CppLogLevel.Warn:
                LogHelper.WARN("CppCore", message);
                break;
            case (int)CppLogLevel.Error:
                LogHelper.ERROR("CppCore", message);
                break;
        }
    }

    [MonoPInvokeCallback(typeof(CppProtocolCallback))]
    private static void OnCppProtocolCallback(int protocol, IntPtr data, int size)
    {
        if (CppCore._OnMesageReceivedDelegate != null)
            CppCore._OnMesageReceivedDelegate(protocol, data, size);
    }

    [MonoPInvokeCallback(typeof(CppServerReadyCallback))]
    private static void OnCppServerReadyCallback(bool ready)
    {
        LogHelper.DEBUG("CppCore", "ServerReadyCallback ready={0}", ready);
        if (CppCore._OnServerReadyDelegate != null)
            CppCore._OnServerReadyDelegate(ready);
    }

    public IMapBlock GetBlock(Vector3 blockPos)
    {
        return _GameDef.getMap().getBlock((short)blockPos.x, (short)blockPos.y, (short)blockPos.z);
    }

    public string GetNodeName(int content)
    {
        var def = _GameDef.getNodeDefManager().getDef((ushort)content);
        return def.getName();
    }

    public bool ConnectServer(string host, int port)
    {
        bool result = _GameDef.connect(host, (ushort)port);
        LogHelper.DEBUG("CppCore", "Connect({0}:{1}) result={0}", result, host, port);
        return result;
    }

    public bool LoginServer(uint uid, string name, uint roleID, ulong loginSession, int wid, string token)
    {
        var loginParam = new LoginParam();
        loginParam.uid = uid;
        loginParam.name = name;
        loginParam.roleid = roleID;
        loginParam.login_session = loginSession;
        loginParam.wid = wid;
        loginParam.token = token;

        bool result = _GameDef.login(loginParam);
        LogHelper.DEBUG("CppCore", "Login result={0}", result);
        return result;
    }

    public bool SendClientReady()
    {
        return _GameDef.ready();
    }

    public void SendMessageToServer(int protocol, byte[] data)
    {
        IntPtr ptr = UtilsHelper.BytesToIntPtr(data);
        _GameDef.sendMessage(protocol, ptr, data.Length);
    }

    public void UpdatePosition(Vector3 nodePos)
    {
        _GameDef.updatePosition((short)nodePos.x, (short)nodePos.y, (short)nodePos.z);
    }

    public void Active()
    {
        if (_GameDef != null)
        {
            _GameDef.step(System.Environment.TickCount);
        }
    }
}