using System.Linq;
using System.Collections.Generic;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Regions;

namespace HexesOfMortvell.GameModes.Common
{
	public class LineFromLastClick : ActionTargetFilter
	{
		public int maxLength;
		public bool includeOrigin;
		public bool stopAtOccupiedCells;

		public override IEnumerable<BoardCell> ValidTargets(
			BoardCellContent actor,
			IEnumerable<BoardCell> partialTargets)
		{
			var lastTarget = partialTargets.LastOrDefault();
			var center = lastTarget ?? actor.Cell;
			return Direction.NonStayDirections
				.SelectMany(
					direction => center.StraightLineTowards(
						direction,
						maxLength: this.maxLength,
						includeOrigin: this.includeOrigin,
						stopAtOccupiedCell: this.stopAtOccupiedCells));
		}
	}
}