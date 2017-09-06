using UnityEngine;

namespace Core.GameLogic.ActiveObjects.FSM
{
    public class Jump : IState
    {
        public float _JumpForce = 50.0f; // 跳跃施加的力
        public void Enter(Player player, int mode, params object[] args)
        {
            if (player == null)
                return;

            if (player.CanMove())
            {
                EntityRepresentStateName.PlayState(player._PlayerAnimator, mode, RepresentStateDef.JUMP);
                if (player is LocalPlayer)
                {
                    player._World._NetworkManager._MessageSender.SendPlayerKeyPress(PlayerControl.JumpBegin());
                }
                //player._PlayerRigidbody.AddForce(Vector3.up * _JumpForce, ForceMode.Impulse);
            }
        }
        public void Exit(Player player, params object[] args)
        {
            if (player is LocalPlayer)
            {
                player._World._NetworkManager._MessageSender.SendPlayerKeyPress(PlayerControl.JumpEnd());
            }
        }
    }
}
