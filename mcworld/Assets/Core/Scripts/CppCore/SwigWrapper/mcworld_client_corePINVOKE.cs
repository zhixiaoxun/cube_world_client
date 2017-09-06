#if !UNITY_EDITOR
//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.12
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


class mcworld_client_corePINVOKE {

  protected class SWIGExceptionHelper {

    public delegate void ExceptionDelegate(string message);
    public delegate void ExceptionArgumentDelegate(string message, string paramName);

    static ExceptionDelegate applicationDelegate = new ExceptionDelegate(SetPendingApplicationException);
    static ExceptionDelegate arithmeticDelegate = new ExceptionDelegate(SetPendingArithmeticException);
    static ExceptionDelegate divideByZeroDelegate = new ExceptionDelegate(SetPendingDivideByZeroException);
    static ExceptionDelegate indexOutOfRangeDelegate = new ExceptionDelegate(SetPendingIndexOutOfRangeException);
    static ExceptionDelegate invalidCastDelegate = new ExceptionDelegate(SetPendingInvalidCastException);
    static ExceptionDelegate invalidOperationDelegate = new ExceptionDelegate(SetPendingInvalidOperationException);
    static ExceptionDelegate ioDelegate = new ExceptionDelegate(SetPendingIOException);
    static ExceptionDelegate nullReferenceDelegate = new ExceptionDelegate(SetPendingNullReferenceException);
    static ExceptionDelegate outOfMemoryDelegate = new ExceptionDelegate(SetPendingOutOfMemoryException);
    static ExceptionDelegate overflowDelegate = new ExceptionDelegate(SetPendingOverflowException);
    static ExceptionDelegate systemDelegate = new ExceptionDelegate(SetPendingSystemException);

    static ExceptionArgumentDelegate argumentDelegate = new ExceptionArgumentDelegate(SetPendingArgumentException);
    static ExceptionArgumentDelegate argumentNullDelegate = new ExceptionArgumentDelegate(SetPendingArgumentNullException);
    static ExceptionArgumentDelegate argumentOutOfRangeDelegate = new ExceptionArgumentDelegate(SetPendingArgumentOutOfRangeException);

    [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="SWIGRegisterExceptionCallbacks_mcworld_client_core")]
    public static extern void SWIGRegisterExceptionCallbacks_mcworld_client_core(
                                ExceptionDelegate applicationDelegate,
                                ExceptionDelegate arithmeticDelegate,
                                ExceptionDelegate divideByZeroDelegate, 
                                ExceptionDelegate indexOutOfRangeDelegate, 
                                ExceptionDelegate invalidCastDelegate,
                                ExceptionDelegate invalidOperationDelegate,
                                ExceptionDelegate ioDelegate,
                                ExceptionDelegate nullReferenceDelegate,
                                ExceptionDelegate outOfMemoryDelegate, 
                                ExceptionDelegate overflowDelegate, 
                                ExceptionDelegate systemExceptionDelegate);

