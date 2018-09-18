using UnityEngine;

namespace HexesOfMortvell.Core.Grid
{
	/// <summary>
	/// A cell's terrain type.
	/// </summary>
	[CreateAssetMenu(
		fileName="New Terrain Type",
		menuName="HexesOfMortvell/Terrain")]
	public class BoardCellTerrain : ScriptableObject
	{
		/// <summary>
		/// How much moving into the cell costs in movement points.
		/// </summary>
		public int movementCost;

		/// <summary>
		/// The sprite for the cell to use.
		/// </summary>
		public Sprite sprite;
	}
}
