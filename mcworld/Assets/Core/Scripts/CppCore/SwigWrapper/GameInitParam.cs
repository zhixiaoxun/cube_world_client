//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.12
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class GameInitParam : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal GameInitParam(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(GameInitParam obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~GameInitParam() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          mcworld_client_corePINVOKE.delete_GameInitParam(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public GameInitParam() : this(mcworld_client_corePINVOKE.new_GameInitParam(), true) {
  }

  public bool bLogtoStd {
    set {
      mcworld_client_corePINVOKE.GameInitParam_bLogtoStd_set(swigCPtr, value);
    } 
    get {
      bool ret = mcworld_client_corePINVOKE.GameInitParam_bLogtoStd_get(swigCPtr);
      return ret;
    } 
  }

  public CppCore.CppLogCallback logCallback {
    set {
      mcworld_client_corePINVOKE.GameInitParam_logCallback_set(swigCPtr, value);
    } 
get {
  return mcworld_client_corePINVOKE.GameInitParam_logCallback_get(swigCPtr);
} 
  }

  public CppCore.CppBlockReceiveCallback blockCallback {
    set {
      mcworld_client_corePINVOKE.GameInitParam_blockCallback_set(swigCPtr, value);
    } 
get {
  return mcworld_client_corePINVOKE.GameInitParam_blockCallback_get(swigCPtr);
} 
  }

  public CppCore.CppProtocolCallback messageCallback {
    set {
      mcworld_client_corePINVOKE.GameInitParam_messageCallback_set(swigCPtr, value);
    } 
get {
  return mcworld_client_corePINVOKE.GameInitParam_messageCallback_get(swigCPtr);
} 
  }

  public CppCore.CppServerReadyCallback serverReadyCallback {
    set {
      mcworld_client_corePINVOKE.GameInitParam_serverReadyCallback_set(swigCPtr, value);
    } 
get {
  return mcworld_client_corePINVOKE.GameInitParam_serverReadyCallback_get(swigCPtr);
} 
  }

}
