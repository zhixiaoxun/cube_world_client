using UnityEngine;
using Core.GameLogic;
using Core.RepresentLogic;

namespace Core.Config
{
    public static class CoreEnv
    {
        public static InputManager inputMngr = null;
        public static World _World = null;
        public static string _CurGameName = "";
        public static CoreEntry CoreDriver = null;
        public static string ApplicationDataPath = null;

        static CoreEnv()
        {
            ApplicationDataPath = Application.dataPath;
        }
    }
}
