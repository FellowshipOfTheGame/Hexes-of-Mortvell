using UnityEngine;
using HexCasters.Core.Actions;
using HexCasters.Core.Grid;
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
