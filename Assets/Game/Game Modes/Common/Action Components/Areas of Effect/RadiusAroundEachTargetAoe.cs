using System.Linq;
using System.Collections.Generic;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Regions;

namespace HexesOfMortvell.GameModes.Common
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