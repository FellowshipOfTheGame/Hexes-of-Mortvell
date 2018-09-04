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
			ErrorIfTargetCountNot2(targetList);
			targetList[0].MoveContentTo(targetList[1]);
		}

		void ErrorIfTargetCountNot2(List<BoardCell> targets)
		{
			if (targets.Count != 2)
				throw new System.ArgumentException(
					$"{GetType()} requires exactly 2 targets.");
		}
	}
}