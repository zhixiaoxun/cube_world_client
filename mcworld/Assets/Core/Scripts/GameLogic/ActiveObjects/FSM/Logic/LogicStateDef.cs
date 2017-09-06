namespace Core.GameLogic.ActiveObjects.FSM
{
    // 逻辑状态定义
    public enum LogicStateDef
    {
		NONE = -1,
        IDLE = 0,
		RUN = 1,
		ATTACK = 2,
		JUMP = 3,
		DEATH = 4,
		DAMAGE = 5,
		PICKUP = 6,
		SWIM = 7,
	}
}
