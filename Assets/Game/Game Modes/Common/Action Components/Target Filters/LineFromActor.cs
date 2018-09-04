using System.Linq;
using System.Collections.Generic;
using HexCasters.Core.Actions;
using HexCasters.Core.Grid;
using HexCasters.Core.Grid.Regions;

namespace HexCasters.GameModes.Common
{
	public class LineFromActor : ActionTargetFilter
	{
		public override int TargetCount => 1;
		public int maxLength;
		public bool includeOrigin;
		public bool stopAtOccupiedCells;

		public override IEnumerable<BoardCell> ValidTargets(
			BoardCellContent actor,
			IEnumerable<BoardCell> partialTargets)
		{
			return Direction.NonStayDirections
				.SelectMany(
					direction => actor.Cell.StraightLineTowards(
						direction,
						maxLength: this.maxLength,
						includeOrigin: this.includeOrigin,
						stopAtOccupiedCell: this.stopAtOccupiedCells));
		}
	}
}