using UnityEngine;
using HexesOfMortvell.Hud;
using HexesOfMortvell.Hud.Actions;
using HexesOfMortvell.Core.Grid;

namespace HexesOfMortvell.GameModes
{
	public class SimpleActionHighlight : ActionHighlight
	{
		public ColorReference insideAoeColor;

		public override Color GetColor(BoardCell cell)
		{
			return this.insideAoeColor.Value;
		}
	}
}
