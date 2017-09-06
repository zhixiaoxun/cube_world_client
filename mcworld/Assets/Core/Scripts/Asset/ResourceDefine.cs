using System;
using UnityEngine;
using Core.Config;

namespace Core.Asset
{
    public class AssetGroup
    {
        public const int Core = 0; // Core资源 不用ab
        public const int Game = 1; // 业务资源 使用ab
    }

    public class AssetLevel
    {
        public const int Immeditately = 0; // 马上就要使用的资源，需要立刻加载.
        public const int WaitAMoment = 1; // 一会就要用的资源，加载了就驻留不释放.
        public const int OnlyDownload = 2; // 不着急使用的资源，加载了就释放，适合提前下载下个场景用到的资源.
        public const int MaxLoadLevel = AssetLevel.OnlyDownload;
    }

    public enum LoaderMode
    {
        Async,
        Sync,
    }

    public class ResourceDefine
    {
        public const string AB_EXT = ".unity3d";// ab资源包后缀名
        public const string AB_FOLDER = "AssetBundles"; // ab包根目录
        public const string CORE_RESOURCE_HEADER = "Core/"; // 用于判断Core资源的路径开头

        // AB包导出的基础路径 在其之后还需要添加游戏名字和平台
        public static string AssetBundleExportPathBase
        {
            get { return $"{Application.streamingAssetsPath}/{AB_FOLDER}";}
        }

        // ex: IOS, Android
        public static string BuildPlatformName
        {
            get { return GetBuildPlatformName(); }
        }

        // for WWW...with file:///xxx
        public static string FileProtocol
        {
            get { return GetFileProtocol(); }
        }

        private static string _unityEditorEditorUserBuildSettingsActiveBuildTarget;
        /// <summary>
        /// UnityEditor.EditorUserBuildSettings.activeBuildTarget, Can Run in any platform~
        /// </summary>
        public static string UnityEditor_EditorUserBuildSettings_activeBuildTarget
        {
            get
            {
                if (Application.isPlaying && !string.IsNullOrEmpty(_unityEditorEditorUserBuildSettingsActiveBuildTarget))
                {
                    return _unityEditorEditorUserBuildSettingsActiveBuildTarget;
                }
                var assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
                foreach (var a in assemblies)
                {
                    if (a.GetName().Name == "UnityEditor")
                    {
                        Type lockType = a.GetType("UnityEditor.EditorUserBuildSettings");
                        var p = lockType.GetProperty("activeBuildTarget");

                        var em = p.GetGetMethod().Invoke(null, new object[] { }).ToString();
                        _unityEditorEditorUserBuildSettingsActiveBuildTarget = em;
                        return em;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Different platform's assetBundles is incompatible.
        /// CosmosEngine put different platform's assetBundles in different folder.
        /// Here, get Platform name that represent the AssetBundles Folder.
        /// </summary>
        /// <returns>Platform folder Name</returns>
        public static string GetBuildPlatformName()
        {
            string buildPlatformName = "Windows"; // default

            if (Application.isEditor)
            {
                var buildTarget = UnityEditor_EditorUserBuildSettings_activeBuildTarget;
                //UnityEditor.EditorUserBuildSettings.activeBuildTarget;
                switch (buildTarget)
                {
                    case "StandaloneOSXIntel":
                    case "StandaloneOSXIntel64":
                    case "StandaloneOSXUniversal":
                        buildPlatformName = "MacOS";
                        break;
                    case "StandaloneWindows": // UnityEditor.BuildTarget.StandaloneWindows:
                    case "StandaloneWindows64": // UnityEditor.BuildTarget.StandaloneWindows64:
                        buildPlatformName = "Windows";
                        break;
                    case "Android": // UnityEditor.BuildTarget.Android:
                        buildPlatformName = "Android";
                        break;
                    case "iPhone": // UnityEditor.BuildTarget.iPhone:
                    case "iOS":
                        buildPlatformName = "iOS";
                        break;
                    default:
                        Performance.Profiler.Assert(false);
                        break;
                }
            }
            else
            {
                switch (Application.platform)
                {
                    case RuntimePlatform.OSXPlayer:
                        buildPlatformName = "MacOS";
                        break;
                    case RuntimePlatform.Android:
                        buildPlatformName = "Android";
                        break;
                    case RuntimePlatform.IPhonePlayer:
                        buildPlatformName = "iOS";
                        break;
                    case RuntimePlatform.WindowsPlayer:
                        buildPlatformName = "Windows";
                        break;
                    default:
                        Performance.Profiler.Assert(false);
                        break;
                }
            }

            return buildPlatformName;
        }

        /// <summary>
        /// On Windows, file protocol has a strange rule that has one more slash
        /// </summary>
        /// <returns>string, file protocol string</returns>
        public static string GetFileProtocol()
        {
            string fileProtocol = "file://";
            if (Application.platform == RuntimePlatform.WindowsEditor ||
                Application.platform == RuntimePlatform.WindowsPlayer)
                fileProtocol = "file:///";

            return fileProtocol;
        }

        public static string GetCoreResourcePathInEditor()
        {
            return $"{CoreEnv.ApplicationDataPath}/Core/Resources/";
        }

        public static string GetGameResourcePathInEditor(string game)
        {
            return $"{CoreEnv.ApplicationDataPath}/Game/{game}/Resources/";
        }
    }
}