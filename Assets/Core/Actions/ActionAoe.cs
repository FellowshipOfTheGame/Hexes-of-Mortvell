using UnityEngine;
using System.Collections.Generic;
using HexCasters.Core.Grid;

namespace HexCasters.Core.Actions
{
	[DisallowMultipleComponent]
	public abstract class ActionAoe : MonoBehaviour
	{
		public abstract IEnumerable<BoardCell> GetAoe(
			IEnumerable<BoardCell> targets);
	}
}