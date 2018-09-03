using UnityEngine;
using HexCasters.Core.Grid;
using HexCasters.Hud.Actions;
using System.Collections.Generic;

namespace HexCasters.Testing.ActionsTest
{
	public class ActionsTestMeleeAoeHighlight : ActionHighlight
	{
		public override Color GetColor(BoardCell cell)
		{
			return Color.black;
		}
	}
}
