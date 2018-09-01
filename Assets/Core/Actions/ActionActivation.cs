using System.Collections.Generic;
using UnityEngine;
using HexCasters.Core.Grid;

namespace HexCasters.Core.Actions
{
	[DisallowMultipleComponent]
	public abstract class ActionActivation : MonoBehaviour
	{
		public abstract void Perform(
			Actor actor,
			IEnumerable<BoardCell> targets,
			IEnumerable<BoardCell> aoe);
	}
}