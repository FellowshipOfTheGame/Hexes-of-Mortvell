using UnityEngine;
using HexesOfMortvell.DesignPatterns.Fsm;

namespace HexesOfMortvell.Testing.GameModeTest
{
	public class GameModeTestWaitForRightClickState : FsmState
	{
		public override void Enter() {}

		public override void Exit() {}

		void Update()
		{
			if (Input.GetMouseButtonDown(1))
				this.fsm.Transition<GameModeTestTurnEndState>();
		}
	}
}
