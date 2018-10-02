using System.Linq;
using System.Collections.Generic;
using HexesOfMortvell.Core.Units;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;

namespace HexesOfMortvell.GameModes.Common
{
	public class SpawnSkeletonActivation : HealActivation
	{
		public override void Perform(
			BoardCellContent actor,
			IEnumerable<BoardCell> targets,
			IEnumerable<BoardCell> aoe)
		{
			var target = targets.ToList()[0];
			if (target.Empty) {
				// spawn skeleton
			}
			else {
				base.Perform(actor, targets, aoe);
			}
		}
	}
}
