using System;
using UnityEngine;

namespace HexCasters.Hud.Grid
{
	public class LayeredHighlight : MonoBehaviour
	{
		public SpriteRenderer highlightRenderer;
		public float maxAlpha = 0.3f;

		private HighlightLayer baseLayer;

		private HighlightLayer TopLayer => this.baseLayer.layerBelow;


		void Awake()
		{
			this.baseLayer = new HighlightLayer(this, Color.clear);
			UpdateRendererColor();
		}

		public IHighlightLayer AddLayer(Color color)
		{
			if (color.a > this.maxAlpha)
				color.a = this.maxAlpha;
			return new HighlightLayer(this, this.TopLayer, color);
		}

		public void RemoveTopLayer()
		{
			this.TopLayer.Dispose();
		}

		public void Clear()
		{
			while (this.TopLayer != this.baseLayer)
				this.RemoveTopLayer();
		}

		void UpdateRendererColor()
		{
			this.highlightRenderer.color = this.TopLayer.Color;
		}

		public interface IHighlightLayer : IDisposable
		{
			Color Color { get; set; }
		}

		private class HighlightLayer : IHighlightLayer
		{
			Color color;
			LayeredHighlight layers;
			bool disposed;


			public Color Color
			{
				get { return this.color; }
				set { this.color = value; }
			}

			public HighlightLayer layerAbove;
			public HighlightLayer layerBelow;

			public HighlightLayer(
				LayeredHighlight layers,
				HighlightLayer layerBelow,
				Color color)
				: this(layers, color)
			{
				this.layerBelow = layerBelow;
				this.layerAbove = layerBelow.layerAbove;
				this.layerBelow.layerAbove = this;
				this.layerAbove.layerBelow = this;
				this.layers.UpdateRendererColor();
			}

			public HighlightLayer(LayeredHighlight layers, Color color)
			{
				this.layerBelow = this;
				this.layerAbove = this;
				this.Color = color;
				this.layers = layers;
				this.disposed = false;
			}

			public void Dispose()
			{
				if (this.disposed)
					throw new ObjectDisposedException("Highlight layer");
				this.disposed = true;
				this.layerBelow.layerAbove = this.layerAbove;
				this.layerAbove.layerBelow = this.layerBelow;
				this.layers.UpdateRendererColor();
			}
		}
	}
}