using System.Collections.Generic;

namespace Core.GameLogic.ActiveObjects.FSM
{
    public class Represent2LogicMap
	{
		static public Dictionary<RepresentStateDef, LogicStateDef> MapData = new Dictionary<RepresentStateDef, LogicStateDef>();

		static Represent2LogicMap()
		{
			MapData.Add(RepresentStateDef.IDLE, LogicStateDef.IDLE);
			MapData.Add(RepresentStateDef.RUN, LogicStateDef.RUN);
			MapData.Add(RepresentStateDef.ATTACK1, LogicStateDef.ATTACK);
			MapData.Add(RepresentStateDef.ATTACK2, LogicStateDef.ATTACK);
			MapData.Add(RepresentStateDef.ATTACK3, LogicStateDef.ATTACK);
			MapData.Add(RepresentStateDef.ATTACK4, LogicStateDef.ATTACK);
			MapData.Add(RepresentStateDef.ATTACK5, LogicStateDef.ATTACK);
			MapData.Add(RepresentStateDef.DEATH, LogicStateDef.DEATH);
			MapData.Add(RepresentStateDef.JUMP, LogicStateDef.JUMP);
			MapData.Add(RepresentStateDef.DAMAGE, LogicStateDef.DAMAGE);
			MapData.Add(RepresentStateDef.PICKUP, LogicStateDef.PICKUP);
			MapData.Add(RepresentStateDef.SWIM, LogicStateDef.SWIM);
		}

		static public LogicStateDef GetLogicState(RepresentStateDef state)
		{
			if (MapData.ContainsKey(state))
				return MapData[state];

			return LogicStateDef.NONE;
		}
	}
}
