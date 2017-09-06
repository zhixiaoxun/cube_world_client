using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using Core.Utils.Log;
using SLua;

namespace Core.Projects
{
    [CustomLuaClass]
    public class ProjectManager : Singleton<ProjectManager>
    {
        private List<string> _projectNameList = new List<string>();
        public List<string> ProjectNameList
        {
            get { return _projectNameList; }
        }

        public static new ProjectManager Instance
        {
            get { return _instance; }
        }

        protected override IEnumerator OnInitCoroutine()
        {
#if UNITY_EDITOR
            // 直接分析目录得到项目列表 仅Editor模式生效
            string projectRootpath = Application.dataPath + "/Game/";
            DirectoryInfo dir = new DirectoryInfo(projectRootpath);
            var dirList = dir.GetDirectories().Where(t => t.Name.StartsWith(".") == false);

            foreach (var subDir in dirList)
            {
                _projectNameList.Add(subDir.Name);
                LogHelper.DEBUG("ProjectManager", "found project {0}", subDir.Name);
            }
#else
            // 移动平台上暂时写死吧，以后应该会从服务端拉取
            _projectNameList.Add("Demo");
            LogHelper.DEBUG("ProjectManager", "found project {0}", "Demo");
#endif
            yield return 1;
        }
    }
}
