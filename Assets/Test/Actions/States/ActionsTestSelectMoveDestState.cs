using UnityEngine;
using HexCasters.DesignPatterns.Fsm;

namespace HexCasters.Testing.ActionsTest
{
	public class ActionsTestSelectMoveDestState : FsmState
	{
		public ActionsTestPlayerOrder playerOrder;

		public override void Enter()
		{
			Debug.Log(playerOrder.selectedUnit);
			this.fsm.Transition<ActionsTestTurnEndState>();
		}

		public override void Exit() {}
	}
}