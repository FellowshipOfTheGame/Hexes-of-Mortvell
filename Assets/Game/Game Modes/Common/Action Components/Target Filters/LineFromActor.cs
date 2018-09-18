using System.Linq;
using System.Collections.Generic;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Regions;

namespace HexesOfMortvell.GameModes.Common
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