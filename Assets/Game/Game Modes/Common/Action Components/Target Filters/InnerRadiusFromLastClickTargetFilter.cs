using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Regions;
using HexesOfMortvell.Core.Units;

namespace HexesOfMortvell.GameModes.Common
{
	public class InnerRadiusFromLastClickTargetFilter : RadiusFromLastClickTargetFilter
	{
		public int innerRadius;

		public override IEnumerable<BoardCell> ValidTargets(
			BoardCellContent actor,
			IEnumerable<BoardCell> partialTargets)
		{
			var validTargets = base.ValidTargets(actor, partialTargets).ToList();
			var possibleTargets =
				GetRadiusFromLastClick(actor, partialTargets, innerRadius);
			foreach (var cell in possibleTargets) {
				validTargets.Add(cell);
			}
			return validTargets;
		}
	}
}
