using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Regions;

namespace HexesOfMortvell.GameModes.Common
{
	public class RadiusFromLastClickTargetFilter : ActionTargetFilter
	{
		public int radius;

		public override IEnumerable<BoardCell> ValidTargets(
			BoardCellContent actor,
			IEnumerable<BoardCell> partialTargets)
		{
			var lastTarget = partialTargets.LastOrDefault();
			var radiusCenter = lastTarget ?? actor.Cell;
			return radiusCenter.Neighborhood(this.radius);
		}
	}
}
