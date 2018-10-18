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
			var defaultTerrain = this.terrainTypes.First();
			var terrainMatrix = FindLayerMatrix("Terrain", this.terrainTypes);
			var objectMatrix = FindLayerMatrix("Objects", this.spawnTypes);
			var weatherMatrix = FindLayerMatrix("Weather", this.weatherTypes);
			BoardLayout layout = ScriptableObject.CreateInstance<BoardLayout>();
			FillLayout(
				layout,
				terrainMatrix.GetLength(0),
				terrainMatrix.GetLength(1),
				defaultTerrain,
				terrainMatrix,
				objectMatrix,
				weatherMatrix);
			return layout;
		}

		void FillLayout(
			BoardLayout layout,
			int nrows, int ncols,
			BoardCellTerrain defaultTerrain,
			BoardCellTerrain[,] terrainMatrix,
			BoardLayout.SpawnInformation[,] objectMatrix,
			GameObject[,] weatherMatrix)
		{
			layout.NumRows = nrows;
			layout.NumCols = ncols;
			layout.defaultTerrain = defaultTerrain;

			for (int i = 0; i < layout.NumRows; i++)
			{
				for (int j = 0; j < layout.NumCols; j++)
				{
					var nullablePos = CsvCoordsToBoardPosition(layout, i, j);
					if (!nullablePos.HasValue)
						continue;
					var pos = nullablePos.Value;
					if (terrainMatrix[i, j] != defaultTerrain)
					{
						layout.nonDefaultTerrainPositions.Add(pos);
						layout.nonDefaultTerrains.Add(terrainMatrix[i, j]);
					}

					if (objectMatrix[i, j] != null)
					{
						layout.spawnPositions.Add(pos);
						layout.spawnInfo.Add(objectMatrix[i, j]);
					}

					if (weatherMatrix[i, j] != null)
					{
						layout.weatherPositions.Add(pos);
						layout.weather.Add(weatherMatrix[i, j]);
					}
				}
			}
		}

		BoardPosition? CsvCoordsToBoardPosition(
			BoardLayout layout, int row, int col)
		{
			int centerRow = layout.NumRows / 2;
			int centerCol = layout.NumCols / 2;
			int distFromBottomEdge = layout.NumRows - row - 1;

			int actualFirstCol = distFromBottomEdge / 2;
			int distFromLeftEdge = col - actualFirstCol;
			if (distFromLeftEdge < 0 || distFromLeftEdge >= layout.NumCols)
				return null;

			int x = distFromLeftEdge - centerCol;
			int y = distFromBottomEdge - centerRow;
			return new BoardPosition(x, y);
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
