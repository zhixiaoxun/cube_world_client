using UnityEngine;

namespace Core.Config
{
    public class GlobalSwitches
    {
#if UNITY_EDITOR
        public const bool USE_AB = false;
#else
        public const bool USE_AB = true;
#endif
    }

    public static class MemBufSizes
    {
        public static readonly int Log2ScreenBufSize = 2000;  // log entries cached and shown on screen
        public static readonly int LuaCacheBytesImmediately = 512;  // < 512 means caching directly on first visit
        public static readonly int LuaCacheBytesThreshold = 2048;   // 2048 is the max caching size
        public static readonly int LuaCacheDictSize = 32; // default capacity of the lua code caching dictionary
    }
}
