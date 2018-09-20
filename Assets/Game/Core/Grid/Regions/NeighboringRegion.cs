using System;
using System.Collections.Generic;

namespace HexesOfMortvell.Core.Grid.Regions
{
	public static class BoardNeighboringRegionExtension
	{
		/// <summary>
		/// Returns a region surrounding a cell, up to a maximum distance from the center cell.
		/// </summary>
		/// <param name="center">The point from which to start the search.</param>
		/// <param name="maxDistance">The maximum value of the distance to the center accepted.</param>
		/// <param name="distanceFunction">The function to calculate the distance between two adjacent cells. Its arguments are the origin cell and the target cell, in order.</param>
		public static IEnumerable<BoardCell> Neighborhood(
			this BoardCell center,
			int maxDistance,
			Func<BoardCell, BoardCell, int> distanceFunction=null)
		{
			distanceFunction = distanceFunction ?? DefaultDistanceFunction;
			var openNodes = new List<BoardCell>();
			var distances = new Dictionary<BoardCell, int>();
			var visited = new HashSet<BoardCell>();
			var ll = new LinkedList<BoardCell>();
			var distanceComparer = CreateComparerFromDistanceDict(
				distances);

			openNodes.Add(center);
			distances[center] = 0;

			while (openNodes.Count > 0)
			{
				openNodes.Sort(distanceComparer);

				var currentNode = openNodes[0];
				var currentDist = distances[currentNode];
				openNodes.RemoveAt(0);

				foreach (var neighbor in currentNode.FindAdjacentCells())
				{
					var distanceFromCurrentNode =
						currentDist + distanceFunction(currentNode, neighbor);

					if (distanceFromCurrentNode > maxDistance)
						continue;

					SetInfiniteDistanceIfAbsent(distances, neighbor);
					if (distanceFromCurrentNode >= distances[neighbor])
						continue;

					openNodes.Add(neighbor);
					distances[neighbor] = distanceFromCurrentNode;
				}

				if (!visited.Contains(currentNode))
				{
					visited.Add(currentNode);
					yield return currentNode;
				}
			}
		}

		static int DefaultDistanceFunction(BoardCell from, BoardCell to)
		{
			return 1;
		}

		static IComparer<BoardCell> CreateComparerFromDistanceDict(
			Dictionary<BoardCell, int> distances)
		{
			Comparison<BoardCell> comparisonFunction = (x, y) =>
				distances[x].CompareTo(distances[y]);
			return Comparer<BoardCell>.Create(comparisonFunction);
		}

		static void SetInfiniteDistanceIfAbsent(
			Dictionary<BoardCell, int> distances,
			BoardCell cell)
		{
			if (!distances.ContainsKey(cell))
				distances[cell] = int.MaxValue;
		}
	}
}