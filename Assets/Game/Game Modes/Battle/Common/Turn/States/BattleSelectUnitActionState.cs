#pragma warning disable
#pragma warning restore 0618

using System;
using System.Collections.Generic;
using UnityEngine;
using HexesOfMortvell.DesignPatterns.Fsm;
using HexesOfMortvell.Core.Actions;

namespace HexesOfMortvell.GameModes.Battle
{
	public class BattleSelectUnitActionState : FsmState
	{
		public ActionSet actionSet;
		public BattlePlayerOrders playerOrders;

		public override void Enter()
		{
			FindUnitActions();
			if (!UnitCanAct())
			{
				this.fsm.Transition<BattleFinishOrdersState>();
			}
		}

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
			else if (Input.GetKeyDown(KeyCode.Alpha7))
				Select(6);
			else if (Input.GetKeyDown(KeyCode.Alpha8))
				Select(7);
		}

		void Select(int actionIdx)
		{
			this.playerOrders.action = this.actionSet.actions[actionIdx];
			this.fsm.Transition<BattleSelectUnitActionTargetsState>();
		}

		void FindUnitActions()
		{
			this.actionSet = this.playerOrders.unit.GetComponent<ActionSet>();
		}

		bool UnitCanAct()
		{
			return this.actionSet != null && this.actionSet.actions.Count > 0;
		}
	}
}
