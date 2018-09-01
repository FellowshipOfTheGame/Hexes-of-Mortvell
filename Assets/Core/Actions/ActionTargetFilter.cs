using UnityEngine;
using System.Collections.Generic;
using HexCasters.Core.Grid;

namespace HexCasters.Core.Actions
{
	[DisallowMultipleComponent]
	public abstract class ActionTargetFilter : MonoBehaviour
	{
		public int targetCount;

		public abstract IEnumerable<BoardCell> ValidTargets(
			BoardCellContent actor,
			IEnumerable<BoardCell> partialTargets=null);
	}
}