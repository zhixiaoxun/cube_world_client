//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.12
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class IMap : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal IMap(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(IMap obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~IMap() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          mcworld_client_corePINVOKE.delete_IMap(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public virtual IMapBlock getBlock(short x, short y, short z) {
    global::System.IntPtr cPtr = mcworld_client_corePINVOKE.IMap_getBlock(swigCPtr, x, y, z);
    IMapBlock ret = (cPtr == global::System.IntPtr.Zero) ? null : new IMapBlock(cPtr, false);
    return ret;
  }

  public virtual IMapNode getNodeNoEx(short x, short y, short z, SWIGTYPE_p_bool is_valid_position) {
    IMapNode ret = new IMapNode(mcworld_client_corePINVOKE.IMap_getNodeNoEx(swigCPtr, x, y, z, SWIGTYPE_p_bool.getCPtr(is_valid_position)), false);
    return ret;
  }

}