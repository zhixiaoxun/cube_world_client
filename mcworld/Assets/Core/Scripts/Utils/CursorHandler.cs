using UnityEngine;

namespace Core.Utils
{
    public class CursorHandler
    {
        public static void ReleaseCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public static void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public static bool IsCursorLocked()
        {
            return Cursor.lockState == CursorLockMode.Locked;
        }
    }
}
