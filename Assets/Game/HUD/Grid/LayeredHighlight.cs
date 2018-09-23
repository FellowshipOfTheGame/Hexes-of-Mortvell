using System;
using UnityEngine;
using HexesOfMortvell.DesignPatterns.Observer;

namespace HexesOfMortvell.Hud.Grid
{
	/// <summary>
	/// Layered highlight system for board cells.
	/// </summary>
	public class LayeredHighlight : MonoBehaviour
	{
		[Tooltip("The renderer whose color will match the top layer.")]
		public SpriteRenderer highlightRenderer;

		[Tooltip("Maximum alpha value. Colors are clipped to this value.")]
		public float maxAlpha = 0.3f;

		private HighlightLayer baseLayer;

		private HighlightLayer TopLayer => this.baseLayer.layerBelow;


		void Awake()
		{
			this.baseLayer = new HighlightLayer(this, Color.clear);
			UpdateRendererColor();
		}

		/// <summary>
		/// Creates a new layer of highlight.
		/// </summary>
		/// <param name="color">The color of the layer.</param>
		/// <returns>The layer created.</returns>
		/// <remarks>
		/// <para>
		/// The layer can be Disposed of even if it is not the top layer.
		/// </para>
		/// </remarks>
		public IHighlightLayer AddLayer(Color color)
		{
			return new HighlightLayer(this, this.TopLayer, color);
		}

		public IHighlightLayer AddLayer(ColorReference colorReference)
		{
			return new HighlightLayer(this, this.TopLayer, colorReference);
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
			var color = this.TopLayer.Color;
			if (color.a > this.maxAlpha)
				color.a = this.maxAlpha;
			this.highlightRenderer.color = color;
		}

		public interface IHighlightLayer : IDisposable
		{
			/// <summary>
			/// The color of the renderer when this layer is on top.
			/// </summary>
			Color Color { get; }
		}

		private class HighlightLayer : IHighlightLayer
		{
			Color color;
			LayeredHighlight layers;
			bool disposed;
			IDisposable colorReferenceSubscription;

			public Color Color
			{
				get {
					return this.color;
				}
				private set
				{
					this.color = value;
				}
			}

			public HighlightLayer layerAbove;
			public HighlightLayer layerBelow;

			public HighlightLayer(
				LayeredHighlight layers,
				HighlightLayer layerBelow,
				ColorReference colorReference)
				: this(layers, layerBelow, colorReference.Value)
			{
				var colorChangeHandler = new ValueObserver<Color>(
					nextEventHandler: UpdateColorFromReference);
				this.colorReferenceSubscription = colorReference.AsObservable
					.Subscribe(colorChangeHandler);
			}

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
				this.colorReferenceSubscription?.Dispose();
				this.layers.UpdateRendererColor();
			}

			void UpdateColorFromReference(Color newColor)
			{
				this.color = newColor;
				this.layers.UpdateRendererColor();
			}
		}
	}
}