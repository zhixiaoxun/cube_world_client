using Core.Utils;
using Core.GameLogic;
using System.Collections.Generic;

namespace Core.GameLogic.ActiveObjects.FSM
{
	static public class EntityLogicStateSet
    {
		static public Idle stateIdle()
		{
			return new Idle();
		}
		static public Run stateRun()
		{
			return new Run();
		}
		static public Attack stateAttack()
		{
			return new Attack();
		}
		static public Jump stateJump()
		{
			return new Jump();
		}
		static public Damage stateDamage()
		{
			return new Damage();
		}
		static public Death stateDeath()
		{
			return new Death();
		}
		static public Swim stateSwim()
		{
			return new Swim();
		}
		static public Pickup statePickup()
		{
			return new Pickup();
		}
	}

    public class LogicStateManager
    {
		// 逻辑状态映射表
		static public Dictionary<LogicStateDef, Dictionary<LogicStateDef, int>> StateMap = new Dictionary<LogicStateDef, Dictionary<LogicStateDef, int>>();

		// 逻辑状态列表
		public Dictionary<LogicStateDef, IState> StateList = new Dictionary<LogicStateDef, IState>();

		public LogicStateManager()
		{
			////////////////////////////////////////////////////////////////////////////////////////////////////
			// 构建逻辑状态映射表
			// 任意状态
			StateMap[LogicStateDef.NONE] = new Dictionary<LogicStateDef, int>();
			StateMap[LogicStateDef.NONE].Add(LogicStateDef.IDLE, 1);
			StateMap[LogicStateDef.NONE].Add(LogicStateDef.RUN, 1);
			StateMap[LogicStateDef.NONE].Add(LogicStateDef.ATTACK, 1);
			StateMap[LogicStateDef.NONE].Add(LogicStateDef.JUMP, 1);
			StateMap[LogicStateDef.NONE].Add(LogicStateDef.DEATH, 1);
			StateMap[LogicStateDef.NONE].Add(LogicStateDef.DAMAGE, 1);
			StateMap[LogicStateDef.NONE].Add(LogicStateDef.PICKUP, 1);
			StateMap[LogicStateDef.NONE].Add(LogicStateDef.SWIM, 1);

			// 站立状态
			StateMap[LogicStateDef.IDLE] = new Dictionary<LogicStateDef, int>();
			StateMap[LogicStateDef.IDLE].Add(LogicStateDef.RUN, 1);
			StateMap[LogicStateDef.IDLE].Add(LogicStateDef.ATTACK, 1);
			StateMap[LogicStateDef.IDLE].Add(LogicStateDef.JUMP, 1);
			StateMap[LogicStateDef.IDLE].Add(LogicStateDef.DEATH, 1);
			StateMap[LogicStateDef.IDLE].Add(LogicStateDef.DAMAGE, 1);
			StateMap[LogicStateDef.IDLE].Add(LogicStateDef.PICKUP, 1);
			StateMap[LogicStateDef.IDLE].Add(LogicStateDef.SWIM, 1);

			// 移动状态
			StateMap[LogicStateDef.RUN] = new Dictionary<LogicStateDef, int>();
			StateMap[LogicStateDef.RUN].Add(LogicStateDef.IDLE, 1);
			StateMap[LogicStateDef.RUN].Add(LogicStateDef.ATTACK, 1);
			StateMap[LogicStateDef.RUN].Add(LogicStateDef.JUMP, 1);
			StateMap[LogicStateDef.RUN].Add(LogicStateDef.DEATH, 1);
			StateMap[LogicStateDef.RUN].Add(LogicStateDef.DAMAGE, 1);
			StateMap[LogicStateDef.RUN].Add(LogicStateDef.PICKUP, 1);
			StateMap[LogicStateDef.RUN].Add(LogicStateDef.SWIM, 1);

			// 技能状态
			StateMap[LogicStateDef.ATTACK] = new Dictionary<LogicStateDef, int>();
			StateMap[LogicStateDef.ATTACK].Add(LogicStateDef.IDLE, 1);
			StateMap[LogicStateDef.ATTACK].Add(LogicStateDef.RUN, 1);

			// 跳跃状态
			StateMap[LogicStateDef.JUMP] = new Dictionary<LogicStateDef, int>();
			StateMap[LogicStateDef.JUMP].Add(LogicStateDef.IDLE, 1);
			StateMap[LogicStateDef.JUMP].Add(LogicStateDef.RUN, 1);
			StateMap[LogicStateDef.JUMP].Add(LogicStateDef.SWIM, 1);

			// 死亡状态
			StateMap[LogicStateDef.DEATH] = new Dictionary<LogicStateDef, int>();
			StateMap[LogicStateDef.DEATH].Add(LogicStateDef.IDLE, 1);

			// 被击状态
			StateMap[LogicStateDef.DAMAGE] = new Dictionary<LogicStateDef, int>();
			StateMap[LogicStateDef.DAMAGE].Add(LogicStateDef.IDLE, 2);

			// 拾取状态
			StateMap[LogicStateDef.PICKUP] = new Dictionary<LogicStateDef, int>();
			StateMap[LogicStateDef.PICKUP].Add(LogicStateDef.IDLE, 1);

			// 游泳状态
			StateMap[LogicStateDef.SWIM] = new Dictionary<LogicStateDef, int>();
			StateMap[LogicStateDef.SWIM].Add(LogicStateDef.IDLE, 1);
			StateMap[LogicStateDef.SWIM].Add(LogicStateDef.JUMP, 1);


			/////////////////////////////////////////////////////////////////////////////////////////
			//状态列表
			StateList.Add(LogicStateDef.IDLE, EntityLogicStateSet.stateIdle());
			StateList.Add(LogicStateDef.RUN, EntityLogicStateSet.stateRun());
			StateList.Add(LogicStateDef.ATTACK, EntityLogicStateSet.stateAttack());
			StateList.Add(LogicStateDef.JUMP, EntityLogicStateSet.stateJump());
			StateList.Add(LogicStateDef.DAMAGE, EntityLogicStateSet.stateDamage());
			StateList.Add(LogicStateDef.DEATH, EntityLogicStateSet.stateDeath());
			StateList.Add(LogicStateDef.SWIM, EntityLogicStateSet.stateSwim());
			StateList.Add(LogicStateDef.PICKUP, EntityLogicStateSet.statePickup());
		}

		public void ChangeState(Player player, LogicStateDef newState, params object[] args)
		{
			if (newState == LogicStateDef.NONE)
				return;

			if (newState == player._LogicState/* && newState != LogicStateDef.RUN*/)
			{
				return;
			}

			int changeMode = GetStateChangeMode(player, newState);
			if (changeMode == 0)
				return;

			if (player._LogicState != LogicStateDef.NONE)
			{
				//LoggerHelper.Debug(string.Format("Exit state {0}", player._LogicState.ToString()));
				StateList[player._LogicState].Exit(player, args);
			}

			//LoggerHelper.Debug(string.Format("Enter state {0}", newState.ToString()));
			player._LogicState = newState;
			StateList[newState].Enter(player, changeMode, args);
		}

		// 0不可以
		// 1正常切换
		// 2强制切换
		public int GetStateChangeMode(Player player, LogicStateDef newState)
		{
			if (StateMap[LogicStateDef.NONE].ContainsKey(newState))
				return StateMap[LogicStateDef.NONE][newState];

			if (StateMap[player._LogicState].ContainsKey(newState))
				return StateMap[player._LogicState][newState];

			return 0;
		}
	}
}