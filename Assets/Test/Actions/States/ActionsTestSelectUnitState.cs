using System;
using System.Linq;
using UnityEngine;
using HexCasters.Core.Grid;
using HexCasters.DesignPatterns.Fsm;
using HexCasters.Hud.Grid;

namespace HexCasters.Testing.ActionsTest
{
	public class ActionsTestSelectUnitState : FsmState
	{
		public ActionsTestTurn turn;
		private IDisposable unmovedUnitsHighlight;

		public override void Enter()
		{
			var units = turn.CurrentTeam.Members
				.Select(
					teamMember => teamMember.GetComponent<BoardCellContent>());
			Debug.Log(units.ToList().Count);
			var unitCells = units
				.Select(
					cellContent => cellContent.Cell)
				.ToList();
			unitCells.Highlight(Color.white);
			// Debug.Log(unitCells.Select(cell => cell.Position.ToString()).ToList()[0]);
		}

		public override void Exit()
		{
			unmovedUnitsHighlight.Dispose();
		}
	}
}
