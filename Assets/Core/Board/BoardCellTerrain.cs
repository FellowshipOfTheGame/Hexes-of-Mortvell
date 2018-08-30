using UnityEngine;

namespace HexCasters.Core.Grid
{
	[CreateAssetMenu(
		fileName="New Terrain Type",
		menuName="HexCasters/Terrain")]
	public class BoardCellTerrain : ScriptableObject
	{
		public int movementCost;
	}
}
