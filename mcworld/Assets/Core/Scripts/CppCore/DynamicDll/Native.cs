#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN) || (UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX)
/*
* Native dll invocation helper by Francis R. Griffiths-Keam
* www.runningdimensions.com/blog
*/

using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEngine;
using Core.Utils.Log;

namespace RunningDimensions
{
    public static class Native
    {
		public static string DelegateSurffix = "_delegate";
        public static Dictionary<string, Delegate> _DelegateCacheMap = new Dictionary<string, Delegate>();

		public static Delegate GetFunction<T>(IntPtr library)
        {
			string funcName = typeof(T).Name.Replace(DelegateSurffix, "");
			//LogHelper.DEBUG("Native", "GetFunction({0})", funcName);

			IntPtr funcPtr = GetProcAddress(library, funcName);
            if (funcPtr == IntPtr.Zero)
            {
                Debug.LogErrorFormat("Could not gain reference to method address: {0}", funcName);
                return null;
            }

            var func = Marshal.GetDelegateForFunctionPointer(funcPtr, typeof(T));
            if (func == null)
            {
                Debug.LogErrorFormat("Could not gain reference to method address: {0}", typeof(T).Name);
                return null;
            }
            return func;
        }

        public static T Invoke<T, T2>(IntPtr library, params object[] pars)
        {
			string funcName = typeof(T2).Name.Replace(DelegateSurffix, "");
			//LogHelper.DEBUG("Native", "Invoke({0})", funcName);

            if (_DelegateCacheMap.ContainsKey(funcName))
                return (T)_DelegateCacheMap[funcName].DynamicInvoke(pars);

            IntPtr funcPtr = GetProcAddress(library, funcName);
            if (funcPtr == IntPtr.Zero)
            {
                Debug.LogErrorFormat("Could not gain reference to method address: {0}", funcName);
                return default(T);
            }

            var func = Marshal.GetDelegateForFunctionPointer(funcPtr, typeof(T2));
            if (func == null)
            {
                Debug.LogErrorFormat("Could not gain reference to method address: {0}", funcName);
                return default(T);
            }
            _DelegateCacheMap[funcName] = func;
            return (T)func.DynamicInvoke(pars);
        }

        public static void Invoke<T>(IntPtr library, params object[] pars)
        {
			string funcName = typeof(T).Name.Replace(DelegateSurffix, "");
            //LogHelper.DEBUG("Native", "Invoke({0})", funcName);

            if (_DelegateCacheMap.ContainsKey(funcName))
            {
                _DelegateCacheMap[funcName].DynamicInvoke(pars);
                return;
            }

            IntPtr funcPtr = GetProcAddress(library, funcName);
            if (funcPtr == IntPtr.Zero)
            {
                Debug.LogErrorFormat("Could not gain reference to method address: {0}", funcName);
                return;
            }

            var func = Marshal.GetDelegateForFunctionPointer(funcPtr, typeof(T));
            if (func == null)
            {
                Debug.LogErrorFormat("Could not gain reference to method address: {0}", funcName);
                return;
            }
            _DelegateCacheMap[funcName] = func;
            func.DynamicInvoke(pars);
        }

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        [DllImport("kernel32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FreeLibrary(IntPtr hModule);
        
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr LoadLibrary(string lpFileName);
        
        [DllImport("kernel32")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);
        
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr SetDllDirectory(string lpPathName);
#endif

#if UNITY_EDITOR_OSX
        [DllImport("dl")]
        private static extern IntPtr dlopen(string path, int mode);

        [DllImport("dl")]
        private static extern IntPtr dlsym(IntPtr module, string symbol);

        [DllImport("dl")]
        private static extern int dlclose(IntPtr module);

        [DllImport("dl")]
        private static extern IntPtr dlerror();

        private static string dllPath = "";

        public static IntPtr LoadLibrary(string fileName)
        {
            const int RTLD_LAZY = 0x1;
            if (fileName[0] != '/')
                fileName = dllPath + fileName + ".dylib";
            var module = dlopen(fileName, RTLD_LAZY);
            if (module == IntPtr.Zero)
                Debug.LogError("LoadLibrary() error " + Marshal.PtrToStringAnsi(dlerror()));
            return module;
        }

        public static IntPtr GetProcAddress(IntPtr hModule, string procedureName)
        {
            var sym = dlsym(hModule, procedureName);
            if (sym == IntPtr.Zero)
                Debug.LogError("GetProcAddress() error " + Marshal.PtrToStringAnsi(dlerror()));
            return sym;
        }

        public static bool FreeLibrary(IntPtr hModule)
        {
            if (dlclose(hModule) == 0)
                return true;

            Debug.LogError("FreeLibrary() error " + Marshal.PtrToStringAnsi(dlerror()));
            return false;
        }

        public static IntPtr SetDllDirectory(string pathName)
        {
            dllPath = pathName;
            if (dllPath[dllPath.Length - 1] != '/')
                dllPath += "/";
            return IntPtr.Zero;
        }

#endif
    }

    public class NativeHelper
    {
        public static IntPtr LoadLibrary(string dllDir, string dllFile)
        {
            Native.SetDllDirectory(dllDir);

            var libPtr = Native.LoadLibrary(dllFile);
#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
            if (libPtr == IntPtr.Zero)
                LogHelper.ERROR("NativeHelper", "Failed to load native library: '{0}/{1}', error:{2}", dllDir, dllFile, Marshal.GetLastWin32Error());
            else
                LogHelper.INFO("NativeHelper", "Native library loaded: '{0}/{1}'", dllDir, dllFile);
#endif
            return libPtr;            
        }

        public static IntPtr FreeLibrary(string dllFile, IntPtr libPtr)
        {
            if (libPtr == IntPtr.Zero)
                return IntPtr.Zero;

            if (Native.FreeLibrary(libPtr))
                LogHelper.INFO("NativeHelper", "Native library:'{0}' successfully unloaded.", dllFile);
            else
                LogHelper.ERROR("NativeHelper", "Native library:'{0}' could not be unloaded.", dllFile);

            return IntPtr.Zero;
        }
    }
}
#endif