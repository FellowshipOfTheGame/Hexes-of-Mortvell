using System.Collections.Generic;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Actions;

namespace HexesOfMortvell.GameModes
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

		public override void Cleanup(IEnumerable<BoardCell> aoe) {}

		void ErrorIfTargetCountNot2(List<BoardCell> targets)
		{
			if (targets.Count != 2)
				throw new System.ArgumentException(
					$"{GetType()} requires exactly 2 targets.");
		}
	}
}