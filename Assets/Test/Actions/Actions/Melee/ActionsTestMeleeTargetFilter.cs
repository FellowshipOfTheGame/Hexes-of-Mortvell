using UnityEngine;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Regions;
using System.Collections.Generic;

namespace HexesOfMortvell.Testing.ActionsTest
{
	public class ActionsTestMeleeTargetFilter : ActionTargetFilter
	{
		public override IEnumerable<BoardCell> ValidTargets(
			BoardCellContent actor,
			IEnumerable<BoardCell> partialTargets)
		{
			return actor.Cell.Neighborhood(1);
		}
	}
}
