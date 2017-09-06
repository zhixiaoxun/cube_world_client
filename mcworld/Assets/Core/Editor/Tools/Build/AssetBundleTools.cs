using System.IO;
using System.Collections.Generic;
using UnityEditor;
using Core.Utils.Log;
using Core.Asset;

namespace Assets.Core.Editor.Tools.Build.AssetBundles
{
    public class AssetBundleTools
    {
        static string ResourcesBuildDir
        {
            get
            {
                return "Assets/Game/Demo/Resources/";
            }
        }

        static string GameName = "Demo";

        // 直接将KEngine配置的BundleResources目录整个自动配置名称，因为这个目录本来就是整个导出
        [MenuItem("扩展工具/Core/构建/配置AB资源导出名字")]
        public static void MakeAssetBundleNames()
        {
            var dir = ResourcesBuildDir;

            // Check marked asset bundle whether real
            foreach (var assetGuid in AssetDatabase.FindAssets(""))
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
                var assetImporter = AssetImporter.GetAtPath(assetPath);
                var bundleName = assetImporter.assetBundleName;
                if (string.IsNullOrEmpty(bundleName))
                {
                    continue;
                }
                if (!assetPath.StartsWith(dir))
                {
                    assetImporter.assetBundleName = null;
                }
            }

            // set BundleResources's all bundle name
            foreach (var filepath in Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories))
            {
                if (filepath.EndsWith(".meta")) continue;

                var importer = AssetImporter.GetAtPath(filepath);
                if (importer == null)
                {
                    LogHelper.ERROR("MakeAssetBundleNames", "Not found: {0}", filepath);
                    continue;
                }
                var bundleName = filepath.Substring(dir.Length, filepath.Length - dir.Length);
                importer.assetBundleName = bundleName + ResourceDefine.AB_EXT;
            }

