using System;
using UnityEngine;

namespace HexCasters.Hud.Grid
{
	[Obsolete("Use LayeredHighlight")]
	public class Highlight : MonoBehaviour
	{
		public SpriteRenderer highlightRenderer;
		public float maxAlpha = 1.0f;

		public Color Color
		{
			get { return highlightRenderer.color; }
			set
			{
				if (value.a > this.maxAlpha)
					value.a = this.maxAlpha;
				highlightRenderer.color = value;
			}
		}
	}
}