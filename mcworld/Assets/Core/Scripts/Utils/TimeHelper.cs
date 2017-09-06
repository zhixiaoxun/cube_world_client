using System;

namespace Core.Utils
{
    public class TimeHelper
    {
        private static DateTime UnixEpochTime = new DateTime(1970, 1, 1);

        // 将DateTime转换为以毫秒为单位的Unix时间
        public static long DateTimeToUnixTime(DateTime dt)
        {
            long ticks = dt.Ticks - UnixEpochTime.Ticks;  // 得到以Unix开始时间为原点的Tick数
            ticks /= 10000;  // Tick的单位是100ns，转换为毫秒
            return ticks;
        }

        // 将以毫秒为单位的Unix时间转换为DateTime
        public static DateTime UnixTimeToDateTime(long ut)
        {
            long ticks = ut * 10000;  // 转换为Tick
            ticks += UnixEpochTime.Ticks;  // 转换为DateTime的时间原点
            return new DateTime(ticks, DateTimeKind.Utc);
        }
    }

}
