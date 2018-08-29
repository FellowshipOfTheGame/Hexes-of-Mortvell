using UnityEngine;
using HexCasters.Core.Units.Teams;

public class TeamColoredSpriteRenderer : TeamColoredComponent
{
	public SpriteRenderer component;

	public override void UpdateColor(Color color)
	{
		this.component.color = color;
	}
}