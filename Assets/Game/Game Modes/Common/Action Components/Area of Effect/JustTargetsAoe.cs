using System.Collections.Generic;
using HexCasters.Core.Actions;
using HexCasters.Core.Grid;

namespace HexCasters.GameModes.Common
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