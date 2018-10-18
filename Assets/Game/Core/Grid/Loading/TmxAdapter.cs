using System;
using System.Xml;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using HexesOfMortvell.Util;

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
			var layerData = layer.SelectSingleNode("data");
			var layerMatrixText = layerData.InnerText.Trim();
			var layerMatrixReader = new CsvReader<int>(
				layerMatrixText,
				Convert.ToInt32);
			var layerIndexArray = layerMatrixReader.ToMatrix();

			var minIndex = layerIndexArray
				.Select(row => row.Min())
				.Min();
			var matrixWidth = layerIndexArray.Count;
			var matrixHeight = layerIndexArray[0].Count;
			var layerElementMatrix = new T[matrixWidth, matrixHeight];

			for (int i = 0; i < matrixHeight; i++)
			{
				for (int j = 0; j < matrixWidth; j++)
				{
					int elementIndex = layerIndexArray[i][j] - minIndex;
					if (elementIndex >= elementTypes.Count)
						layerElementMatrix[i, j] = default(T);
					else
						layerElementMatrix[i, j] = elementTypes[elementIndex];
				}
			}

			return layerElementMatrix;
		}

		XmlNode FindLayer(string layerName)
		{
			return this.layers.Single(
				layer => layer.Attributes["name"].Value == layerName);
		}
	}
}
