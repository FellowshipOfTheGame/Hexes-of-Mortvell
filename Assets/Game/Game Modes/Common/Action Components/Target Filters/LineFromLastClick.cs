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

		public enum OccupiedCellBehaviour
		{
			StopBefore,
			StopButInclude,
			Ignore
		}
		public OccupiedCellBehaviour occupiedCellBehaviour;

		public override IEnumerable<BoardCell> ValidTargets(
			BoardCellContent actor,
			IEnumerable<BoardCell> partialTargets)
		{
			var lastTarget = partialTargets.LastOrDefault();
			var center = lastTarget ?? actor.Cell;
			bool stopAtOccupiedCells =
				this.occupiedCellBehaviour != OccupiedCellBehaviour.Ignore;
			bool includeFirstOccupiedCell =
				this.occupiedCellBehaviour != OccupiedCellBehaviour.StopBefore;
			return Direction.NonStayDirections
				.SelectMany(
					direction => GetBranch(
						center,
						stopAtOccupiedCells,
						includeFirstOccupiedCell,
						direction));
		}

		IEnumerable<BoardCell> GetBranch(
			BoardCell center,
			bool stopAtOccupiedCells,
			bool includeFirstOccupiedCell,
			Direction direction)
		{
			var branch = center.StraightLineTowards(
				direction,
				maxLength: this.maxLength,
				includeOrigin: this.includeOrigin,
				stopAtOccupiedCell: stopAtOccupiedCells,
				includeFirstOccupiedCell: includeFirstOccupiedCell);

			return branch;
		}
	}
}