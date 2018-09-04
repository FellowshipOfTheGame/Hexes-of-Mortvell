using System.Collections.Generic;
using HexCasters.Core.Grid;
using HexCasters.Core.Actions;

namespace HexCasters.GameModes.Common
{
	public class MoveActivation : ActionActivation
	{
		public override void Perform(
			BoardCellContent actor,
			IEnumerable<BoardCell> targets,
			IEnumerable<BoardCell> aoe)
		{
			var targetList = new List<BoardCell>(targets);
			targetList[0].MoveContentTo(targetList[1]);
		}
	}
}