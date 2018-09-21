using System.Linq;
using System.Collections.Generic;
using UnityEngine;
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

		/// <summary>
		/// Returns the corresponding colors that should be given to the cells.
		/// </summary>
		/// <param name="cells">The cells to be checked.</param>
		public IEnumerable<Color> GetColors(IEnumerable<BoardCell> cells)
		{
			return cells.Select(GetColor);
		}
	}
}
