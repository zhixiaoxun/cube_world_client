using System;
using System.IO;
using System.Collections.Generic;
using Core.Utils.Log;
using Core.GameLogic.ActiveObjects;

namespace Core.GameLogic.Network
{
    public class MessageHandler
    {
        private World _World = null;

        private Dictionary<ToClientCommand, Action<int, MemoryStream>> _ProtocolProcessFuncs = new Dictionary<ToClientCommand, Action<int, MemoryStream>>();

        public void Init(World world)
        {
            _World = world;

            InitMessageHandlers();
        }

        public void UnInit()
        {

        }

        private void InitMessageHandlers()
        {
            _ProtocolProcessFuncs[ToClientCommand.TOCLIENT_TIME_OF_DAY] = HandleMsgTimeOfDay;
            _ProtocolProcessFuncs[ToClientCommand.TOCLIENT_ACCESS_DENIED] = HandleMsgAccessDenied;
            _ProtocolProcessFuncs[ToClientCommand.TOCLIENT_PLAYERPOS] = HandleMsgPlayerPos;
            _ProtocolProcessFuncs[ToClientCommand.TOCLIENT_ACTIVE_OBJECT_REMOVE_ADD] = HandleMsgActiveObjectRemoveAdd;
            _ProtocolProcessFuncs[ToClientCommand.TOCLIENT_ACTIVE_OBJECT_MESSAGES] = HandleMsgActiveObjectMessages;
            _ProtocolProcessFuncs[ToClientCommand.TOCLIENT_PLAY_SOUND] = HandleMsgPlaySound;
            _ProtocolProcessFuncs[ToClientCommand.TOCLIENT_PLAYER_PITCH_YAW] = HandleMsgPlayerYawPitch;
            _ProtocolProcessFuncs[ToClientCommand.TOCLIENT_TIME_SYN] = HandleMsgTimeSync;
            _ProtocolProcessFuncs[ToClientCommand.TOCLIENT_SHOW_TIPS] = HandleMsgShowTips;
            _ProtocolProcessFuncs[ToClientCommand.TOCLIENT_NUMBER_EXT_FIELDS_UPDATE] = HandleMsgUpdateNumberExtFields;
            _ProtocolProcessFuncs[ToClientCommand.TOCLIENT_STRING_EXT_FIELDS_UPDATE] = HandleMsgUpdateStringExtFields;
            _ProtocolProcessFuncs[ToClientCommand.TOCLIENT_MODSDATA] = HandleMsgModsData;
        }

        public void ProcessMessage(int protocol, byte[] data)
        {
            if (protocol < (int)ToClientCommand.TOCLIENT_GAME_BREATH || protocol >= (int)ToClientCommand.TOCLIENT_NUM_MSG_TYPES)
            {
                LogHelper.ERROR("MessageHandler", "UnSupport protocol={0} size={1}", protocol, data.Length);
                return;
            }

            if (!_ProtocolProcessFuncs.ContainsKey((ToClientCommand)protocol))
            {
                LogHelper.ERROR("MessageHandler", "UnRegister protocol={0} size={1}", protocol, data.Length);
                return;
            }

            _ProtocolProcessFuncs[(ToClientCommand)protocol](protocol, new MemoryStream(data));
        }

        private void HandleMsgTimeOfDay(int protocol, MemoryStream ms)
        {
            proto_server.s2c_timeofday pack = ProtoBuf.Serializer.Deserialize<proto_server.s2c_timeofday>(ms);
            LogHelper.DEBUG("HandleMsgTimeOfDay", "toc.TimeOfDay={0} pack.Moon={1} pack.DaySpeed={2}", pack.time_of_day, pack.moon, pack.day_speed);
        }

        private void HandleMsgAccessDenied(int protocol, MemoryStream ms)
        {
            proto_server.s2c_access_denied pack = ProtoBuf.Serializer.Deserialize<proto_server.s2c_access_denied>(ms);
            _World.OnAccessDenied(pack.code, pack.info);
            LogHelper.DEBUG("HandleMsgAccessDenied", "pack.code;={0} pack.info={1}", pack.code, pack.info);
        }

        private void HandleMsgPlayerPos(int protocol, MemoryStream ms)
        {
            proto_server.s2c_object_movemont pack = ProtoBuf.Serializer.Deserialize<proto_server.s2c_object_movemont>(ms);
            //LogHelper.DEBUG("HandleMsgPlayerPos", "pack.pos.x={0} pack.pos.y={1} pack.pos.z={2}", pack.pos.x, pack.pos.y, pack.pos.z);
        }

