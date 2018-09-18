using System.Collections.Generic;
using UnityEngine;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Regions;

namespace HexesOfMortvell.GameModes.Common
{
	public class RadiusFromActorTargetFilter : ActionTargetFilter
	{
		[SerializeField]
		private int _targetCount = 1;
		public override int TargetCount => this._targetCount;

		public int radius;

		public override IEnumerable<BoardCell> ValidTargets(
			BoardCellContent actor,
			IEnumerable<BoardCell> partialTargets)
		{
			return actor.Cell.Neighborhood(this.radius);
		}
	}
}
