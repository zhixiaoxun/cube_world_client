
namespace Core.GameLogic.ActiveObjects.FSM
{
    public interface IState
    {
        void Enter(Player player, int mode, params object[] args);
        void Exit(Player player, params object[] args);
    }
}
