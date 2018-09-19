using System.Collections.Generic;
using UnityEngine;

namespace HexesOfMortvell.Core.Grid.Regions
{
	public static class WeatherConnectedComponentRegion
	{
		public static IEnumerable<BoardCell> WeatherConnectedComponent(
			this BoardCell seed,
			GameObject weather)
		{
			Queue<BoardCell> openNodes = new Queue<BoardCell>();
			HashSet<BoardCell> closedNodes = new HashSet<BoardCell>();
			openNodes.Enqueue(seed);
			while (openNodes.Count > 0)
			{
				var currentNode = openNodes.Dequeue();
				closedNodes.Add(currentNode);
				if (!currentNode.HasWeather(weather))
					continue;

				yield return currentNode;
				foreach (var neighbor in currentNode.FindAdjacentCells())
					openNodes.Enqueue(neighbor);
			}
		}
	}
}
