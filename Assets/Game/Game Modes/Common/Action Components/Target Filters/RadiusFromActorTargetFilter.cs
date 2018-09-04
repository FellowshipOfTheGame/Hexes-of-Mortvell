using System.Collections.Generic;
using UnityEngine;
using HexCasters.Core.Actions;
using HexCasters.Core.Grid;
using HexCasters.Core.Grid.Regions;

namespace HexCasters.GameModes.Common
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
