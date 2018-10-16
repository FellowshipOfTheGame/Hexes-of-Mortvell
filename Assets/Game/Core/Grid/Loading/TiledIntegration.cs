using System;
using System.Xml;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Util;

namespace HexesOfMortvell.Core.Grid.Loading
{
	public static class TiledIntegration
	{
		public static BoardLayout TmxToBoardLayout(
			string tmxContent,
			Dictionary<string, BoardCellTerrain> terrainTypes,
			Dictionary<string, BoardLayout.SpawnInformation> spawnTypes)
		{
			var tmx = new XmlDocument();
			tmx.LoadXml(tmxContent);

			var layers = FindLayers(tmx);
			var terrainData = FindLayerData(layers, "Terrain");
			var objectsData = FindLayerData(layers, "Objects");

			var layout = ScriptableObject.CreateInstance<BoardLayout>();
			FillLayout(
				layout,
				terrainData,
				objectsData,
				terrainTypes,
				spawnTypes);
			return layout;
		}

		private static void FillLayout(
			BoardLayout layout,
			XmlNode terrainData,
			XmlNode objectsData,
			Dictionary<string, BoardCellTerrain> terrainTypes,
			Dictionary<string, BoardLayout.SpawnInformation> spawnTypes,
			string defaultTerrainId="1")
		{
			var terrainCsv = terrainData.InnerText;
			var terrainMatrix = CsvReader
				.FromText(terrainCsv, new[] { ',' })
				.Select(row => row.ToList())
				.ToList();
			var objectsCsv = objectsData.InnerText;
			var objectsMatrix = CsvReader
				.FromText(objectsCsv, new[] { ',' })
				.Select(row => row.ToList())
				.ToList();

			int csvNRows = terrainMatrix.Count;
			int csvNCols = terrainMatrix[0].Count;

			layout.NumRows = csvNRows;
			layout.NumCols = csvNCols - csvNRows/2;

			layout.defaultTerrain = terrainTypes[defaultTerrainId];

			layout.nonDefaultTerrainPositions = new List<BoardPosition>();
			layout.nonDefaultTerrains = new List<BoardCellTerrain>();

			layout.spawnPositions = new List<BoardPosition>();
			layout.spawnInfo = new List<BoardLayout.SpawnInformation>();

			int rowIndex = 0;
			foreach (var terrainObjectRow in
				terrainMatrix.Zip(objectsMatrix, Tuple.Create))
			{
				int colIndex = 0;
				var terrainRow = terrainObjectRow.Item1;
				var objectsRow = terrainObjectRow.Item2;
				foreach (var positionInfo in
					terrainRow.Zip(objectsRow, Tuple.Create))
				{
					var terrainId = positionInfo.Item1;
					var objectId = positionInfo.Item2;
					BoardCellTerrain terrain = null;
					BoardLayout.SpawnInformation spawnInfo = null;

					terrainTypes.TryGetValue(terrainId, out terrain);
					spawnTypes.TryGetValue(objectId, out spawnInfo);

					UpdateLayout(
						layout,
						rowIndex,
						colIndex,
						csvNRows,
						csvNCols,
						terrain,
						spawnInfo,
						terrainId == defaultTerrainId);

					colIndex++;
				}
				rowIndex++;
			}
		}

		private static void UpdateLayout(
			BoardLayout layout,
			int rowIndex,
			int colIndex,
			int nRows,
			int nCols,
			BoardCellTerrain terrain,
			BoardLayout.SpawnInformation spawnInfo,
			bool isDefaultTerrain)
		{
			var nullableBoardPosition = CsvPositionToBoardPosition(
				rowIndex,
				colIndex,
				nRows,
				nCols,
				layout.NumCols);
			if (!nullableBoardPosition.HasValue)
				return;
			var boardPosition = nullableBoardPosition.Value;
			if (!isDefaultTerrain)
			{
				layout.nonDefaultTerrainPositions.Add(boardPosition);
				layout.nonDefaultTerrains.Add(terrain);
			}

			if (spawnInfo != null)
			{
				layout.spawnPositions.Add(boardPosition);
				layout.spawnInfo.Add(spawnInfo);
			}
		}

		private static BoardPosition? CsvPositionToBoardPosition(
			int csvRow, int csvCol, int csvHeight, int csvWidth,
			int boardWidth)
		{
			int centerRow = csvHeight / 2;
			int centerCol = boardWidth / 2;
			int distFromBottomEdge = csvHeight - csvRow - 1;

			int actualFirstCol = distFromBottomEdge / 2;
			int distFromLeftEdge = csvCol - actualFirstCol;
			if (distFromLeftEdge < 0 || distFromLeftEdge >= boardWidth)
				return null;

			int x = distFromLeftEdge - centerCol;
			int y = distFromBottomEdge - centerRow;
			return new BoardPosition(x, y);
		}

		private static IEnumerable<XmlNode> FindLayers(XmlDocument tmx)
		{
			return tmx.SelectNodes("map/layer").ToEnumerable();
		}

		private static XmlNode FindLayerData(
			IEnumerable<XmlNode> layers,
			string layerName)
		{
			var layer = layers.Single(NameEquals(layerName));
			var data = layer
				.SelectNodes("data")
				.ToEnumerable()
				.Single();
			return data;
		}

		private static Func<XmlNode, bool> NameEquals(string name)
		{
			Predicate<XmlNode> hasGivenName =
				(XmlNode node) => node.Attributes["name"]?.Value == name;
			return new Func<XmlNode, bool>(hasGivenName);
		}
	}

	public static class XmlNodeListExtensions
	{
		public static IEnumerable<XmlNode> ToEnumerable(
			this XmlNodeList nodeList)
		{
			return nodeList.Cast<XmlNode>();
		}
	}
}