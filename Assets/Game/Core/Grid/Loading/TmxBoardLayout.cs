using UnityEngine;
using HexesOfMortvell.Util;

namespace HexesOfMortvell.Core.Grid.Loading
{
	[CreateAssetMenu(
		fileName="New TMX Layout",
		menuName="HexesOfMortvell/TMX Layout")]
	public class TmxBoardLayout : ScriptableObject
	{
		public TextAsset tmxFile;
		public TmxTerrainSet terrainSet;
		public TmxSpawnSet spawnSet;

		public BoardLayout ToBoardLayout()
		{
			return TiledIntegration.TmxToBoardLayout(
				tmxFile.text, terrainSet.AsDict(), spawnSet.AsDict());
		}
	}
}
