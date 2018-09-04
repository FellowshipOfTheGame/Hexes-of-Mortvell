using System;
using System.Collections.Generic;
using UnityEngine;
using HexCasters.Core.Grid;

namespace HexCasters.Core.Actions
{
	[DisallowMultipleComponent]
	/// <summary>
	/// The component of the action that actually does stuff.
	/// </summary>
	public abstract class ActionActivation : MonoBehaviour
	{
		/// <summary>
		/// Executes the action.
		/// </summary>
		/// <param name="actor"></param>
		/// <param name="targets"></param>
		/// <param name="aoe"></param>
		public abstract void Perform(
			BoardCellContent actor,
			IEnumerable<BoardCell> targets,
			IEnumerable<BoardCell> aoe);

		public abstract void Cleanup(IEnumerable<BoardCell> aoe);
	}
}