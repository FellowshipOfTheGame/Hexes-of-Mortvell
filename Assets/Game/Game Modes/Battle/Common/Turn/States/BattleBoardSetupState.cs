using UnityEngine;
using HexCasters.DesignPatterns.Fsm;
using HexCasters.Core.Grid;

namespace HexCasters.GameModes.Battle.Common
{
	public class BattleBoardSetupState : FsmState
	{
		public Board board;

		public override void Enter()
		{
			Debug.Log(GetType().Name);
			this.board.doneLoadingEvent += StartFirstTurn;
		}

		public override void Exit()
		{
			this.board.doneLoadingEvent -= StartFirstTurn;
		}

		void StartFirstTurn(Board board)
		{
			this.fsm.Transition<BattleStartTurnState>();
		}
	}
}