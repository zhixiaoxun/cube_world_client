using UnityEngine;

namespace Core.Utils
{
    public class PositionHelper
    {
        public static Vector3 WorldPosToBlockPos(Vector3 pos)
        {
            return new Vector3((int)(pos.x / 16), (int)(pos.y / 16), (int)(pos.z / 16));
        }
    }
}
