using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Regions;

namespace HexesOfMortvell.Hud.Grid
{
	/// <summary>
	/// Used for highlighting multiple cells at once.
	/// </summary>
	public class HighlightedRegion : IDisposable
	{
		private List<IDisposable> highlights;

		/// <summary>
		/// Adds a layer of highlight with a given color to all cells.
		/// </summary>
		/// <param name="cellHighlights">The LayeredHighlight components to be used.</param>
		/// <param name="color">The color of the new layer.</param>
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
			IEnumerable<LayeredHighlight> cellHighlights,
			IEnumerable<Color> colors)
		{
			this.highlights = new List<IDisposable>();
			var highlightsAndColors = cellHighlights
				.Zip(
					colors,
					Tuple.Create);
			foreach (var tuple in highlightsAndColors)
			{
				var layeredHighlight = tuple.Item1;
				var color = tuple.Item2;
				var layer = layeredHighlight.AddLayer(color);
				this.highlights.Add(layer);
			}
		}

		public void Dispose()
		{
			foreach (var layer in this.highlights)
				layer.Dispose();
		}
	}

	public static class BoardRegionLayeredHighlightExtensions
	{
		/// <summary>
		/// Adds a layer of highlight with a given color to all cells.
		/// </summary>
		/// <param name="region">The cells to be highlighted.</param>
		/// <param name="color">The color of the new layer.</param>
		/// <returns>The highlighted region to be Disposed of when done.</returns>
		public static HighlightedRegion AddHighlightLayer(
			this IEnumerable<BoardCell> region,
			Color color)
		{
			var highlightComponents = region
				.Select(cell => cell.GetComponent<LayeredHighlight>());
			return new HighlightedRegion(highlightComponents, color);
		}

		/// <summary>
		/// Adds a layer of highlight to all cells with corresponding colors.
		/// </summary>
		/// <param name="region">The cells to be highlighted.</param>
		/// <param name="colors">The colors of the new layer for each cell.</param>
		/// <returns>The highlighted region to be Disposed of when done.</returns>
		public static HighlightedRegion AddHighlightLayer(
			this IEnumerable<BoardCell> region,
			IEnumerable<Color> colors)
		{
			var highlightComponents = region
				.Select(cell => cell.GetComponent<LayeredHighlight>());
			return new HighlightedRegion(highlightComponents, colors);
		}

		/// <summary>
		/// Adds a layer of highlight with a given color to all cells.
		/// </summary>
		/// <param name="regionHighlights">The LayeredHighlight components of the cells to be highlighted.</param>
		/// <param name="color">The color of the new layer.</param>
		/// <returns>The highlighted region to be Disposed of when done.</returns>
		public static HighlightedRegion AddHighlightLayer(
			this IEnumerable<LayeredHighlight> regionHighlights,
			Color color)
		{
			return new HighlightedRegion(regionHighlights, color);
		}

		/// <summary>
		/// Adds a layer of highlight to all cells with corresponding colors.
		/// </summary>
		/// <param name="regionHighlights">The LayeredHighlight components cells to be highlighted.</param>
		/// <param name="colors">The colors of the new layer for each cell.</param>
		/// <returns>The highlighted region to be Disposed of when done.</returns>
		public static HighlightedRegion AddHighlightLayer(
			this IEnumerable<LayeredHighlight> regionHighlights,
			IEnumerable<Color> colors)
		{
			return new HighlightedRegion(regionHighlights, colors);
		}
	}
}