using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using HexCasters.Core.Grid;
using HexCasters.Core.Grid.Regions;

namespace HexCasters.Hud.Grid
{
	public class HighlightedRegion : IDisposable
	{
		private List<IDisposable> highlights;

		public HighlightedRegion(
			IEnumerable<LayeredHighlight> cellHighlights,
			Color color)
		{
			this.highlights = new List<IDisposable>();
			foreach (var cellHighlight in cellHighlights)
			{
				var layer = cellHighlight.AddLayer(color);
				this.highlights.Add(layer);
			}
		}

		public HighlightedRegion(
			IEnumerable<BoardCell> cells,
			Color color)
			: this(
				cells.Select(
					cell => cell.GetComponent<LayeredHighlight>()),
				color)
		{}

		public void Dispose()
		{
			foreach (var layer in this.highlights)
				layer.Dispose();
		}
	}

	public static class BoardRegionLayeredHighlightExtensions
	{
		public static HighlightedRegion Highlight(
			this IEnumerable<BoardCell> region,
			Color color)
		{
			return new HighlightedRegion(region, color);
		}

		public static HighlightedRegion AddLayer(
			this IEnumerable<LayeredHighlight> region,
			Color color)
		{
			return new HighlightedRegion(region, color);
		}
	}
}