using System.Collections.Generic;
using UnityEngine;

namespace HexesOfMortvell.Core.Grid.Loading
{
	[CreateAssetMenu(
		menuName="HexesOfMortvell/TMX Terrain Set")]
	public class TmxTerrainSet : ScriptableObject
	{
		public List<BoardCellTerrain> elements;
	}
}