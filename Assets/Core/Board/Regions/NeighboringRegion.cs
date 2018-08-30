using System;
using System.Collections.Generic;

namespace HexCasters.Core.Board.Regions
{
	public static class BoardNeighboringRegionExtension
	{
		public static IEnumerable<BoardCell> Neighborhood(
			this BoardCell center,
			int maxDistance,
			Func<BoardCell, BoardCell, int> distanceFunction=null)
		{
			distanceFunction = distanceFunction ?? DefaultDistanceFunction;
			var openNodes = new List<BoardCell>();
			var distances = new Dictionary<BoardCell, int>();
			var visited = new HashSet<BoardCell>();
			var distanceComparer = DistanceComparer(
				distanceFunction,
				distances);

			openNodes.Add(center);
			distances[center] = 0;

			visited.Add(center);
			yield return center;

			while (openNodes.Count > 0)
			{
				openNodes.Sort(distanceComparer);
				var currentNode = openNodes[0];
				var currentDist = distances[currentNode];
				openNodes.RemoveAt(0);

				if (currentDist > maxDistance)
					continue;

				foreach (var neighbor in currentNode.FindAdjacentCells())
				{
					var distanceFromCurrentNode
						= currentDist + distanceFunction(currentNode, neighbor);

					if (!visited.Contains(neighbor))
					{
						visited.Add(neighbor);
						yield return neighbor;
					}

					SetInfiniteDistanceIfAbsent(distances, neighbor);
					if (distanceFromCurrentNode < distances[neighbor])
					{
						openNodes.Add(neighbor);
						distances[neighbor] = distanceFromCurrentNode;
					}
				}
			}
		}

		private static int DefaultDistanceFunction(BoardCell from, BoardCell to)
		{
			return 1;
		}

		private static IComparer<BoardCell> DistanceComparer(
			Func<BoardCell, BoardCell, int> distanceFunction,
			Dictionary<BoardCell, int> distances)
		{
			Comparison<BoardCell> comparisonFunction = (x, y) =>
				distances[x].CompareTo(distances[y]);
			return Comparer<BoardCell>.Create(comparisonFunction);
		}

		private static void SetInfiniteDistanceIfAbsent(
			Dictionary<BoardCell, int> distances,
			BoardCell cell)
		{
			if (!distances.ContainsKey(cell))
				distances[cell] = int.MaxValue;
		}
	}
}