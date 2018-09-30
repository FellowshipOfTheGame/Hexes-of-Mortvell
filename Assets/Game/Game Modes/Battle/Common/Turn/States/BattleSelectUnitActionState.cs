#pragma warning disable
#pragma warning restore 0618

using System;
using System.Collections.Generic;
using UnityEngine;
using HexesOfMortvell.DesignPatterns.Fsm;

namespace HexesOfMortvell.GameModes.Battle.Common
{
	public class BattleSelectUnitActionState : FsmState
	{
		[Obsolete("This is only for testing the turn structure.")]
		public List<GameObject> debugActions;
		public BattlePlayerOrders playerOrders;

		public override void Enter() {}

		public override void Exit() {}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Alpha1))
				Select(0);
			else if (Input.GetKeyDown(KeyCode.Alpha2))
				Select(1);
			else if (Input.GetKeyDown(KeyCode.Alpha3))
				Select(2);
			else if (Input.GetKeyDown(KeyCode.Alpha4))
				Select(3);
			else if (Input.GetKeyDown(KeyCode.Alpha5))
				Select(4);
			else if (Input.GetKeyDown(KeyCode.Alpha6))
				Select(5);
		}

		void Select(int actionIdx)
		{
			this.playerOrders.action = this.debugActions[actionIdx];
			this.fsm.Transition<BattleSelectUnitActionTargetsState>();
		}
	}
}
