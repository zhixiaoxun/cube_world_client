using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLua;
using Core.RepresentLogic;
using Core.Asset;
using Core.Config;
using Core.Utils;
using Core.Utils.Log;
using Core.GameLogic.ActiveObjects.FSM;

namespace Core.GameLogic.ActiveObjects
{
	[CustomLuaClass]
	public partial class Player : ActiveObject
	{
        public PlayerController _Component { get; protected set; } = null;
        public Animator _PlayerAnimator { get; protected set; } = null;
        public Rigidbody _PlayerRigidbody { get; protected set; } = null;

        private LogicStateManager _LogicStateManager = new LogicStateManager();
        public LogicStateDef _LogicState { get; set; } = LogicStateDef.IDLE;
		public RepresentStateDef _RepresentState { get; set; } = RepresentStateDef.IDLE;
		
        public Dictionary<int, int> _NumberExtFields { get; protected set; } = new Dictionary<int, int>();
        public Dictionary<int, string> _StringExtFields { get; protected set; } = new Dictionary<int, string>();

        public Vector3 _CurrentBlockPos { get; set; } // block坐标=unity坐标/16
        public Vector3 _LastBlockPos { get; set; }
        public bool IsBlockPosChanged { get { return !_CurrentBlockPos.Equals(_LastBlockPos); } }

        public Vector3 _CurrentNodePos { get; set; } // node坐标=unity坐标

        public Vector3 _CurrentPos { get; set; } // 玩家坐标=unity坐标*10=node坐标
        public Vector3 _LastPos { get; set; }
        public bool IsPosChanged { get { return !_CurrentPos.Equals(_LastPos); } }

        public Vector3 _CurrentSpeed { get { return _Component._MoveSpeed * _GameObject.transform.forward.normalized; } }
        public Vector3 _LastSpeed { get; protected set; }
        public bool IsSpeedChanged { get { return !_CurrentSpeed.Equals(_LastSpeed); } }

        public bool _IsDead { get; private set; } = false;
        public int _CurrentSkill { get; private set; } = 1;


        public CharacterController _PlayerCharaContrl { get; protected set; } = null;

        public Player(World world) : base(world)
        {
        }

        protected override IEnumerator DoCreateModel(proto_server.s2c_object_init_message ao_data)
        {
            Vector3 pos = new Vector3(ao_data.movemont.pos.x / 10, ao_data.movemont.pos.y / 10, ao_data.movemont.pos.z / 10);
            _GameObject = ResourceManager.Instance.LoadPrefabAssetFromResource(ao_data.properties.mesh);
            _GameObject.transform.position = pos;
            _GameObject.name = "Player_" + _ID.ToString();
            _GameObject.transform.parent = _ActiveObjectManager._PlayerSetObject.transform;

			_Component = _GameObject.AddComponent<PlayerController>();
			_Component.player = this;

            _PlayerAnimator = _GameObject.GetComponent<Animator>();

            _PlayerCharaContrl = _GameObject.GetComponent<CharacterController>();

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
        }

        public int GetNumberExtField(int key)
        {
            if (_NumberExtFields.ContainsKey(key))
                return _NumberExtFields[key];

            return 0;
        }

        public void SetNumberExtField(int key, int val)
        {
            _NumberExtFields[key] = val;
        }

        public string GetStringExtField(int key)
        {
            if (_StringExtFields.ContainsKey(key))
                return _StringExtFields[key];

            return "";
        }

        public void SetStringExtField(int key, string val)
        {
            _StringExtFields[key] = val;
        }

        public void ChangeState(LogicStateDef newState, params object[] args)
		{
			_LogicStateManager.ChangeState(this, newState, args);
		}

		public bool CanMove()
		{
			return true;
		}

		public void Idle()
		{
			ChangeState(LogicStateDef.IDLE);
		}

		public void Run()
		{
			ChangeState(LogicStateDef.RUN);
		}

		public void Jump()
		{
			ChangeState(LogicStateDef.JUMP);
		}

        public void Attack1()
        {
            if (_LogicState != LogicStateDef.IDLE)
                return;

            _CurrentSkill = 4;
            ChangeState(LogicStateDef.ATTACK);
            TimerHeap.AddTimer(500, 0, () =>
            {
                for (int j = 0; j < 13; j++)
                {
                    _World._ActiveObjectManager.CreateBullet(_GameObject, new Vector3((j - 6) * 0.3f, 0.5f, 0));
                }
            });
        }
        public void Attack2()
        {
            if (_LogicState != LogicStateDef.IDLE)
                return;

            _CurrentSkill = 1;
            ChangeState(LogicStateDef.ATTACK);
            TimerHeap.AddTimer(700, 0, () =>
            {
                _World._ActiveObjectManager.CreateBullet(_GameObject, new Vector3(0, 0.5f, 0));
            });
        }
    }
}
