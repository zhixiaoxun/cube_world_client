using UnityEditor;
using Core.Config;
using Core.Utils.Log;
using System.Collections.Generic;

namespace Assets.Core.Editor.Tools.Map
{
    public class MapTools : EditorWindow
    {
        [MenuItem("扩展工具/Core/地图相关/输出所有block信息到文件")]
        public static void DumpAllBlockInfo()
        {
            Dictionary<int, int> ContentMap = new Dictionary<int, int>();
            foreach (var pairs in CoreEnv._World._BlockManager._BlockMap)
            {
                for (int x = 0; x < 16; x++)
                {
                    for (int y = 0; y < 16; y++)
                    {
                        for (int z = 0; z < 16; z++)
                        {
                            var node = pairs.Value.getNode((short)x, (short)y, (short)z);
                            int content = node.getContent();
                            if (ContentMap.ContainsKey(content))
                                ContentMap[content]++;
                            else
                                ContentMap[content] = 1;
                        }
                    }
                }
                LogHelper.DEBUG("MapTools", "Block x={0} y={1} z={2}", pairs.Key.x, pairs.Key.y, pairs.Key.z);
            }
            foreach (var pairs in ContentMap)
            {
                LogHelper.DEBUG("MapTools", "Node Content {0}={1}", pairs.Key, pairs.Value);
            }
        }
	}
}