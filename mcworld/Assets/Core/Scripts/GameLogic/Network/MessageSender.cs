using System.IO;
using Core.GameLogic.ActiveObjects;

namespace Core.GameLogic.Network
{
    public class MessageSender
    {
        private World _World = null;

        public void Init(World world)
        {
            _World = world;
        }

        public void UnInit()
        {

        }

        public void SendMessageToServer(int protocol, byte[] data)
        {
            _World._NetworkManager.EnqueueOutBoundMessage(protocol, data);
        }

        public void SendClientReady()
        {
            proto_client.c2s_client_ready pack = new proto_client.c2s_client_ready();

            MemoryStream ms = new MemoryStream();
            ProtoBuf.Serializer.Serialize<proto_client.c2s_client_ready>(ms, pack);
            SendMessageToServer((int)ToServerCommand.TOSERVER_CLIENT_READY, ms.ToArray());
        }

        public void SendInteract(int action)
        {
            proto_client.c2s_interact pack = new proto_client.c2s_interact();
            pack.action = (uint)action;
            pack.item = 0;
            pack.pointed = new byte[5];

            MemoryStream ms = new MemoryStream();
            ProtoBuf.Serializer.Serialize<proto_client.c2s_interact>(ms, pack);
            SendMessageToServer((int)ToServerCommand.TOSERVER_INTERACT, ms.ToArray());
        }

        public void SendPlayerPos(LocalPlayer localPlayer)
        {
            proto_client.c2s_player_pos pack = new proto_client.c2s_player_pos();
            pack.posx = (int)(localPlayer._CurrentPos.x * 100); // 乘100为了消除浮点数，到达服务端会再除100
            pack.posy = (int)(localPlayer._CurrentPos.y * 100);
            pack.posz = (int)(localPlayer._CurrentPos.z * 100);
            pack.speedx = (int)(localPlayer._CurrentSpeed.x * 100);
            pack.speedy = (int)(localPlayer._CurrentSpeed.y * 100);
            pack.speedz = (int)(localPlayer._CurrentSpeed.z * 100);

            MemoryStream ms = new MemoryStream();
            ProtoBuf.Serializer.Serialize<proto_client.c2s_player_pos>(ms, pack);
            SendMessageToServer((int)ToServerCommand.TOSERVER_PLAYERPOS, ms.ToArray());
        }

        public void SendLogout()
        {
            proto_client.c2s_logout pack = new proto_client.c2s_logout();
            pack.param = 1;

            MemoryStream ms = new MemoryStream();
            ProtoBuf.Serializer.Serialize<proto_client.c2s_logout>(ms, pack);
            SendMessageToServer((int)ToServerCommand.TOSERVER_LOGOUT, ms.ToArray());
        }

        public void SendTimeSync()
        {
            proto_client.c2s_time_syn pack = new proto_client.c2s_time_syn();
            pack.client_ts = (ulong)System.Environment.TickCount;

            MemoryStream ms = new MemoryStream();
            ProtoBuf.Serializer.Serialize<proto_client.c2s_time_syn>(ms, pack);
            SendMessageToServer((int)ToServerCommand.TOSERVER_TIME_SYN, ms.ToArray());
        }

        public void SendModsData()
        {
            //TOSERVER_MODSDATA
        }

        public void SendPlayerKeyPress(int keyCode)
        {
            proto_client.c2s_player_keypress pack = new proto_client.c2s_player_keypress();
            pack.keypressed = (uint)keyCode;

            MemoryStream ms = new MemoryStream();
            ProtoBuf.Serializer.Serialize<proto_client.c2s_player_keypress>(ms, pack);
            SendMessageToServer((int)ToServerCommand.TOSERVER_PLAYER_KEYPRESS, ms.ToArray());
        }
    }
}
