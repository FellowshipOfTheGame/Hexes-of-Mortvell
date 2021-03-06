using UnityEngine;
using HexesOfMortvell.DesignPatterns.Fsm;

namespace HexesOfMortvell.GameModes.Battle
{
	public class BattleMoveUnitState : FsmState
	{
		public BattlePlayerOrders playerOrders;

		public override void Enter()
		{
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