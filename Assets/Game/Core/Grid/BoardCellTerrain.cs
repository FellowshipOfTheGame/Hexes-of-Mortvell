using UnityEngine;

namespace HexCasters.Core.Grid
{
	/// <summary>
	/// A cell's terrain type.
	/// </summary>
	[CreateAssetMenu(
		fileName="New Terrain Type",
		menuName="HexCasters/Terrain")]
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
