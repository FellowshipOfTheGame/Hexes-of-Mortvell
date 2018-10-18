using System.Xml;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace HexesOfMortvell.Core.Grid.Loading
{
	public class TmxAdapter
	{
		private XmlDocument tmx;
		private List<XmlNode> layers;

		List<BoardCellTerrain> terrainTypes;
		List<BoardLayout.SpawnInformation> spawnTypes;
		List<GameObject> weatherTypes;

		public TmxAdapter(
			string tmxText,
			List<BoardCellTerrain> terrainTypes,
			List<BoardLayout.SpawnInformation> spawnTypes,
			List<GameObject> weatherTypes)
		{
			this.tmx = new XmlDocument();
			this.tmx.LoadXml(tmxText);
			this.layers = this.tmx
				.SelectNodes("map/layer")
				.ToEnumerable()
				.ToList();

			this.terrainTypes = terrainTypes;
			this.spawnTypes = spawnTypes;
			this.weatherTypes = weatherTypes;
		}

		public BoardLayout ToBoardLayout()
		{
			var terrainMatrix = FindLayerMatrix("Terrain", this.terrainTypes);
			var objectMatrix = FindLayerMatrix("Objects", this.spawnTypes);
			var weatherMatrix = FindLayerMatrix("Weather", this.weatherTypes);
			BoardLayout layout = ScriptableObject.CreateInstance<BoardLayout>();
			return layout;
		}

		T[,] FindLayerMatrix<T>(string layerName, List<T> elementTypes)
		{
			var layer = FindLayer(layerName);
			return null;
		}

		XmlNode FindLayer(string layerName)
		{
			return this.layers.Single(
				layer => layer.Attributes["name"].Value == layerName);
		}
	}
}
