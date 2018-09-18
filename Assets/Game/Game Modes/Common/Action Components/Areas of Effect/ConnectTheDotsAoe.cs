using System.Collections.Generic;
using System.Linq;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Regions;

namespace HexesOfMortvell.GameModes.Common
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
				var segment = currentVertex.StraightLineTowards(
					nextVertex,
					includeOrigin: false,
					stopAtOccupiedCell: false);
				aoe.AddRange(segment);
				currentVertex = nextVertex;
			}
			return aoe;
		}
	}
}