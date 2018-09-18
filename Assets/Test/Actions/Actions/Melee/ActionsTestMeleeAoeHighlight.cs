using UnityEngine;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Hud.Actions;
using System.Collections.Generic;

namespace HexesOfMortvell.Testing.ActionsTest
{
	public class ActionsTestMeleeAoeHighlight : ActionHighlight
	{
		public override Color GetColor(BoardCell cell)
		{
			return Color.black;
		}
	}
}
