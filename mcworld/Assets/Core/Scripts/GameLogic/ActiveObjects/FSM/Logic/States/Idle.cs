using Core.GameLogic.ActiveObjects;

namespace Core.GameLogic.ActiveObjects.FSM
{
    public class Idle : IState
    {
        public void Enter(Player player, int mode, params object[] args)
        {
			if (player == null)
				return;

			EntityRepresentStateName.PlayState(player._PlayerAnimator, mode, RepresentStateDef.IDLE);
		}
        public void Exit(Player player, params object[] args)
        {

        }
    }
}
