using System.Collections.Generic;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Actions;

namespace HexesOfMortvell.GameModes.Common
{
	public class KnockbackActivation : DamageActivation
	{
		private List<BoardCell> realAoe;

		public override void Perform(
			BoardCellContent actor,
			IEnumerable<BoardCell> targets,
			IEnumerable<BoardCell> aoe)
		{
      var targetList = new List<BoardCell>(targets);
			realAoe = new List<BoardCell>(aoe);
      Direction direction = (Direction)DirectionCellExtensions.StraightLineDirectionTowards(
        actor.Cell.GetPosition(),
        targetList[0].GetPosition());
      BoardCell knockedBack = targetList[0].FindAdjacentCell(direction);
      if (knockedBack != null) {
        if (knockedBack.GetContent() == null) {
					realAoe.Add(knockedBack);
					targetList[0].MoveContentTo(knockedBack);
				}
      }
	    base.Perform(actor, targets, realAoe);
		}

		public override void Cleanup(IEnumerable<BoardCell> aoe) {
      base.Cleanup(realAoe);
    }
	}
}
