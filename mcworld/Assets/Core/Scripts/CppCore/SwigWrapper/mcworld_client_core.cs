//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.12
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class mcworld_client_core {
  public static IGameDef CreateGameDef() {
    global::System.IntPtr cPtr = mcworld_client_corePINVOKE.CreateGameDef();
    IGameDef ret = (cPtr == global::System.IntPtr.Zero) ? null : new IGameDef(cPtr, false);
    return ret;
  }

}