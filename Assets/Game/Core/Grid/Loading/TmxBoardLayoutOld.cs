using UnityEngine;
using HexesOfMortvell.Util;

namespace HexesOfMortvell.Core.Grid.Loading
{
	// [CreateAssetMenu(
	// 	fileName="New TMX Layout",
	// 	menuName="HexesOfMortvell/TMX Layout")]
	public class TmxBoardLayoutOld : ScriptableObject
	{
		public TextAsset tmxFile;
		public TmxTerrainSetOld terrainSet;
		public TmxSpawnSetOld spawnSet;

		public BoardLayout ToBoardLayout()
		{
			return TiledIntegration.TmxToBoardLayout(
				tmxFile.text, terrainSet.AsDict(), spawnSet.AsDict());
		}
	}
}
