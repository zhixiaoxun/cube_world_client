using System.Collections.Generic;
using UnityEngine;

namespace Core.GameLogic.ActiveObjects.FSM
{
    // 表现状态定义 必须与动画状态机中action字段值匹配
    public enum RepresentStateDef
    {
		NONE = -1,
        IDLE = 0, // 站立
        RUN = 1, // 移动
        ATTACK1 = 5, // 技能1
        ATTACK2 = 6, // 技能2
		ATTACK3 = 7, // 技能3
		ATTACK4 = 8, // 技能4
		ATTACK5 = 9, // 技能5
		DEATH = 10, // 死亡
		JUMP = 11, // 跳
		DAMAGE = 12, // 被击
		PICKUP = 15, // 拾取
		SWIM = 19, // 游泳
    }

    public class EntityRepresentStateName
    {
        static public Dictionary<int, RepresentStateDef> Hash2State = new Dictionary<int, RepresentStateDef>();

        static public Dictionary<RepresentStateDef, int> State2Hash = new Dictionary<RepresentStateDef, int>();

		

        static EntityRepresentStateName()
        {
			registerState("idle_w", RepresentStateDef.IDLE);
			registerState("run000", RepresentStateDef.RUN);
			registerState("attack1", RepresentStateDef.ATTACK1);
			registerState("attack2", RepresentStateDef.ATTACK2);
			registerState("attack3", RepresentStateDef.ATTACK3);
			registerState("attack4", RepresentStateDef.ATTACK4);
			registerState("attack_combo", RepresentStateDef.ATTACK5);
			registerState("death", RepresentStateDef.DEATH);
			registerState("jump_w", RepresentStateDef.JUMP);
			registerState("damage", RepresentStateDef.DAMAGE);
			registerState("pickup_w", RepresentStateDef.PICKUP);
			registerState("swim", RepresentStateDef.SWIM);
		}

		static void registerState(string stateStr, RepresentStateDef state)
		{
			Hash2State.Add(Animator.StringToHash(stateStr), state);
			State2Hash.Add(state, Animator.StringToHash(stateStr));
		}

		static public RepresentStateDef GetStateByHash(int nameHash)
		{
			if (Hash2State.ContainsKey(nameHash))
			{
				return Hash2State[nameHash];
			}

			return RepresentStateDef.IDLE;
		}

		static public int GetHashByState(RepresentStateDef state)
		{
			if (State2Hash.ContainsKey(state))
			{
				return State2Hash[state];
			}

			return 0;
		}

		static public void PlayState(Animator animator, int mode, RepresentStateDef state)
        {
			if (animator == null)
				return;

			if (mode == 1)
			{
				animator.SetInteger("action", (int)state);
			}
			else if (mode == 2) // 强制播放
			{
				animator.Play(GetHashByState(state), 0);
			}
		}
    }
}