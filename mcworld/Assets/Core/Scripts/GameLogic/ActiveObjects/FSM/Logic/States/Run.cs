using Core.GameLogic.ActiveObjects;

namespace Core.GameLogic.ActiveObjects.FSM
{
    public class Run : IState
    {
        public void Enter(Player player, int mode, params object[] args)
        {
			if (player == null)
				return;

			if (player.CanMove())
			{
				EntityRepresentStateName.PlayState(player._PlayerAnimator, mode, RepresentStateDef.RUN);
			}
		}
        public void Exit(Player player, params object[] args)
        {
			//EntityRepresentStateName.PlayState(player.avatarAnimator, 2, RepresentStateDef.IDLE);
		}
    }
}
