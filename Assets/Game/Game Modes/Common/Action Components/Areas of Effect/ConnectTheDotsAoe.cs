using System.Collections.Generic;
using System.Linq;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Regions;

namespace HexesOfMortvell.GameModes.Common
{
	public class ConnectTheDotsAoe : ActionAoe
	{
		public bool startAtActor;
		public bool includeFirstDot;

		public override IEnumerable<BoardCell> GetAoe(
			BoardCellContent actor,
			IEnumerable<BoardCell> targets)
		{
			var targetList = new List<BoardCell>(targets);
			var aoe = new List<BoardCell>();

			BoardCell currentVertex;
			IEnumerable<BoardCell> otherVertices;
			if (this.startAtActor)
			{
				currentVertex = actor.Cell;
				otherVertices = targetList;
			}
			else
			{
				currentVertex = targetList[0];
				otherVertices = targetList.Skip(1);
			}
			if (this.includeFirstDot)
			{
				aoe.Add(currentVertex);
			}

			foreach (var nextVertex in otherVertices)
			{
				print($"{currentVertex} -> {nextVertex}");
				var segment = currentVertex.StraightLineTowards(
					nextVertex,
					includeOrigin: false,
					stopAtOccupiedCell: false).ToList();
				foreach (var x in segment)
					print(x);
				aoe.AddRange(segment);
				currentVertex = nextVertex;
			}
			return aoe;
		}
	}
}