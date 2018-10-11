using UnityEngine;
using HexesOfMortvell.DesignPatterns.Fsm;
using HexesOfMortvell.Core.Grid.Loading;

namespace HexesOfMortvell.Testing.ActionsTest
{
	public class ActionsTestSetupState : FsmState
	{
		public ActionsTestLayoutLoader loader;

		public override void Enter()
		{
			this.loader.DoneLoadingEvent += StartFirstTurn;
		}

		public override void Exit()
		{
			this.loader.DoneLoadingEvent -= StartFirstTurn;
		}

		void StartFirstTurn()
		{
			this.fsm.Transition<ActionsTestTurnStartState>();
		}
	}
}
