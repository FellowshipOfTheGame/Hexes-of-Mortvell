using UnityEngine;
using HexesOfMortvell.DesignPatterns.Fsm;
using HexesOfMortvell.Core.Units.Teams;

namespace HexesOfMortvell.Testing.GameModeTest
{
	public class GameModeTestSelectDudeState : FsmState
	{
		public GameModeTestTurn turn;
		public DudeClickListener dudeClickListener;

		public override void Enter()
		{
			dudeClickListener.dudeClickedEvent += DudeClicked;
		}

		public override void Exit()
		{
			dudeClickListener.dudeClickedEvent -= DudeClicked;
		}

		void DudeClicked(GameObject dude)
		{
			var dudeTeam = dude.GetComponent<TeamMember>()?.team;
			if (dudeTeam == null || this.turn.CurrentTeam == dudeTeam)
				return;
			Destroy(dude);

			this.fsm.Transition<GameModeTestWaitForRightClickState>();
		}
	}
}