            LogHelper.INFO("MakeAssetBundleNames", "Make all asset name successs!");
        }

        // 清理冗余，即无此资源，却有AssetBundle的
        [MenuItem("扩展工具/Core/构建/清理冗余AB资源")]
        public static void CleanAssetBundlesRedundancies()
        {
            var platformName = ResourceDefine.BuildPlatformName;
            var outputPath = GetExportPath(EditorUserBuildSettings.activeBuildTarget, GameName);
            var srcList = new List<string>(Directory.GetFiles(ResourcesBuildDir, "*.*", SearchOption.AllDirectories));
            for (var i = srcList.Count - 1; i >= 0; i--)
            {
                if (srcList[i].EndsWith(".meta"))
                    srcList.RemoveAt(i);
                else
                    srcList[i] = srcList[i].Replace(ResourcesBuildDir, "").ToLower();
            }

            var toListMap = new Dictionary<string, string>();
            var toList = new List<string>(Directory.GetFiles(outputPath, "*.*", SearchOption.AllDirectories));
            for (var i = toList.Count - 1; i >= 0; i--)
            {
                var filePath = toList[i];

                if (toList[i].EndsWith((".meta")) || toList[i].EndsWith(".manifest"))
                {
                    toList.RemoveAt(i);
                }
                else
                {
                    var rName = toList[i].Replace(outputPath, "");
                    if (rName == platformName || // 排除AB 平台总索引文件,
                        rName == (platformName + ".manifest") ||
                        rName == (platformName + ".meta") ||
                        rName == (platformName + ".manifest.meta"))
                    {
                        toList.RemoveAt(i);
                    }
                    else
                    {
                        // 去掉扩展名，因为AssetBundle额外扩展名
                        toList[i] = Path.ChangeExtension(rName, "");// 会留下最后句点
                        toList[i] = toList[i].Substring(0, toList[i].Length - 1); // 去掉句点

                        toListMap[toList[i]] = filePath;
                    }
                }
            }

            // 删文件和manifest
            for (var i = 0; i < toList.Count; i++)
            {
                if (!srcList.Contains(toList[i]))
                {
                    var filePath = toListMap[toList[i]];
                    var manifestPath = filePath + ".manifest";
                    File.Delete(filePath);
                    LogHelper.WARN("CleanAssetBundlesRedundancies", "Delete... " + filePath);
                    if (File.Exists(manifestPath))
                    {
                        File.Delete(manifestPath);
                        LogHelper.WARN("CleanAssetBundlesRedundancies", "Delete... " + manifestPath);
                    }
                }
            }
        }

        [MenuItem("扩展工具/Core/构建/生成所有平台AB资源")]
        public static void BuildAllAssetBundlesToAllPlatforms()
        {
            var platforms = new Dictionary<BuildTarget, BuildTargetGroup>()
            {
                [BuildTarget.iOS] = BuildTargetGroup.iOS,
                [BuildTarget.Android] = BuildTargetGroup.Android,
                [BuildTarget.StandaloneWindows] = BuildTargetGroup.Standalone,
            };

            // Build all support platforms asset bundle
            var currentBuildTarget = EditorUserBuildSettings.activeBuildTarget;
            platforms.Remove(currentBuildTarget);
            BuildAllAssetBundles();

            foreach (var platform in platforms)
            {
                if (EditorUserBuildSettings.SwitchActiveBuildTarget(platform.Value, platform.Key))
                    BuildAllAssetBundles();
            }

            // revert platform 
            EditorUserBuildSettings.SwitchActiveBuildTarget(platforms[currentBuildTarget], currentBuildTarget);
        }

        [MenuItem("扩展工具/Core/构建/重新生成当前平台AB资源")]
        public static void ReBuildAllAssetBundles()
        {
            var outputPath = GetExportPath(EditorUserBuildSettings.activeBuildTarget, GameName);
            Directory.Delete(outputPath, true);

            LogHelper.INFO("AssetBundleTools", "ReBuildAllAssetBundles Delete folder: " + outputPath);

            BuildAllAssetBundles();
        }

        [MenuItem("扩展工具/Core/构建/生成当前平台AB资源")]
        public static void BuildAllAssetBundles()
        {
            if (EditorApplication.isPlaying)
            {
                LogHelper.ERROR("AssetBundleTools", "BuildAllAssetBundles Cannot build in playing mode! Please stop!");
                return;
            }
            MakeAssetBundleNames();
            var outputPath = GetExportPath(EditorUserBuildSettings.activeBuildTarget, GameName);
            LogHelper.INFO("AssetBundleTools", "Asset bundle start build to: {0}", outputPath);
            BuildAssetBundleOptions buildOptions = BuildAssetBundleOptions.DeterministicAssetBundle;
            //buildOptions |= BuildAssetBundleOptions.ChunkBasedCompression; // lz4
            BuildPipeline.BuildAssetBundles(outputPath, buildOptions, EditorUserBuildSettings.activeBuildTarget);
        }

        /// <summary>
        /// 获取完整的打包路径，并确保目录存在
        /// </summary>
        /// <param name="path"></param>
        /// <param name="buildTarget"></param>
        /// <returns></returns>
        public static string MakeSureExportPath(string path, BuildTarget buildTarget)
        {
            path = GetExportPath(buildTarget, GameName) + path;

            path = path.Replace("\\", "/");

            string exportDirectory = path.Substring(0, path.LastIndexOf('/'));

            if (!System.IO.Directory.Exists(exportDirectory))
                System.IO.Directory.CreateDirectory(exportDirectory);

            return path;
        }

        public static string GetExportPath(BuildTarget platfrom, string game)
        {
            string basePath = ResourceDefine.AssetBundleExportPathBase;
            if (File.Exists(basePath))
            {
                LogHelper.DEBUG("AssetBundleTools", $"GetExportPath 路径配置错误={basePath}");
                throw new System.Exception("路径配置错误");
            }

            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            string path = $"{basePath}/{game}/{ResourceDefine.BuildPlatformName}/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
    }
}
