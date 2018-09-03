using UnityEngine;
using HexCasters.Core.Actions;
using HexCasters.Core.Grid;
using HexCasters.Core.Grid.Regions;
using System.Collections.Generic;

namespace HexCasters.Testing.ActionsTest
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
