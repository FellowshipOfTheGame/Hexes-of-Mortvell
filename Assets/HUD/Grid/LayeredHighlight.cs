using System;
using UnityEngine;

namespace HexCasters.Testing.ActionsTest
{
	public class LayeredHighlight : MonoBehaviour
	{
		public SpriteRenderer highlightRenderer;
		public float maxAlpha;

		private HighlightLayer baseLayer;

		private HighlightLayer TopLayer => this.baseLayer.layerBelow;


		void Awake()
		{
			this.baseLayer = new HighlightLayer(this, Color.clear);
		}

		public IHighlightLayer AddLayer(Color color)
		{
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
			}

			public HighlightLayer(LayeredHighlight layers, Color color)
			{
				this.layerBelow = this;
				this.layerAbove = this;
				this.Color = color;
				this.layers = layers;
				this.layers.UpdateRendererColor();
			}

			public void Dispose()
			{
				this.layerBelow.layerAbove = this.layerAbove;
				this.layerAbove.layerBelow = this.layerBelow;
				this.layers.UpdateRendererColor();
			}
		}
	}
}