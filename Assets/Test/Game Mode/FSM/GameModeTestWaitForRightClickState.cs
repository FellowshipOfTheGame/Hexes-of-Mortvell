using UnityEngine;
using HexCasters.DesignPatterns.Fsm;

namespace HexCasters.Testing.GameModeTest
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
