using UnityEngine;
using HexesOfMortvell.Util;

namespace HexesOfMortvell.Core.Grid.Loading
{
	public class TmxBoardLayout : ScriptableObject
	{
		public TextAsset tmxFile;
		public TmxTerrainSet terrainSet;

		public BoardLayout ToBoardLayout()
		{
			return TiledIntegration.TmxToBoardLayout(
				tmxFile.text, terrainSet.AsDict(), spawnSet.AsDict());
		}
	}
}
