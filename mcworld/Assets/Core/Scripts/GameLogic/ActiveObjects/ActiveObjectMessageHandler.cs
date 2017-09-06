using UnityEngine;

namespace Core.GameLogic.ActiveObjects
{
    public class ActiveObjectMessageHandler
    {
        public void ProcessMessage(ActiveObject obj, proto_server.s2c_object_init_message ao_data)
        {
            ProcessProperty(obj, ao_data.properties);
            ProcessYawPitch(obj, ao_data.yawpitch);
            ProcessMovemont(obj, ao_data.movemont);
            ProcesssControlBits(obj, ao_data.control);
        }

        public void ProcessProperty(ActiveObject obj, proto_server.s2c_object_properties properties)
        {
            Debug.Log("ProcessProperty......");
        }

        public void ProcessYawPitch(ActiveObject obj, proto_server.s2c_object_yaw_and_picth yawpitch)
        {
            Debug.Log("ProcessYawPitch......");
        }

        public void ProcessMovemont(ActiveObject obj, proto_server.s2c_object_movemont movement)
        {
            if (obj is Player)
            {
                if (obj._GameObject != null && movement.aid != obj._World._LocalPlayerServerID) // 只处理其它人
                {
                    //obj._GameObject.transform.position = new Vector3(movement.pos.x / 10, movement.pos.y / 10, movement.pos.z / 10);
                    (obj as Player)._Component.SetTargetPos(new Vector3(movement.pos.x / 10, movement.pos.y / 10, movement.pos.z / 10));
                    (obj as Player)._Component.SetTargetDir(new Vector3(movement.speed.x, movement.speed.y, movement.speed.z).normalized);
                }
            }
        }

        public void ProcesssControlBits(ActiveObject obj, proto_server.s2c_object_control_bits control)
        {
            Debug.Log("ProcesssControlBits......");
            if(obj is Player && (obj as Player)._Component != null)
            {
                uint flagJump = control.flags & (1 << 4);
                Debug.Log(flagJump);

                if(flagJump > 0)
                {
                    (obj as Player)._Component.NeedJump(true);
                }
                else
                {
                    (obj as Player)._Component.NeedJump(false);
                }
            }
        }
    }
}
