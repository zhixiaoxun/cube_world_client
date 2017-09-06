using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using UnityEngine;
using SLua;

namespace Game.Asset
{
    public class VersionInfo
    {
        public VersionCode GameVersionCode; // 游戏版本号

        public string GameVersion
        {
            get
            {
                return GameVersionCode.ToString();
            }
            set
            {
                GameVersionCode = new VersionCode(value);
            }
        }
        
        public VersionInfo()
        {
            GameVersionCode = new VersionCode("0.0.0");
        }

        public override string ToString()
        {
            return GameVersionCode.ToString();
        }
    }

    /// 版本号 由3个整形数字组成，前两个数字表示程序版本，后一个表示资源版本
    /// 1.2.15 表示程序版本1.2 资源版本15
    public class VersionCode
    {
        private int numCount = 3;
        private List<int> m_tags = new List<int>();

        // version 版本号字符串
        public VersionCode(String version)
        {
            if (string.IsNullOrEmpty(version))
                return;
            var versions = version.Split('.');
            for (int i = 0; i < versions.Length; i++)
            {
                int v;
                if (int.TryParse(versions[i], out v))
                    m_tags.Add(v);
                else
                    m_tags.Add(v);
            }
        }

        // 返回无小数点版本号
        public string ToShortString()
        {
            var sb = new StringBuilder();
            foreach (var item in m_tags)
            {
                sb.Append(item);
            }
            return sb.ToString();
        }

        // 比较程序版本号，自己比参数大，返回1，比参数小，返回-1，相等返回0
        public int CompareProgramVersion(VersionCode info)
        {
            var count = 2; // 只比较前两个数字
            for (int i = 0; i < count; i++)
            {
                if (this.m_tags[i] == info.m_tags[i])
                    continue;
                else
                    return this.m_tags[i] > info.m_tags[i] ? 1 : -1;
            }
            return 0;
        }

        // 比较资源版本号，自己比参数大，返回1，比参数小，返回-1，相等返回0
        public int CompareResourceVersion(VersionCode info)
        {
            if (m_tags[2] == info.m_tags[2])
                return 0;

            return this.m_tags[2] > info.m_tags[2] ? 1 : -1;
        }

        // 提高程序小版本号
        public void UpProgramSmallVersion()
        {
            m_tags[1]++;
        }

        // 提高程序大版本号
        public void UpProgramBigVersion()
        {
            m_tags[0]++;
        }

        // 提高资源版本号
        public void UpResourceVersion()
        {
            m_tags[2]++;
        }

        public int GetBundleVersionCode()
        {
            return m_tags[0] * 100000 + m_tags[1] * 1000 + m_tags[2];
        }

        // 获取版本号字符串
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var item in m_tags)
            {
                sb.AppendFormat("{0}.", item);
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }

    public class VersionManager
    {
        static public VersionInfo LocalVersion; // 本地的版本信息
        static public VersionInfo PkgVersion;   // 程序包中的版本信息

        //static public void InitVersion()
        //{
        //    GetLocalVersion();
        //    GetPkgVersion();
        //}

        //// 是否需要把程序包中的资源导出到本地（是否是新版本第一次运行） 程序包中的程序版本大于本地程序版本
        //static public bool IsNeedFirstExport()
        //{
        //    return PkgVersion.GameVersionCode.CompareProgramVersion(LocalVersion.GameVersionCode) > 0;
        //}

        //// Application.persistentDataPath + "/version.xml"
        //static public VersionInfo GetLocalVersion()
        //{
        //    // 从本地资源（persistent.DataPath里面）中加载version.xml文件内容
        //    var localVersionText = Utils.LoadFile(GameConfig.VersionPath);
        //    // 获取本地的版本信息
        //    LocalVersion = GetVersionInXML(localVersionText);

        //    return LocalVersion;
        //}

        //static public VersionInfo GetPkgVersion()
        //{
        //    // 从程序打包的version.xml里加载文件内容
        //    var pkgVersionText = Resources.Load(GameConfig.VERSION_URL_KEY) as TextAsset;
        //    // 获取程序包中的版本信息
        //    PkgVersion = GetVersionInXML(pkgVersionText.text);

        //    return PkgVersion;
        //}

        //static public string GetGameVersionString()
        //{
        //    if (GameSwitch.ReleaseMode)
        //        return GetLocalVersion().GameVersion;

        //    return GetPkgVersion().GameVersion;
        //}
    }
}
