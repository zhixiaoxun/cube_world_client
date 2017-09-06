using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.GameLogic.ActiveObjects
{
    public class PlayerControl
    {
        public bool up = false;
        public bool down = false;
        public bool left = false;
        public bool right = false;
        public bool jump = false;
        public bool aux1 = false;
        public bool sneak = false;
        public bool fly = false;
        public bool LMB = false; //left mouse button
        public bool RMB = false; //right mouse button
        public bool swimming = false;
        public bool climbing = false;
        public bool eating = false;
        public bool flying = false;

        public void ParseControlBits(int controlBits)
        {
            up = (controlBits & (0x01 << 0)) != 0;
            down = (controlBits & (0x01 << 1)) != 0;
            left = (controlBits & (0x01 << 2)) != 0;
            right = (controlBits & (0x01 << 3)) != 0;
            jump = (controlBits & (0x01 << 4)) != 0;
            aux1 = (controlBits & (0x01 << 5)) != 0;
            sneak = (controlBits & (0x01 << 6)) != 0;
            LMB = (controlBits & (0x01 << 7)) != 0;
            RMB = (controlBits & (0x01 << 8)) != 0;
            swimming = (controlBits & (0x01 << 9)) != 0;
            climbing = (controlBits & (0x01 << 10)) != 0;
            eating = (controlBits & (0x01 << 11)) != 0;
            flying = (controlBits & (0x01 << 12)) != 0;
        }

        public static int JumpBegin()
        {
            return (0x01 << 4);
        }
        public static int JumpEnd()
        {
            return 0;
        }
    }
}
