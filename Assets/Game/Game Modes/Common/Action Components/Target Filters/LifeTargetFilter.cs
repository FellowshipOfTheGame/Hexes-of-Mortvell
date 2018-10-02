using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Regions;
using HexesOfMortvell.Core.Units;

namespace HexesOfMortvell.GameModes.Common
{
	public class LifeTargetFilter : RadiusFromLastClickTargetFilter
	{
		public int skeletonSpawnRange;

		public override IEnumerable<BoardCell> ValidTargets(
			BoardCellContent actor,
			IEnumerable<BoardCell> partialTargets)
		{
			var validTargets = new HashSet<BoardCell>(
				base.ValidTargets(actor, partialTargets));
			if (actor.GetComponent<SkeletonBinding>() == null)
			{
				var skeletonSpawnTargets =
					GetRadiusFromLastClick(
						actor,
						partialTargets,
						skeletonSpawnRange);
				foreach (var cell in skeletonSpawnTargets)
					validTargets.Add(cell);
			}
			return validTargets;
		}
	}
}
