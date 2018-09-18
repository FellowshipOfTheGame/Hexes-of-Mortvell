using System.Collections.Generic;
using UnityEngine;
using HexesOfMortvell.Core.Grid;

namespace HexesOfMortvell.Testing.ActionsTest
{
	public class ActionsTestPlayerOrder : MonoBehaviour
	{
		// use origin cell instead when implementing for real
		// will make undo easier
		public BoardCellContent selectedUnit;
		public BoardCell moveDest;
		public GameObject action;
		public List<BoardCell> selectedTargets;
		public IEnumerable<BoardCell> aoe;

		public void Clear()
		{
			this.selectedUnit = null;
			this.moveDest = null;
			this.action = null;
			this.selectedTargets = new List<BoardCell>();
			this.aoe = null;
		}

	}
}
