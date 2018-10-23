using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace HexesOfMortvell.Core.Grid.Loading
{
	// [CreateAssetMenu(
	// 	fileName="New TMX Terrain Set",
	// 	menuName="HexesOfMortvell/TMX Terrain Set")]
	public class TmxTerrainSetOld : ScriptableObject
	{
		[Serializable]
		public struct TmxTileInfo
		{
			public string id;
			public BoardCellTerrain terrain;
		}

		public List<TmxTileInfo> tileTypes;

		public Dictionary<string, BoardCellTerrain> AsDict()
		{
			return tileTypes.ToDictionary(t => t.id, t => t.terrain);
		}
	}
}
