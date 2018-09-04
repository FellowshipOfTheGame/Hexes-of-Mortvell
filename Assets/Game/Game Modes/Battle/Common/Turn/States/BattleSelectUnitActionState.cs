using System;
using UnityEngine;
using HexCasters.DesignPatterns.Fsm;

namespace HexCasters.GameModes.Battle.Common
{
	public class BattleSelectUnitActionState : FsmState
	{
		[Obsolete("This is only for testing the turn structure.")]
		public GameObject debugAction;
		public BattlePlayerOrders playerOrders;

		public override void Enter()
		{
			this.playerOrders.action = this.debugAction;
			this.fsm.Transition<BattleSelectUnitActionTargets>();
		}

		public override void Exit()
		{
		}
	}
}
