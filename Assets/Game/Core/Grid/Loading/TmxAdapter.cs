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

			layout.nonDefaultTerrainPositions = new List<BoardPosition>();
			layout.nonDefaultTerrains = new List<BoardCellTerrain>();

			layout.spawnPositions = new List<BoardPosition>();
			layout.spawnInfo = new List<BoardLayout.SpawnInformation>();

			layout.weatherPositions = new List<BoardPosition>();
			layout.weather = new List<GameObject>();

			layout.defaultTerrain = defaultTerrain;

			for (int i = 0; i < layout.NumRows; i++)
			{
				for (int j = 0; j < layout.NumCols; j++)
				{
					var nullablePos = CsvCoordsToBoardPosition(layout, i, j);
					if (!nullablePos.HasValue)
						continue;
					var pos = nullablePos.Value;
					if (terrainMatrix[i, j] != null)
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
			if (col < 0 || col >= layout.NumCols)
				return null;

			int centerRow = layout.NumRows / 2;
			int centerCol = layout.NumCols / 2;
			int distFromBottomEdge = layout.NumRows - row - 1;

			int x = col - centerCol;
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

			var maxIndex = layerIndexArray
				.Select(row => row.Max())
				.Max();

			int minIndex;
			if (maxIndex == 0)
				minIndex = 1;
			else
				minIndex = layerIndexArray
					.Where(row => row.Max() != 0)
					.Select(row => row.Where(id => id != 0).Min())
					.Min();
			var matrixHeight = layerIndexArray.Count;
			var matrixWidth = layerIndexArray[0].Count;
			var realWidth = matrixWidth - matrixHeight/2;
			var layerElementMatrix = new T[matrixHeight, realWidth];

			for (int csvRow = 0; csvRow < matrixHeight; csvRow++)
			{
				for (int realCol = 0; realCol < realWidth; realCol++)
				{
					var distFromBottom = matrixHeight - csvRow - 1;
					var csvCol = realCol + distFromBottom/2;
					int elementIndex = layerIndexArray[csvRow][csvCol] - minIndex;
					var e = elementTypes.ElementAtOrDefault(elementIndex);
					layerElementMatrix[csvRow, realCol] = e;
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
