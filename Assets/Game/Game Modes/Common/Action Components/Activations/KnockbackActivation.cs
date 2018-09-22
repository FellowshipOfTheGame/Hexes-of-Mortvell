using System.Collections.Generic;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Actions;

namespace HexesOfMortvell.GameModes.Common
{
	public class KnockbackActivation : DamageActivation
	{
		public override void Perform(
			BoardCellContent actor,
			IEnumerable<BoardCell> targets,
			IEnumerable<BoardCell> aoe)
		{
      base.Perform(actor, targets, aoe);
      var targetList = new List<BoardCell>(targets);
      Direction direction = (Direction)DirectionCellExtensions.StraightLineDirectionTowards(
        actor.Cell.GetPosition(),
        targetList[0].GetPosition());
      BoardCell knockedBack = targetList[0].FindAdjacentCell(direction);
      if (knockedBack != null) {
        if (knockedBack.GetContent() == null)
          targetList[0].MoveContentTo(knockedBack);
      }
		}

		public override void Cleanup(IEnumerable<BoardCell> aoe) {
      base.Cleanup(aoe);
    }
	}
}