    [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="SWIGRegisterExceptionArgumentCallbacks_mcworld_client_core")]
    public static extern void SWIGRegisterExceptionCallbacksArgument_mcworld_client_core(
                                ExceptionArgumentDelegate argumentDelegate,
                                ExceptionArgumentDelegate argumentNullDelegate,
                                ExceptionArgumentDelegate argumentOutOfRangeDelegate);

    static void SetPendingApplicationException(string message) {
      SWIGPendingException.Set(new global::System.ApplicationException(message, SWIGPendingException.Retrieve()));
    }
    static void SetPendingArithmeticException(string message) {
      SWIGPendingException.Set(new global::System.ArithmeticException(message, SWIGPendingException.Retrieve()));
    }
    static void SetPendingDivideByZeroException(string message) {
      SWIGPendingException.Set(new global::System.DivideByZeroException(message, SWIGPendingException.Retrieve()));
    }
    static void SetPendingIndexOutOfRangeException(string message) {
      SWIGPendingException.Set(new global::System.IndexOutOfRangeException(message, SWIGPendingException.Retrieve()));
    }
    static void SetPendingInvalidCastException(string message) {
      SWIGPendingException.Set(new global::System.InvalidCastException(message, SWIGPendingException.Retrieve()));
    }
    static void SetPendingInvalidOperationException(string message) {
      SWIGPendingException.Set(new global::System.InvalidOperationException(message, SWIGPendingException.Retrieve()));
    }
    static void SetPendingIOException(string message) {
      SWIGPendingException.Set(new global::System.IO.IOException(message, SWIGPendingException.Retrieve()));
    }
    static void SetPendingNullReferenceException(string message) {
      SWIGPendingException.Set(new global::System.NullReferenceException(message, SWIGPendingException.Retrieve()));
    }
    static void SetPendingOutOfMemoryException(string message) {
      SWIGPendingException.Set(new global::System.OutOfMemoryException(message, SWIGPendingException.Retrieve()));
    }
    static void SetPendingOverflowException(string message) {
      SWIGPendingException.Set(new global::System.OverflowException(message, SWIGPendingException.Retrieve()));
    }
    static void SetPendingSystemException(string message) {
      SWIGPendingException.Set(new global::System.SystemException(message, SWIGPendingException.Retrieve()));
    }

    static void SetPendingArgumentException(string message, string paramName) {
      SWIGPendingException.Set(new global::System.ArgumentException(message, paramName, SWIGPendingException.Retrieve()));
    }
    static void SetPendingArgumentNullException(string message, string paramName) {
      global::System.Exception e = SWIGPendingException.Retrieve();
      if (e != null) message = message + " Inner Exception: " + e.Message;
      SWIGPendingException.Set(new global::System.ArgumentNullException(paramName, message));
    }
    static void SetPendingArgumentOutOfRangeException(string message, string paramName) {
      global::System.Exception e = SWIGPendingException.Retrieve();
      if (e != null) message = message + " Inner Exception: " + e.Message;
      SWIGPendingException.Set(new global::System.ArgumentOutOfRangeException(paramName, message));
    }

    static SWIGExceptionHelper() {
      SWIGRegisterExceptionCallbacks_mcworld_client_core(
                                applicationDelegate,
                                arithmeticDelegate,
                                divideByZeroDelegate,
                                indexOutOfRangeDelegate,
                                invalidCastDelegate,
                                invalidOperationDelegate,
                                ioDelegate,
                                nullReferenceDelegate,
                                outOfMemoryDelegate,
                                overflowDelegate,
                                systemDelegate);

      SWIGRegisterExceptionCallbacksArgument_mcworld_client_core(
                                argumentDelegate,
                                argumentNullDelegate,
                                argumentOutOfRangeDelegate);
    }
  }

  protected static SWIGExceptionHelper swigExceptionHelper = new SWIGExceptionHelper();

  public class SWIGPendingException {
    [global::System.ThreadStatic]
    private static global::System.Exception pendingException = null;
    private static int numExceptionsPending = 0;

    public static bool Pending {
      get {
        bool pending = false;
        if (numExceptionsPending > 0)
          if (pendingException != null)
            pending = true;
        return pending;
      } 
    }

    public static void Set(global::System.Exception e) {
      if (pendingException != null)
        throw new global::System.ApplicationException("FATAL: An earlier pending exception from unmanaged code was missed and thus not thrown (" + pendingException.ToString() + ")", e);
      pendingException = e;
      lock(typeof(mcworld_client_corePINVOKE)) {
        numExceptionsPending++;
      }
    }

    public static global::System.Exception Retrieve() {
      global::System.Exception e = null;
      if (numExceptionsPending > 0) {
        if (pendingException != null) {
          e = pendingException;
          pendingException = null;
          lock(typeof(mcworld_client_corePINVOKE)) {
            numExceptionsPending--;
          }
        }
      }
      return e;
    }
  }


  protected class SWIGStringHelper {

    public delegate string SWIGStringDelegate(string message);
    static SWIGStringDelegate stringDelegate = new SWIGStringDelegate(CreateString);

    [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="SWIGRegisterStringCallback_mcworld_client_core")]
    public static extern void SWIGRegisterStringCallback_mcworld_client_core(SWIGStringDelegate stringDelegate);

    static string CreateString(string cString) {
      return cString;
    }

    static SWIGStringHelper() {
      SWIGRegisterStringCallback_mcworld_client_core(stringDelegate);
    }
  }

  static protected SWIGStringHelper swigStringHelper = new SWIGStringHelper();


  static mcworld_client_corePINVOKE() {
  }


  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_new_GameInitParam")]
  public static extern global::System.IntPtr new_GameInitParam();

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_GameInitParam_bLogtoStd_set")]
  public static extern void GameInitParam_bLogtoStd_set(global::System.Runtime.InteropServices.HandleRef jarg1, bool jarg2);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_GameInitParam_bLogtoStd_get")]
  public static extern bool GameInitParam_bLogtoStd_get(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_GameInitParam_logCallback_set")]
  public static extern void GameInitParam_logCallback_set(global::System.Runtime.InteropServices.HandleRef jarg1, CppCore.CppLogCallback jarg2);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_GameInitParam_logCallback_get")]
  public static extern CppCore.CppLogCallback GameInitParam_logCallback_get(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_GameInitParam_blockCallback_set")]
  public static extern void GameInitParam_blockCallback_set(global::System.Runtime.InteropServices.HandleRef jarg1, CppCore.CppBlockReceiveCallback jarg2);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_GameInitParam_blockCallback_get")]
  public static extern CppCore.CppBlockReceiveCallback GameInitParam_blockCallback_get(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_GameInitParam_messageCallback_set")]
  public static extern void GameInitParam_messageCallback_set(global::System.Runtime.InteropServices.HandleRef jarg1, CppCore.CppProtocolCallback jarg2);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_GameInitParam_messageCallback_get")]
  public static extern CppCore.CppProtocolCallback GameInitParam_messageCallback_get(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_GameInitParam_serverReadyCallback_set")]
  public static extern void GameInitParam_serverReadyCallback_set(global::System.Runtime.InteropServices.HandleRef jarg1, CppCore.CppServerReadyCallback jarg2);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_GameInitParam_serverReadyCallback_get")]
  public static extern CppCore.CppServerReadyCallback GameInitParam_serverReadyCallback_get(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_delete_GameInitParam")]
  public static extern void delete_GameInitParam(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_new_LoginParam")]
  public static extern global::System.IntPtr new_LoginParam();

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_LoginParam_uid_set")]
  public static extern void LoginParam_uid_set(global::System.Runtime.InteropServices.HandleRef jarg1, uint jarg2);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_LoginParam_uid_get")]
  public static extern uint LoginParam_uid_get(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_LoginParam_name_set")]
  public static extern void LoginParam_name_set(global::System.Runtime.InteropServices.HandleRef jarg1, string jarg2);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_LoginParam_name_get")]
  public static extern string LoginParam_name_get(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_LoginParam_roleid_set")]
  public static extern void LoginParam_roleid_set(global::System.Runtime.InteropServices.HandleRef jarg1, uint jarg2);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_LoginParam_roleid_get")]
  public static extern uint LoginParam_roleid_get(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_LoginParam_wid_set")]
  public static extern void LoginParam_wid_set(global::System.Runtime.InteropServices.HandleRef jarg1, int jarg2);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_LoginParam_wid_get")]
  public static extern int LoginParam_wid_get(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_LoginParam_login_session_set")]
  public static extern void LoginParam_login_session_set(global::System.Runtime.InteropServices.HandleRef jarg1, ulong jarg2);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_LoginParam_login_session_get")]
  public static extern ulong LoginParam_login_session_get(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_LoginParam_token_set")]
  public static extern void LoginParam_token_set(global::System.Runtime.InteropServices.HandleRef jarg1, string jarg2);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_LoginParam_token_get")]
  public static extern string LoginParam_token_get(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_delete_LoginParam")]
  public static extern void delete_LoginParam(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_delete_IMapNode")]
  public static extern void delete_IMapNode(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_IMapNode_getContent")]
  public static extern ushort IMapNode_getContent(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_delete_IMapBlock")]
  public static extern void delete_IMapBlock(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_IMapBlock_getNode")]
  public static extern global::System.IntPtr IMapBlock_getNode(global::System.Runtime.InteropServices.HandleRef jarg1, short jarg2, short jarg3, short jarg4);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_delete_IMap")]
  public static extern void delete_IMap(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_IMap_getBlock")]
  public static extern global::System.IntPtr IMap_getBlock(global::System.Runtime.InteropServices.HandleRef jarg1, short jarg2, short jarg3, short jarg4);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_IMap_getNodeNoEx")]
  public static extern global::System.IntPtr IMap_getNodeNoEx(global::System.Runtime.InteropServices.HandleRef jarg1, short jarg2, short jarg3, short jarg4, global::System.Runtime.InteropServices.HandleRef jarg5);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_delete_IItemDefinition")]
  public static extern void delete_IItemDefinition(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_new_IItemDefinition")]
  public static extern global::System.IntPtr new_IItemDefinition();

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_delete_IItemDefManager")]
  public static extern void delete_IItemDefManager(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_IItemDefManager_getDef")]
  public static extern global::System.IntPtr IItemDefManager_getDef(global::System.Runtime.InteropServices.HandleRef jarg1, string jarg2);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_delete_IContentFeatures")]
  public static extern void delete_IContentFeatures(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_IContentFeatures_getName")]
  public static extern string IContentFeatures_getName(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_delete_INodeDefManager")]
  public static extern void delete_INodeDefManager(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_INodeDefManager_getDef__SWIG_0")]
  public static extern global::System.IntPtr INodeDefManager_getDef__SWIG_0(global::System.Runtime.InteropServices.HandleRef jarg1, ushort jarg2);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_INodeDefManager_getDef__SWIG_1")]
  public static extern global::System.IntPtr INodeDefManager_getDef__SWIG_1(global::System.Runtime.InteropServices.HandleRef jarg1, global::System.Runtime.InteropServices.HandleRef jarg2);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_INodeDefManager_getDef__SWIG_2")]
  public static extern global::System.IntPtr INodeDefManager_getDef__SWIG_2(global::System.Runtime.InteropServices.HandleRef jarg1, string jarg2);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_delete_ICraftDefManager")]
  public static extern void delete_ICraftDefManager(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_new_ICraftDefManager")]
  public static extern global::System.IntPtr new_ICraftDefManager();

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_delete_IGameDef")]
  public static extern void delete_IGameDef(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_IGameDef_init")]
  public static extern bool IGameDef_init(global::System.Runtime.InteropServices.HandleRef jarg1, global::System.Runtime.InteropServices.HandleRef jarg2);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_IGameDef_unInit")]
  public static extern bool IGameDef_unInit(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_IGameDef_connect")]
  public static extern bool IGameDef_connect(global::System.Runtime.InteropServices.HandleRef jarg1, string jarg2, ushort jarg3);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_IGameDef_login")]
  public static extern bool IGameDef_login(global::System.Runtime.InteropServices.HandleRef jarg1, global::System.Runtime.InteropServices.HandleRef jarg2);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_IGameDef_ready")]
  public static extern bool IGameDef_ready(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_IGameDef_logout")]
  public static extern bool IGameDef_logout(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_IGameDef_timeSync")]
  public static extern bool IGameDef_timeSync(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_IGameDef_sendMessage")]
  public static extern void IGameDef_sendMessage(global::System.Runtime.InteropServices.HandleRef jarg1, int jarg2, global::System.Runtime.InteropServices.HandleRef jarg3, int jarg4);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_IGameDef_step")]
  public static extern bool IGameDef_step(global::System.Runtime.InteropServices.HandleRef jarg1, float jarg2);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_IGameDef_updatePosition")]
  public static extern bool IGameDef_updatePosition(global::System.Runtime.InteropServices.HandleRef jarg1, short jarg2, short jarg3, short jarg4);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_IGameDef_getMap")]
  public static extern global::System.IntPtr IGameDef_getMap(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_IGameDef_getItemDefManager")]
  public static extern global::System.IntPtr IGameDef_getItemDefManager(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_IGameDef_getNodeDefManager")]
  public static extern global::System.IntPtr IGameDef_getNodeDefManager(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_IGameDef_getCraftDefManager")]
  public static extern global::System.IntPtr IGameDef_getCraftDefManager(global::System.Runtime.InteropServices.HandleRef jarg1);

  [global::System.Runtime.InteropServices.DllImport("mcworld_client_core", EntryPoint="CSharp_CreateGameDef")]
  public static extern global::System.IntPtr CreateGameDef();
}

#endif