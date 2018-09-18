using UnityEngine;
using HexesOfMortvell.Core.Units.Teams;

namespace HexesOfMortvell.Hud.Teams
{
	public class TeamColoredSpriteRenderer : TeamColoredComponent
	{
		public SpriteRenderer component;

		public override void UpdateColor(Color color)
		{
			this.component.color = color;
		}
	}
}
