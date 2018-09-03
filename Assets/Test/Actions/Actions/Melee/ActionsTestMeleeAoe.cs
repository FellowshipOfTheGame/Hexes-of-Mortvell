using UnityEngine;
using HexCasters.Core.Actions;
using HexCasters.Core.Grid;
using System.Collections.Generic;

namespace HexCasters.Testing.ActionsTest
{
	public class ActionsTestMeleeAoe : ActionAoe
	{
		public override IEnumerable<BoardCell> GetAoe(
			IEnumerable<BoardCell> targets)
		{
			return targets;
		}
	}
}
