using System;
using System.IO;
using UnityEngine;
using Core.Utils;
using Core.Config;

public partial class WrapHelper
{
    static string _CoreDll = "mcworld_client_core";

    private static IntPtr _libPtr;
    public static IntPtr LibPtr
    {
        get
        {
			//#if UNITY_EDITOR
			//            if (!Application.isPlaying)
			//            {
			//                Debug.LogErrorFormat("WrapHelper API cound not call when not playing");
			//                return IntPtr.Zero;
			//            }
			//#endif

#if UNITY_EDITOR
			if (_libPtr == IntPtr.Zero)
            {
                string dllDir = CoreEnv.ApplicationDataPath.Replace("Assets", "CppCore");
				if (!Directory.Exists(dllDir))
				{
					Directory.CreateDirectory(dllDir);
				}
				DirectoryHelper.CopyFile(dllDir + "/" + _CoreDll + ".dll", Application.dataPath + "/Core/Plugins/x64/" + _CoreDll + ".dll");

                _libPtr = RunningDimensions.NativeHelper.LoadLibrary(dllDir, _CoreDll);
            }
#endif
			return _libPtr;
        }
    }
    public static bool InitLibrary()
    {
#if UNITY_EDITOR
		return LibPtr != IntPtr.Zero;
#else
		return true;
#endif
	}

    public static void FreeLibrary()
    {
#if UNITY_EDITOR
		_libPtr = RunningDimensions.NativeHelper.FreeLibrary(_CoreDll, _libPtr);
#endif
	}
}
