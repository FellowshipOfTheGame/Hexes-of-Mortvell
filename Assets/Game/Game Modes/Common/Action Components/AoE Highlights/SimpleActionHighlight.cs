using UnityEngine;
using HexesOfMortvell.Hud.Actions;
using HexesOfMortvell.Core.Grid;

namespace HexesOfMortvell.GameModes.Common
{
	public class SimpleActionHighlight : ActionHighlight
	{
		public Color insideAoeColor;

		public override Color GetColor(BoardCell cell)
		{
			return this.insideAoeColor;
		}
	}
}
