using UnityEngine;
using HexCasters.DesignPatterns.Fsm;

namespace HexCasters.GameModes.Battle.Common
{
	public class BattleSelectMovementDestinationState : FsmState
	{
		public override void Enter()
		{
			Debug.Log(GetType());
		}

		public override void Exit()
		{
		}
	}
}
