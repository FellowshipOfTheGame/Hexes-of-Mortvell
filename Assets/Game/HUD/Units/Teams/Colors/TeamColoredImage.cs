using UnityEngine;
using UnityEngine.UI;
using HexesOfMortvell.Core.Units.Teams;

namespace HexesOfMortvell.Hud.Teams
{
	public class TeamColoredImage : TeamColoredComponent
	{
		public Image component;

		public override void UpdateColor(Color color)
		{
			this.component.color = color;
		}
	}
}
