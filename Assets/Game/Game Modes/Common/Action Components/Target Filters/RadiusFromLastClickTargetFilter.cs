using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Regions;
using HexesOfMortvell.Core.Units;

namespace HexesOfMortvell.GameModes.Common
{
	public class RadiusFromLastClickTargetFilter : ActionTargetFilter
	{
		public int radius;
		public bool containUnit;
		public bool empty;
		public bool containWeather;

		public override IEnumerable<BoardCell> ValidTargets(
			BoardCellContent actor,
			IEnumerable<BoardCell> partialTargets)
		{
			var possibleTargets =
				GetRadiusFromLastClick(actor, partialTargets, radius);
			var validTargets = new List<BoardCell>();
			if (!this.containUnit && !this.containWeather && !this.empty)
			{
				return possibleTargets;
			}
			else
			{
				foreach (var cell in possibleTargets)
				{
					bool valid = true;
					valid &= !this.containWeather || (cell.Weather != null);
					valid &= !this.empty || cell.Empty;
					valid &= !this.containUnit ||
						(cell.Content?.GetComponent<Unit>() != null);
					if (valid)
					{
						validTargets.Add(cell);
					}
				}
				return validTargets;
			}
		}

		protected IEnumerable<BoardCell> GetRadiusFromLastClick(
			BoardCellContent actor,
			IEnumerable<BoardCell> partialTargets,
			int radius = 0)
		{
			var lastTarget = partialTargets.LastOrDefault();
			var radiusCenter = lastTarget ?? actor.Cell;
			return radiusCenter.Neighborhood(radius);
		}
	}
}
