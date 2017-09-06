using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

namespace Assets.Editor.Core
{
    public class ProjectPanel : EditorWindow
    {
        private static Rect _WindowRect = new Rect(100, 100, 1600, 600);  //定义窗口位置和宽高

        // 顶工具栏
        private enum ToolBar
        {
            Manage, // 管理
            Create, // 新建
        }
        private string[] _ToolBarNames = { "管理项目", "新建项目" }; // Toolbar 选项
        private int _ToolBarSelect = (int)ToolBar.Manage;  // Toolbar选择的项 
        private string[] _ProjectNameList = null; // 工程列表
        private int _CurProjectIndex = 0; // 当前工程索引

        // 生成平台
        private enum BuildPlatform
        {
            Android,
            Win,
        }
        private string[] _PlatformList = new string[] { "Android", "Windows"};
        private int _BuildPlatform = (int)BuildPlatform.Android; // 生成平台
        
        // 运行框架
        private enum ScriptBackend
        {
            mono,
            il2cpp,
        }
        private string[] _ScriptBackendList = new string[] { "mono", "il2cpp" };
        private int _ScriptBackend = (int)ScriptBackend.mono;

        [MenuItem("扩展工具/Core/项目管理")]
        static void ShowProjectPanel()
        {
			ProjectPanel window = (ProjectPanel)EditorWindow.GetWindow(typeof(ProjectPanel));  //实例化窗口  
            window.position = _WindowRect;  //设置窗口坐标和宽高  
            window.SearchProjects(); //查找所有项目
            window.Show();  //显示窗口
		}
        // 查找现有项目
        void SearchProjects()
        {
            string projectRootpath = Application.dataPath + "/Game/";
            DirectoryInfo dir = new DirectoryInfo(projectRootpath);
            var dirList = dir.GetDirectories().Where(t => t.Name.StartsWith(".") == false);

            List<string> tmp = new List<string>();
            foreach (var subDir in dirList)
            {
                //Debug.Log(subDir.Name);
                tmp.Add(subDir.Name);
            }
            _ProjectNameList = tmp.ToArray();
        }

        void OnGUI()
		{
			ShowUI();
		}

		void ShowUI()
		{
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();

            _ToolBarSelect = GUILayout.Toolbar(_ToolBarSelect, _ToolBarNames, GUILayout.Width(200), GUILayout.Height(25));
            Debug.Log(_ToolBarSelect);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();

            if (_ToolBarSelect == (int)ToolBar.Manage)
            {
                ShowManageProjectUI();
            }
            else if (_ToolBarSelect == (int)ToolBar.Create)
            {
                ShowCreateProjectUI();
            }
        }
        // 新建工程面板
		void ShowCreateProjectUI()
		{
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.Space();

            EditorGUILayout.TextField("项目名称:", "", GUILayout.MaxWidth(260));

            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
		}
        // 工程管理面板
		void ShowManageProjectUI()
		{
            GUILayout.BeginVertical("box");

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            // 工程列表
            GUILayout.Label("选择工程", GUILayout.Width(100), GUILayout.Height(40));
            _CurProjectIndex = EditorGUILayout.Popup(_CurProjectIndex, _ProjectNameList, GUILayout.Width(100), GUILayout.Height(40));
            //Debug.Log(_ProjectNameList[_CurProjectIndex]);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            // 生成app
            GUILayout.BeginVertical("box");
            EditorGUILayout.Space();
            GUILayout.Label("生成App", GUILayout.Width(100), GUILayout.Height(40));
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("选择平台", GUILayout.Width(100), GUILayout.Height(40));
            _BuildPlatform = EditorGUILayout.Popup(_BuildPlatform, _PlatformList, GUILayout.Width(100), GUILayout.Height(40));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("运行环境", GUILayout.Width(100), GUILayout.Height(40));
            _ScriptBackend = EditorGUILayout.Popup(_ScriptBackend, _ScriptBackendList, GUILayout.Width(100), GUILayout.Height(40));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.TextField("PackageName", "com.yy." + _ProjectNameList[_CurProjectIndex].ToLower(), GUILayout.MaxWidth(400));
            EditorGUILayout.Space();
            if (GUILayout.Button("生成App", GUILayout.Width(100), GUILayout.Height(25)))
            {

            }
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            

            EditorGUILayout.EndVertical();
        }
	}
}