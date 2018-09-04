using System.Collections.Generic;
using System.Linq;
using HexCasters.Core.Actions;
using HexCasters.Core.Grid;
using HexCasters.Core.Grid.Regions;

namespace HexCasters.GameModes.Common
{
	public class ConnectTheDotsAoe : ActionAoe
	{
		public override IEnumerable<BoardCell> GetAoe(
			IEnumerable<BoardCell> targets)
		{
			var targetList = new List<BoardCell>(targets);
			var aoe = new List<BoardCell>();
			aoe.Add(targetList[0]);

			var currentVertex = targetList[0];
			var otherVertices = targetList.Skip(1);
			foreach (var nextVertex in otherVertices)
			{
				aoe.AddRange(
					currentVertex.StraightLineTowards(
						nextVertex,
						includeOrigin: false,
						stopAtOccupiedCell: false));
				currentVertex = nextVertex;
			}
			return aoe;
		}
	}
}