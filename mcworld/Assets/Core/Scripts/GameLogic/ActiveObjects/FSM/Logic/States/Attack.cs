
namespace Core.GameLogic.ActiveObjects.FSM
{
    public class Attack : IState
    {
        public void Enter(Player player, int mode, params object[] args)
        {
			if (player == null)
				return;

			if (player._CurrentSkill == 1)
				EntityRepresentStateName.PlayState(player._PlayerAnimator, mode, RepresentStateDef.ATTACK1);
			else if (player._CurrentSkill == 4)
				EntityRepresentStateName.PlayState(player._PlayerAnimator, mode, RepresentStateDef.ATTACK4);
		}
        public void Exit(Player player, params object[] args)
        {
        }
    }
}
