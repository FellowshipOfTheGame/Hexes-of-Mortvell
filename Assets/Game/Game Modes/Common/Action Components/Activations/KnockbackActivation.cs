using System.Collections.Generic;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Actions;
using UnityEngine;

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
      var target = new List<BoardCell>(targets)[0];
			realAoe = new List<BoardCell>(aoe);
			if (target.GetContent() != null) {
	      Direction direction = (Direction)DirectionCellExtensions.StraightLineDirectionTowards(
	        actor.Cell.GetPosition(),
	        target.GetPosition());
	      BoardCell knockedBack = target.FindAdjacentCell(direction);
	      if (knockedBack != null) {
	        if (knockedBack.GetContent() == null) {
						realAoe.Add(knockedBack);
						target.MoveContentTo(knockedBack);
					}
	      }
			}
			base.Perform(actor, targets, realAoe);
		}

		public override void Cleanup(IEnumerable<BoardCell> aoe) {
      base.Cleanup(realAoe);
    }
	}
}
