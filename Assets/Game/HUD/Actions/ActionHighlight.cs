using UnityEngine;
using System.Collections.Generic;
using HexesOfMortvell.Core.Grid;

namespace HexesOfMortvell.Hud.Actions
{
	[DisallowMultipleComponent]
	/// <summary>
	/// The component of the action that maps AoE cells into colors.
	/// </summary>
	public abstract class ActionHighlight : MonoBehaviour
	{
		/// <summary>
		/// Returns the color that should be given to the cell.
		/// </summary>
		/// <param name="cell">The cell to be checked.</param>
		/// <returns>The color that should be given to the cell.</returns>
		/// <remarks>
		/// <para>
		/// The implementation should assume the given cell is part of the
		/// action AoE.
		/// </para>
		/// </remarks>
		public abstract Color GetColor(BoardCell cell);
	}
}
