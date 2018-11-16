using System.Linq;
using System.Collections.Generic;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Actions;

namespace HexesOfMortvell.GameModes
{
	public class KnockbackActivation : DamageActivation
	{
		public override void Perform(
			BoardCellContent actor,
			IEnumerable<BoardCell> targets,
			IEnumerable<BoardCell> aoe)
		{
			// TODO only checks target cells, not whole AOE
			var target = new List<BoardCell>(targets)[0];
			var realAoe = new List<BoardCell>(aoe);
			if (!target.Empty) {
				Direction direction = actor.Cell.Position
					.StraightLineDirectionTowards(target.Position).Value;
				BoardCell knockedBack = target.FindAdjacentCell(direction);
				if (knockedBack != null) {
					if (knockedBack.Empty) {
						realAoe.Add(knockedBack);
						target.MoveContentTo(knockedBack);
					}
				}
			}
			base.Perform(actor, targets, aoe);
		}

		public override void Cleanup(IEnumerable<BoardCell> aoe) {
			base.Cleanup(aoe);
		}
	}
}
