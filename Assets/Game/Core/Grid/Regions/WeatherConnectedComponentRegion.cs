using System.Collections.Generic;
using UnityEngine;

namespace HexesOfMortvell.Core.Grid.Regions
{
	public static class WeatherConnectedComponentRegion
	{
		// TODO documentation
		public static IEnumerable<BoardCell> WeatherConnectedComponent(
			this BoardCell seed,
			GameObject weather,
			bool alwaysIncludeSeed=true)
		{
			if (seed.HasWeather(weather))
			{
				return FindConnectedRegion(seed, weather);
			}
			else
			{
				var region = new List<BoardCell>();
				if (alwaysIncludeSeed)
					region.Add(seed);
				return region;
			}
		}

		private static IEnumerable<BoardCell> FindConnectedRegion(
			BoardCell seed,
			GameObject weather)
		{
			Queue<BoardCell> openNodes = new Queue<BoardCell>();
			HashSet<BoardCell> closedNodes = new HashSet<BoardCell>();
			openNodes.Enqueue(seed);
			while (openNodes.Count > 0)
			{
				var currentNode = openNodes.Dequeue();
				if (closedNodes.Contains(currentNode))
					continue;

				yield return currentNode;

				closedNodes.Add(currentNode);

				foreach (var neighbor in currentNode.FindAdjacentCells())
					if (neighbor.HasWeather(weather))
						openNodes.Enqueue(neighbor);
			}
		}
	}
}
