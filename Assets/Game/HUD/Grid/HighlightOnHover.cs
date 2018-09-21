using System;
using UnityEngine;

namespace HexesOfMortvell.Hud.Grid
{
	public class HighlightOnHover : MonoBehaviour
	{
		[Header("References")]
		public LayeredHighlight highlight;

		[Header("Values")]
		public Color highlightColor;

		private IDisposable highlightLayer;

		public void ApplyHighlight()
		{
			this.highlightLayer = this.highlight.AddLayer(this.highlightColor);
		}

		public void DisposeHighlight()
		{
			this.highlightLayer.Dispose();
		}
	}
}
