using UnityEngine;

namespace HexesOfMortvell.Core.Grid.Loading
{
	[CreateAssetMenu(menuName="HexesOfMortvell/TMX Board Layout")]
	public class TmxBoardLayout : ScriptableObject
	{
		public TextAsset tmxFile;
		public TmxTerrainSet terrainSet;
		public TmxSpawnSet spawnSet;
		public TmxWeatherSet weatherSet;

		public BoardLayout ToBoardLayout()
		{
			var tmxContent = tmxFile.text;
			var tmxAdapter = new TmxAdapter(
				tmxContent,
				this.terrainSet.elements,
				this.spawnSet.elements,
				this.weatherSet.elements);
			return tmxAdapter.ToBoardLayout();
		}
	}
}