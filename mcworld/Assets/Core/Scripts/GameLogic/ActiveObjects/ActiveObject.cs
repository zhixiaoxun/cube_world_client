using System.Collections;
using UnityEngine;
using SLua;
using Core.Config;

namespace Core.GameLogic.ActiveObjects
{
    [CustomLuaClass]
    public class ActiveObject
    {
        
        public GameObject _GameObject { get; protected set; } = null;
        public World _World { get; protected set; } = null;
        public bool _IsReady { get; protected set; } = false;
        public int _ID { get; set; } = 0;

        protected ActiveObjectManager _ActiveObjectManager = null;
        protected bool _IsPlayer = false;
        protected bool _IsLocalPlayer = false;

        public ActiveObject(World world)
        {
            _World = world;
        }

        public virtual void Init(ActiveObjectManager manager, int id, proto_server.s2c_object_init_message ao_data)
        {
            _ActiveObjectManager = manager;
            _ID = id;

            manager._ObjectMessageHandler.ProcessMessage(this, ao_data);

            CreateModel(ao_data);
        }

        public virtual void UnInit()
        {
            GameObject.Destroy(_GameObject);
        }

        public virtual void Active()
        {

        }

        protected virtual void CreateModel(proto_server.s2c_object_init_message ao_data)
        {
            CoreEnv.CoreDriver.StartCoroutine(DoCreateModel(ao_data));
        }

        protected virtual IEnumerator DoCreateModel(proto_server.s2c_object_init_message ao_data)
        {
            yield return 1;
        }
    }
}
