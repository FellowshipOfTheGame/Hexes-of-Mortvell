using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Regions;
using LineRegion = HexesOfMortvell.Core.Grid.Regions.BoardLineRegionExtension;

namespace HexesOfMortvell.GameModes.Common
{
	public class LineUpToFirstOccupiedAoe : ActionAoe
	{
		public bool includeOrigin;
		public int maxLength;

		public override IEnumerable<BoardCell> GetAoe(
			BoardCellContent actor,
			IEnumerable<BoardCell> targets)
		{
			var target = targets.Single();
			var direction = actor.Cell.Position
				.StraightLineDirectionTowards(target.Position).Value;
			return actor.Cell.StraightLineTowards(
				direction,
				maxLength: this.maxLength,
				occupiedCellBehaviour: LineRegion.OccupiedCellBehaviour.StopButInclude);
		}
	}
}
