#pragma warning disable

using System;
using UnityEngine;
using HexesOfMortvell.DesignPatterns.Fsm;

namespace HexesOfMortvell.GameModes.Battle.Common
{
	public class BattleSelectUnitActionState : FsmState
	{
		[Obsolete("This is only for testing the turn structure.")]
		public GameObject debugAction;
		public BattlePlayerOrders playerOrders;

		public override void Enter()
		{
			Debug.Log(GetType());
			this.playerOrders.action = this.debugAction;
			this.fsm.Transition<BattleSelectUnitActionTargets>();
		}

		public override void Exit() {}
	}
}
