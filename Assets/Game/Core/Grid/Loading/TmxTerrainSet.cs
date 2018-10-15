using System;
using System.Collections.Generic;
using UnityEngine;

namespace HexesOfMortvell.Core.Grid.Loading
{
	public class TmxTerrainSet : ScriptableObject
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
			// TODO
			return null;
		}
	}
}
