using UnityEngine;

namespace Core.GameLogic.ActiveObjects
{
    public enum ActiveObjectMessage
    {
        GENERIC_CMD_SET_PROPERTIES = 0,
        GENERIC_CMD_UPDATE_MOVEMENT = 1,
        GENERIC_CMD_UPDATE_YAW_PITCH = 2,
        GENERIC_CMD_UPDATE_CONTROL = 3,
    }

    public class LocalPlayerDef
    {
        public static string DefaultPrefab = "Core/Avatar/Player/001";
        public static Vector3 DefaultPos = new Vector3(37, 16, 25);
        public static int PlayerUpdatePositionInterval = 50; // ms
    }
}
