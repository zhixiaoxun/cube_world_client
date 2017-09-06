using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace Assets.Core.Editor.Tools.Build
{
    public class SwitchPlatform : EditorWindow
    {
        [MenuItem("扩展工具/Core/构建/切换到Android平台")]
        public static void SwitchPlatformAndroid()
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
            {
                EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
            }
        }

        [MenuItem("扩展工具/Core/构建/切换到iOS平台")]
        public static void SwitchPlatformIOS()
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.iOS)
            {
                EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.iOS, BuildTarget.iOS);
            }
        }

        [MenuItem("扩展工具/Core/构建/切换到Windows平台")]
        public static void SwitchPlatformWin32()
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.StandaloneWindows)
            {
                EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows);
            }
        }
    }
}
