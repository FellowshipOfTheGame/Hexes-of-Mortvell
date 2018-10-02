using UnityEngine;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;
using System.Collections.Generic;

namespace HexesOfMortvell.Testing.ActionsTest
{
	public class ActionsTestMeleeAoe : ActionAoe
	{
		public override IEnumerable<BoardCell> GetAoe(
			BoardCellContent actor,
			IEnumerable<BoardCell> targets)
		{
			return targets;
		}
	}
}
