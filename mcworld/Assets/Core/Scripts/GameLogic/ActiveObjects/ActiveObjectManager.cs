using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Core.Utils.Log;
using Core.Utils;

namespace Core.GameLogic.ActiveObjects
{
    public class ActiveObjectManager
    {
        public GameObject _PlayerSetObject { get; set; } = null;
        public LocalPlayer _LocalPlayer { get; set; } = null;
        public ActiveObjectMessageHandler _ObjectMessageHandler { get; set; } = new ActiveObjectMessageHandler();

        private Dictionary<int, ActiveObject> _ActiveObjectMap = new Dictionary<int, ActiveObject>();
        private World _World = null;

        public void Init(World world)
        {
            _World = world;

            _PlayerSetObject = new GameObject("PlayerSet");
        }

        public void UnInit()
        {
            foreach (var pairs in _ActiveObjectMap)
                pairs.Value.UnInit();

            _ActiveObjectMap.Clear();
        }

        public void Active()
        {
            foreach (var pairs in _ActiveObjectMap)
                pairs.Value.Active();
        }

        public void AddActiveObject(int id, proto_server.s2c_object_init_message ao_data)
        {
            if (_ActiveObjectMap.ContainsKey(id))
            {
                LogHelper.ERROR("ActiveObjectManager", "AddActiveObject id={0} allready exist!", id);
                return;
            }

            //LogHelper.DEBUG("HandleMsgActiveObjectRemoveAdd", "Add uid={0} id={1} name={2} local={3}",
            //    ao_data.uid, id, ao_data.properties.name, IsLocalPlayer(ao_data.properties.name));

            ao_data.properties.mesh = LocalPlayerDef.DefaultPrefab; // 用临时资源

            Player player = null;
            if (IsLocalPlayer(ao_data.properties.name))
            {
                // 如果是localplayer，暂时用本地设定的坐标，目前同步下来的是000
                ao_data.movemont.pos.x = LocalPlayerDef.DefaultPos.x * 10;
                ao_data.movemont.pos.y = LocalPlayerDef.DefaultPos.y * 10;
                ao_data.movemont.pos.z = LocalPlayerDef.DefaultPos.z * 10;
                player = CreateLocalPlayer(this, id, ao_data);
                _World._LocalPlayerServerID = id;
                _LocalPlayer = player as LocalPlayer;
            }
            else
            {
                player = CreatePlayer(this, id, ao_data);
            }

            if (player != null)
                _ActiveObjectMap[id] = player;

        }

        public void RemoveActiveObject(int id)
        {
            if (!_ActiveObjectMap.ContainsKey(id))
            {
                LogHelper.ERROR("ActiveObjectManager", "RemoveActiveObject id={0} not exist!", id);
                return;
            }
            _ActiveObjectMap[id].UnInit();
            _ActiveObjectMap.Remove(id);
           // LogHelper.DEBUG("HandleMsgActiveObjectRemoveAdd", "RemoveList item={0}", id);
        }

