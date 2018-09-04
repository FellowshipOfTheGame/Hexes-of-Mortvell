using UnityEngine;
using HexCasters.DesignPatterns.Fsm;

namespace HexCasters.GameModes.Battle.Common
{
	public class BattleMoveUnitState : FsmState
	{
		public BattlePlayerOrders playerOrders;

		public override void Enter()
		{
			Debug.Log(GetType());
			PerformMovement();
		}

		public override void Exit() {}

		void PerformMovement()
		{
			this.playerOrders.movementOrigin.MoveContentTo(
				this.playerOrders.movementDestination);
			this.fsm.Transition<BattleSelectUnitActionState>();
		}
	}
}