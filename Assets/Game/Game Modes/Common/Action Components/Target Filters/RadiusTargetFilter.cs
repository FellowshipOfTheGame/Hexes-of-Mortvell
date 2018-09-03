using System.Collections.Generic;
using HexCasters.Core.Actions;
using HexCasters.Core.Grid;
using HexCasters.Core.Grid.Regions;

namespace HexCasters.GameModes.Common
{
	public class RadiusTargetFilter : ActionTargetFilter
	{
		public override int TargetCount => 1;

		public int radius;

		public override IEnumerable<BoardCell> ValidTargets(
			BoardCellContent actor,
			IEnumerable<BoardCell> partialTargets)
		{
			return actor.Cell.Neighborhood(this.radius);
		}
	}
}