        public void ProcessActiveObjectMessage(uint type, byte[] data)
        {
            switch (type)
            {
                case (uint)ActiveObjectMessage.GENERIC_CMD_SET_PROPERTIES:
                    {
                        proto_server.s2c_object_properties pack = ProtoBuf.Serializer.Deserialize<proto_server.s2c_object_properties>(new MemoryStream(data));
                        if (!_ActiveObjectMap.ContainsKey(pack.aid))
                        {
                            LogHelper.ERROR("ActiveObjectManager", "ProcessActiveObjectMessage GENERIC_CMD_SET_PROPERTIES aid={0} not exist!", pack.aid);
                            return;
                        }
                        _ObjectMessageHandler.ProcessProperty(_ActiveObjectMap[pack.aid], pack);
                        //LogHelper.DEBUG("ActiveObjectManager", "ProcessActiveObjectMessage GENERIC_CMD_SET_PROPERTIES aid={0} size={1}", pack.aid, data.Length);
                    }
                    break;
                case (uint)ActiveObjectMessage.GENERIC_CMD_UPDATE_MOVEMENT:
                    {
                        proto_server.s2c_object_movemont pack = ProtoBuf.Serializer.Deserialize<proto_server.s2c_object_movemont>(new MemoryStream(data));
                        if (!_ActiveObjectMap.ContainsKey(pack.aid))
                        {
                            LogHelper.ERROR("ActiveObjectManager", "ProcessActiveObjectMessage GENERIC_CMD_UPDATE_MOVEMENT aid={0} not exist!", pack.aid);
                            return;
                        }
                        _ObjectMessageHandler.ProcessMovemont(_ActiveObjectMap[pack.aid], pack);
                        //LogHelper.DEBUG("ActiveObjectManager", "ProcessActiveObjectMessage GENERIC_CMD_UPDATE_MOVEMENT aid={0} size={1}", pack.aid, data.Length);
                    }
                    break;
                case (uint)ActiveObjectMessage.GENERIC_CMD_UPDATE_YAW_PITCH:
                    {
                        proto_server.s2c_object_yaw_and_picth pack = ProtoBuf.Serializer.Deserialize<proto_server.s2c_object_yaw_and_picth>(new MemoryStream(data));
                        if (!_ActiveObjectMap.ContainsKey(pack.aid))
                        {
                            LogHelper.ERROR("ActiveObjectManager", "ProcessActiveObjectMessage GENERIC_CMD_UPDATE_YAW_PITCH aid={0} not exist!", pack.aid);
                            return;
                        }
                        _ObjectMessageHandler.ProcessYawPitch(_ActiveObjectMap[pack.aid], pack);
                        //LogHelper.DEBUG("ActiveObjectManager", "ProcessActiveObjectMessage GENERIC_CMD_UPDATE_YAW_PITCH aid={0} size={1}", pack.aid, data.Length);
                    }
                    break;
                case (uint)ActiveObjectMessage.GENERIC_CMD_UPDATE_CONTROL:
                    {
                        proto_server.s2c_object_control_bits pack = ProtoBuf.Serializer.Deserialize<proto_server.s2c_object_control_bits>(new MemoryStream(data));
                        if (!_ActiveObjectMap.ContainsKey(pack.aid))
                        {
                            LogHelper.ERROR("ActiveObjectManager", "ProcessActiveObjectMessage GENERIC_CMD_UPDATE_CONTROL aid={0} not exist!", pack.aid);
                            return;
                        }
                        _ObjectMessageHandler.ProcesssControlBits(_ActiveObjectMap[pack.aid], pack);
                        //LogHelper.DEBUG("ActiveObjectManager", "ProcessActiveObjectMessage GENERIC_CMD_UPDATE_CONTROL aid={0} size={1}", pack.aid, data.Length);
                    }
                    break;
                default:
                    break;
            }
        }

        public LocalPlayer CreateLocalPlayer(ActiveObjectManager manager, int id, proto_server.s2c_object_init_message ao_data)
        {
            LocalPlayer localPlayer = new LocalPlayer(_World);
            localPlayer.Init(manager, id, ao_data);

            return localPlayer;
        }

        public Player CreatePlayer(ActiveObjectManager manager, int id, proto_server.s2c_object_init_message ao_data)
        {
            Player player = new Player(_World);
            player.Init(manager, id, ao_data);

            return player;
        }

        public Bullet CreateBullet(GameObject owner, Vector3 offset)
        {
            GameObject bulletObj = GameObject.Instantiate(Resources.Load("Demo/FX/001/fx_001")) as GameObject;
            bulletObj.transform.position = owner.transform.position + offset;
            BulletComponent bullet = bulletObj.AddComponent<BulletComponent>();
            Vector3 targetPosition = owner.transform.position + owner.transform.forward * 50;
            bullet.Setup(null, 8, targetPosition);

            return null;
        }

        public bool IsLocalPlayer(string playerName)
        {
            return _World._LocalPlayerName == playerName;
        }
    }
}
