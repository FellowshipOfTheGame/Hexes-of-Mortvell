using System.Collections.Generic;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;

namespace HexesOfMortvell.GameModes.Common
{
	public class JustTargetsAoe : ActionAoe
	{
		public override IEnumerable<BoardCell> GetAoe(
			IEnumerable<BoardCell> targets)
		{
			return targets;
		}
	}
}