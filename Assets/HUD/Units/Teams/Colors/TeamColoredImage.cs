using UnityEngine;
using UnityEngine.UI;
using HexCasters.Core.Units.Teams;

public class TeamColoredImage : TeamColoredComponent
{
	public Image component;

	public override void UpdateColor(Color color)
	{
		this.component.color = color;
	}
}