        private void HandleMsgActiveObjectRemoveAdd(int protocol, MemoryStream ms)
        {
            proto_server.s2c_ao_changed pack = ProtoBuf.Serializer.Deserialize<proto_server.s2c_ao_changed>(ms);
            foreach (var obj in pack.add_list)
            {
                _World._ActiveObjectManager.AddActiveObject(obj.aid, obj.ao_data);
            }
            foreach (var id in pack.remove_list)
            {
                _World._ActiveObjectManager.RemoveActiveObject((int)id);
            }
        }

        private void HandleMsgActiveObjectMessages(int protocol, MemoryStream ms)
        {
            proto_server.s2c_active_object_msgs pack = ProtoBuf.Serializer.Deserialize<proto_server.s2c_active_object_msgs>(ms);

            _World._ActiveObjectManager.ProcessActiveObjectMessage(pack.type, pack.ao_data);

            //LogHelper.DEBUG("HandleMsgActiveObjectMessages", "pack.type={0} pack.ao_data.Length={1}", pack.type, pack.ao_data.Length);
        }

        private void HandleMsgPlaySound(int protocol, MemoryStream ms)
        {
            proto_server.s2c_play_sound pack = ProtoBuf.Serializer.Deserialize<proto_server.s2c_play_sound>(ms);
            
            LogHelper.DEBUG("HandleMsgPlaySound", "pack.category={0} pack.gain={1} pack.id={2} pack.loop={3} pack.name={4} pack.type={5} pack.pos.x={6} pack.pos.y={7} pack.pos.y={8}", 
                pack.category, pack.gain, pack.id, pack.loop, pack.name, pack.type, pack.pos.x, pack.pos.y, pack.pos.y);
        }

        private void HandleMsgPlayerYawPitch(int protocol, MemoryStream ms)
        {
            proto_server.s2c_object_yaw_and_picth pack = ProtoBuf.Serializer.Deserialize<proto_server.s2c_object_yaw_and_picth>(ms);
            LogHelper.DEBUG("HandleMsgPlayerYawPitch", "pack.yaw={0} pack.pitch={1}", pack.yaw, pack.pitch);
        }

        private void HandleMsgTimeSync(int protocol, MemoryStream ms)
        {
            proto_server.s2c_time_syn pack = ProtoBuf.Serializer.Deserialize<proto_server.s2c_time_syn>(ms);
            //s64 ping = GetTimeMillSecond() - result.client_ts();
            //m_Client->setPing((u32)ping);
            LogHelper.DEBUG("HandleMsgTimeSync", "pack.client_ts={0}", pack.client_ts);
        }

        private void HandleMsgShowTips(int protocol, MemoryStream ms)
        {
            proto_server.s2c_show_tips pack = ProtoBuf.Serializer.Deserialize<proto_server.s2c_show_tips>(ms);
            LogHelper.DEBUG("HandleMsgShowTips", "pack.text={0}", pack.text);
        }

        private void HandleMsgUpdateNumberExtFields(int protocol, MemoryStream ms)
        {
            proto_server.s2c_number_ext_fields_update pack = ProtoBuf.Serializer.Deserialize<proto_server.s2c_number_ext_fields_update>(ms);
            _World._LocalPlayer.SetNumberExtField(pack.key, pack.val);
            LogHelper.DEBUG("HandleMsgUpdateNumberExtFields", "pack.key={0} pack.val={1}", pack.key, pack.val);
        }

        private void HandleMsgUpdateStringExtFields(int protocol, MemoryStream ms)
        {
            proto_server.s2c_string_ext_fields_update pack = ProtoBuf.Serializer.Deserialize<proto_server.s2c_string_ext_fields_update>(ms);
            _World._LocalPlayer.SetStringExtField(pack.key, System.Text.Encoding.Default.GetString(pack.val));
            LogHelper.DEBUG("HandleMsgUpdateStringExtFields", "pack.key={0} pack.val={1}", pack.key, pack.val);
        }

        private void HandleMsgModsData(int protocol, MemoryStream ms)
        {
            //proto_server. pack = ProtoBuf.Serializer.Deserialize<proto_server.s2c_string_ext_fields_update>(ms);
            //LogHelper.DEBUG("HandleMsgUpdateStringExtFields", "pack.key={0} pack.val={1}", pack.key, pack.val);
        }
    }
}

