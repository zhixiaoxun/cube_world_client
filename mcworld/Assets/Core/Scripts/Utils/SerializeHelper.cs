using System.IO;
using Core.Utils.Binary;

namespace Core.Utils
{
    public class SerializeHelper
    {
        public static byte[] DeSerializeLongString(byte[] buf)
        {
            BeBinaryReader br = new BeBinaryReader(new MemoryStream(buf));
            uint size = br.ReadUInt32();
            byte[] data = br.ReadBytes((int)size);
            br.Close();

            return data;
        }
    }
}
