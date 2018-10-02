using UnityEngine;
using System.Collections.Generic;
using HexesOfMortvell.Core.Grid;

namespace HexesOfMortvell.Core.Actions
{
	[DisallowMultipleComponent]
	/// <summary>
	/// The component of the action which determines the area affected by the selected targets.
	/// </summary>
	public abstract class ActionAoe : MonoBehaviour
	{
		/// <summary>
		/// Returns all the affected or possibly affected cells.
		/// </summary>
		/// <param name="targets">The targets for the action.</param>
		/// <returns>The cells affected when performing the action with these targets.</returns>
		public abstract IEnumerable<BoardCell> GetAoe(
			BoardCellContent actor,
			IEnumerable<BoardCell> targets);
	}
}