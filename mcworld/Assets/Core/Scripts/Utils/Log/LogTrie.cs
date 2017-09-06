using System;
using System.Collections.Generic;

namespace Core.Utils.Log
{
    public class LogTrie
    {
        private string m_str;
        private bool m_terminal;
        private List<LogTrie> m_children;

        // 基于一条类似"foo.bar"这样的路径来构造数据。
        // 允许最后一个部分为通配符"*"来匹配所有，例如："*", "foo.*"。
        public bool AddPath(string path)
        {
            return __addPath(path, 0);
        }

        // 移除一条路径。
        public bool RemovePath(string path)
        {
            return __removePath(path, 0);
        }

        // 查找一条类似"foo.bar"这样的路径是否存在。
        public bool Find(string path)
        {
            return __find(path, 0);
        }

        // 返回Path列表
        public List<string> PathList()
        {
            List<string> pathList = new List<string>();
            __pathList(ref pathList, "");
            return pathList;
        }

        #region "Internal helper functions"
        private LogTrie __findChild(string str, bool useWildcard)
        {
            if (m_children != null)
            {
                for (int i = 0; i < m_children.Count; i++)
                {
                    LogTrie child = m_children[i];
                    if (child.m_str == str || (useWildcard && child.m_str == "*"))
                        return child;
                }
            }
            return null;
        }
        private LogTrie __getChild(string str)
        {
            LogTrie child = __findChild(str, false);
            if (child != null)
                return child;

            if (m_children == null)
                m_children = new List<LogTrie>();

            child = new LogTrie();
            child.m_str = str;

            m_children.Add(child);

            return child;
        }
        private bool __addPath(string path, int startIndex)
        {
            if (path.Length - startIndex <= 0)
                return false;

            // 找到第一个'.'的位置
            int pos = path.IndexOf('.', startIndex);
            if (pos == startIndex)  // 头一个字符就是'.'，视为非法输入
                return false;

            // 得到'.'前面的部分，例如"foo"
            string str;
            if (pos != -1)
                str = path.Substring(startIndex, pos - startIndex);
            else
                str = path.Substring(startIndex, path.Length - startIndex);

            // 不允许非终结节点为"*"
            if (str == "*" && pos != -1)
                return false;

            // 查找str是否为一个已经存在的child，若不存在则新建之
            LogTrie child = __getChild(str);

            if (pos != -1)
            {
                // 递归处理余下的path
                return child.__addPath(path, pos + 1);
            }
            else
            {
                // 置上终结符标志
                child.m_terminal = true;
                return true;
            }
        }
        private bool __removePath(string path, int startIndex)
        {
            if (path.Length - startIndex <= 0)
                return false;

            // 找到第一个'.'的位置
            int pos = path.IndexOf('.', startIndex);
            if (pos == startIndex)  // 头一个字符就是'.'，视为非法输入
                return false;

            // 得到'.'前面的部分，例如"foo"
            string str;
            if (pos != -1)
                str = path.Substring(startIndex, pos - startIndex);
            else
                str = path.Substring(startIndex, path.Length - startIndex);

            // 查找str是否为一个已经存在的child
            LogTrie child = __findChild(str, false);
            if (child == null)
                return false;

            if (pos != -1)
            {
                // 递归处理余下的path
                if (!child.__removePath(path, pos + 1))
                    return false;
            }
            else
            {
                // 去掉终结符标志
                child.m_terminal = false;
            }

            // 移除子节点中的非终结符叶子节点
            for (int i = 0; i < m_children.Count; i++)
            {
                child = m_children[i];
                if (!child.m_terminal && (child.m_children == null || child.m_children.Count == 0))
                {
                    m_children.RemoveAt(i);
                    i--;
                }
            }

            return true;
        }
        private bool __find(string path, int startIndex)
        {
            if (path.Length - startIndex <= 0)
                return false;

            // 找到第一个'.'的位置
            int pos = path.IndexOf('.', startIndex);
            if (pos == startIndex)  // 头一个字符就是'.'，视为非法输入
                return false;

            // 得到'.'前面的部分，例如"foo"
            string str;
            if (pos != -1)
                str = path.Substring(startIndex, pos - startIndex);
            else
                str = path.Substring(startIndex, path.Length - startIndex);

            // 查找str是否为一个已经存在的child
            LogTrie child = __findChild(str, true);
            if (child == null)
                return false;

            if (pos != -1 && child.m_str != "*")
            {
                // 递归处理余下的path
                return child.__find(path, pos + 1);
            }
            else
            {
                // path已经结束，若当前节点是一个终结符节点，则说明找到
                return child.m_terminal;
            }
        }
        private void __pathList(ref List<string> pathList, string baseStr)
        {
            if (m_children == null)
                return;

            for (int i = 0; i < m_children.Count; i++)
            {
                LogTrie child = m_children[i];
                string str = baseStr + child.m_str;
                if (child.m_terminal)
                {
                    pathList.Add(str);
                }
                child.__pathList(ref pathList, str + ".");
            }
        }
        #endregion
    }

}
