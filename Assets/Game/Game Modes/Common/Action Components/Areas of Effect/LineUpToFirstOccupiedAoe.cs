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
		[Tooltip("If the line is interrupted by an occupied cell, extend the line further by this much.")]
		public int hitExtensionLength = 0;

		public override IEnumerable<BoardCell> GetAoe(
			BoardCellContent actor,
			IEnumerable<BoardCell> targets)
		{
			var target = targets.Single();
			var direction = actor.Cell.Position
				.StraightLineDirectionTowards(target.Position).Value;
			var line = actor.Cell
				.StraightLineTowards(
					direction,
					maxLength: this.maxLength,
					occupiedCellBehaviour: LineRegion.OccupiedCellBehaviour.StopButInclude);
			line = line.ToList();
			var last = line.LastOrDefault();
			var lastEmpty = last != null && last.Empty;
			if (!lastEmpty && hitExtensionLength != 0)
			{
				var extension = last
					.StraightLineTowards(
						direction,
						maxLength: hitExtensionLength,
						includeOrigin: false,
						occupiedCellBehaviour: LineRegion.OccupiedCellBehaviour.StopButInclude);
				line = Enumerable.Concat(line, extension);
			}

			return line;
		}
	}
}
