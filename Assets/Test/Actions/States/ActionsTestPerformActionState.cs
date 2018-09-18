using System.Linq;
using UnityEngine;
using HexesOfMortvell.DesignPatterns.Fsm;
using HexesOfMortvell.Core.Actions;

namespace HexesOfMortvell.Testing.ActionsTest
{
	public class ActionsTestPerformActionState : FsmState
	{
		public ActionsTestPlayerOrder playerOrder;

		public override void Enter()
		{
			Debug.Log(playerOrder.selectedTargets.ToList());
			var aoe = this.playerOrder.action.GetComponent<ActionAoe>()
				.GetAoe(this.playerOrder.selectedTargets);
			var active = this.playerOrder.action.GetComponent<ActionActivation>();
			active.Perform(
				this.playerOrder.selectedUnit,
				this.playerOrder.selectedTargets,
				aoe);
			this.fsm.Transition<ActionsTestSelectUnitState>();
		}

		public override void Exit() {}
	}
}