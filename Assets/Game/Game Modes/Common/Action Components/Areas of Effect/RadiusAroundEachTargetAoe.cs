using System.Linq;
using System.Collections.Generic;
using HexCasters.Core.Actions;
using HexCasters.Core.Grid;
using HexCasters.Core.Grid.Regions;

namespace HexCasters.GameModes.Common
{
	public class RadiusAroundEachTargetAoe : ActionAoe
	{
		public int radius;

		public override IEnumerable<BoardCell> GetAoe(
			IEnumerable<BoardCell> targets)
		{
			return targets
				.SelectMany(target => target.Neighborhood(this.radius));
		}
	}
}