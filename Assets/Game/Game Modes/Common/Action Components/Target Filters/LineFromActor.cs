using System.Collections.Generic;
using HexCasters.Core.Actions;
using HexCasters.Core.Grid;

namespace HexCasters.GameModes.Common
{
	public class LineFromActor : ActionTargetFilter
	{
		public override int TargetCount => 1;

		public override IEnumerable<BoardCell> ValidTargets(
			BoardCellContent actor,
			IEnumerable<BoardCell> partialTargets)
		{
			throw new System.NotImplementedException();
		}
	}
}