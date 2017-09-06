using System;
using System.Collections;
using UnityEngine;
using SLua;
using Core.Asset;
using Core.RepresentLogic;
using Core.Utils.Log;
using Core.Utils;

namespace Core.GameLogic.ActiveObjects
{
    [CustomLuaClass]
    public class LocalPlayer : Player
    {
        private long _LastUpdatePositionTime = 0;

        public LocalPlayer(World world) : base(world)
        {
        }

        protected override IEnumerator DoCreateModel(proto_server.s2c_object_init_message ao_data)
        {
            Vector3 pos = new Vector3(ao_data.movemont.pos.x / 10, ao_data.movemont.pos.y / 10, ao_data.movemont.pos.z / 10);
            _GameObject = ResourceManager.Instance.LoadPrefabAssetFromResource(ao_data.properties.mesh);
            _GameObject.AddComponent<CameraController>();
            _GameObject.transform.position = pos;
            _GameObject.name = "LocalPlayer_" + _ID.ToString();
            _GameObject.transform.parent = _ActiveObjectManager._PlayerSetObject.transform;

            _Component = _GameObject.AddComponent<LocalPlayerController>();
            _Component.player = this;

            _GameObject.AddComponent<Uniblocks.ExampleInventory>();
            var debugger = _GameObject.AddComponent<Uniblocks.Debugger>();
            debugger.Flashlight = _GameObject.transform.Find("Flashlight").gameObject;
            debugger.Torch = _GameObject.transform.Find("Torch").gameObject;
            var cameraEventsSender = _GameObject.AddComponent<Uniblocks.CameraEventsSender>();
            cameraEventsSender.Range = 10;
            _GameObject.AddComponent<Uniblocks.ColliderEventsSender>();

            _GameObject.AddComponent<Uniblocks.ChunkLoader>();
            _PlayerAnimator = _GameObject.GetComponent<Animator>();

            _PlayerCharaContrl = _GameObject.GetComponent<CharacterController>();

            UpdatePosition();

            Vector3 rayStartPos = new Vector3(pos.x, pos.y + 100, pos.z);
            Ray ray = new Ray(rayStartPos, Vector3.down);
            RaycastHit hit;
            int count = 60;
            while (!Physics.Raycast(ray, out hit) || count > 0)
            {
                count--;
                yield return 1;
            }

            _GameObject.transform.position = hit.point + new Vector3(0, 1, 0);
            yield return 1;

            // 等待玩家落入指定范围内
            if (Math.Abs(_GameObject.transform.position.y - pos.y) > 1)
                yield return 1;

            _IsReady = true;

            if (_World._OnLocalPlayerCreatedCallback != null)
                _World._OnLocalPlayerCreatedCallback(this);
        }

        public void UpdatePosition()
        {
            // 控制时间间隔
            long timeNow = TimeHelper.DateTimeToUnixTime(DateTime.Now);
            if (timeNow - _LastUpdatePositionTime < LocalPlayerDef.PlayerUpdatePositionInterval)
                return;

            _LastUpdatePositionTime = timeNow;

            _CurrentNodePos = _GameObject.transform.position;

            _LastPos = _CurrentPos;
            _CurrentPos = _GameObject.transform.position * 10;
            if ((IsPosChanged ||IsSpeedChanged) && _IsReady && !_IsDead)
            {
                // 上报pos
                _World._NetworkManager._MessageSender.SendPlayerPos(this);
                //LogHelper.DEBUG("LocalPlayer", "UpdatePosition LastPos={0} NewPos={1}", _LastPos, _CurrentPos);
            }

            _LastSpeed = _CurrentSpeed;

            _LastBlockPos = _CurrentBlockPos;
            _CurrentBlockPos = PositionHelper.WorldPosToBlockPos(_CurrentNodePos);
            if (IsBlockPosChanged/* && _IsReady*/)
            {
                OnLocalPlayerChangeBlock(_CurrentBlockPos);
            }
        }

        public void OnLocalPlayerChangeBlock(Vector3 newBlockPos)
        {
            LogHelper.DEBUG("LocalPlayer", "OnLocalPlayerChangeBlock last={0} new={1}", _LastBlockPos, _CurrentBlockPos);
            _World.UpdatePosition(_CurrentNodePos);

            // 移除半径外的block
        }
    }
}
