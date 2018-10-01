using System.Linq;
using System.Collections.Generic;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Regions;
using LineRegion = HexesOfMortvell.Core.Grid.Regions.BoardLineRegionExtension;

namespace HexesOfMortvell.GameModes.Common
{
	public class LineFromLastClick : ActionTargetFilter
	{
		public int maxLength;
		public bool includeOrigin;

		public LineRegion.OccupiedCellBehaviour occupiedCellBehaviour;

		public override IEnumerable<BoardCell> ValidTargets(
			BoardCellContent actor,
			IEnumerable<BoardCell> partialTargets)
		{
			var lastTarget = partialTargets.LastOrDefault();
			var center = lastTarget ?? actor.Cell;
			return Direction.NonStayDirections
				.SelectMany(
					direction => GetBranch(
						center,
						direction));
		}

		IEnumerable<BoardCell> GetBranch(
			BoardCell center,
			Direction direction)
		{
			var branch = center.StraightLineTowards(
				direction,
				maxLength: this.maxLength,
				includeOrigin: this.includeOrigin,
				occupiedCellBehaviour: this.occupiedCellBehaviour);

			return branch;
		}
	}
}