using System.IO;
using UnityEngine;
using UnityEditor;
using Game.Asset;

namespace Assets.Core.Editor.Tools.Build
{
    public class BuildTools : EditorWindow
	{
        private static string[] scenes =
        {
            "Assets/Core/Core.unity"
        };

        [MenuItem("扩展工具/Core/构建/生成App")]
        public static void BuildApp()
        {
            string buildMode = "release";
            string buildPlatform = "android";
            string buildBackend = "mono";
            string buildVersion = "0.0.0";
            string appName = "";
            string[] args = System.Environment.GetCommandLineArgs();
            foreach (string oneArg in args)
            {
                if (oneArg != null && oneArg.Length > 0)
                {
                    if (oneArg.StartsWith("--app"))
                    {
                        appName = oneArg.Replace("--app=", "");
                    }
                    else if (oneArg.ToLower().Contains("--platform"))
                    {
                        buildPlatform = oneArg.Replace("--platform=", "");
                    }
                    else if (oneArg.ToLower().Contains("--mode"))
                    {
                        buildMode = oneArg.Replace("--mode=", "");
                    }
                    else if (oneArg.ToLower().Contains("--backend"))
                    {
                        buildBackend = oneArg.Replace("--backend=", "");
                    }
                    else if (oneArg.ToLower().Contains("--version"))
                    {
                        buildVersion = oneArg.Replace("--version=", "");
                    }
                }
            }

            BuildOptions buildOption = BuildOptions.None;
            if (buildBackend == "il2cpp") // TODO no use
                buildOption = BuildOptions.Il2CPP;

            if (buildMode == "debug")
            {
                buildOption |= BuildOptions.Development;
                buildOption |= BuildOptions.AllowDebugging;
                buildOption |= BuildOptions.ConnectWithProfiler;
            }

            if (buildPlatform == "android")
            {
                PlayerSettings.bundleVersion = buildVersion;
                VersionCode GameVersion = new VersionCode(buildVersion);
                PlayerSettings.Android.bundleVersionCode = GameVersion.GetBundleVersionCode();// ios上无此选项
                Debug.Log("已将版本号修改为" + buildVersion);

                //PlayerSettings.Android.keystorePass = "Z7hT1XrGaCnx";
                //PlayerSettings.Android.keyaliasPass = "Z7hT1XrGaCnx";
                UnityEngine.Debug.Log("编译Apk开始 " + appName);
                string buildError = BuildPipeline.BuildPlayer(scenes, appName, BuildTarget.Android, buildOption);
                if (string.IsNullOrEmpty(buildError))
                {
                    UnityEngine.Debug.Log("编译Apk成功 " + appName);
                    EditorApplication.Exit(0);
                }
                else
                {
                    UnityEngine.Debug.Log("编译Apk失败 " + appName);
                    EditorApplication.Exit(1);
                }
            }
            else
            {
                if (!Directory.Exists(appName))
                    Directory.CreateDirectory(appName);
                //if (Directory.Exists(productName))
                //	Directory.Delete(productName);
                UnityEngine.Debug.Log("生成iOS XCode工程开始 " + appName);
                BuildPipeline.BuildPlayer(scenes, appName, BuildTarget.iOS, buildOption);
                UnityEngine.Debug.Log("生成iOS XCode工程完成 " + appName);
            }
        }
    }
}
