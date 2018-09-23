using System.Collections.Generic;
using UnityEngine;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Regions;

namespace HexesOfMortvell.GameModes.Common
{
	public class ContainingWeatherTargetFilter : RadiusFromActorTargetFilter
	{
		public override IEnumerable<BoardCell> ValidTargets(
			BoardCellContent actor,
			IEnumerable<BoardCell> partialTargets)
		{
      var possibleTargets = actor.Cell.Neighborhood(this.radius);
      var validTargets = new List<BoardCell>();
      foreach (var cell in possibleTargets) {
        if (cell.GetWeather() != null) {
          validTargets.Add(cell);
        }
      }
			return validTargets;
		}
	}
}
