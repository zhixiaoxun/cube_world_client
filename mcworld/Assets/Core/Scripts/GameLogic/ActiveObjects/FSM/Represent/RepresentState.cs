using UnityEngine;

namespace Core.GameLogic.ActiveObjects.FSM
{
    public class RepresentState : StateMachineBehaviour
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            ////Debug.Log("On StateEnter ");

            // 为了防止从AnyState再次进入状态导致动作抽风
            animator.SetInteger("action", (int)RepresentStateDef.NONE);

			// 获取当前状态
			RepresentStateDef representState = EntityRepresentStateName.GetStateByHash(stateInfo.shortNameHash);
			if (representState == RepresentStateDef.NONE)
				return;

            PlayerController representEntity = animator.gameObject.GetComponent<PlayerController>();
			if (representEntity == null)
				representEntity = animator.gameObject.transform.parent.GetComponent<PlayerController>();

			if (representEntity == null)
				return;

			Player entity = representEntity.player;
			entity._RepresentState = representState; // 记录表现状态

			// 获取表现状态对应的逻辑状态
			LogicStateDef logicState = Represent2LogicMap.GetLogicState(representState);
			if (logicState == LogicStateDef.NONE)
				return;

			// 进入逻辑状态
			entity.ChangeState(logicState);
		}
		override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }
        override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }
        override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }
    }
}