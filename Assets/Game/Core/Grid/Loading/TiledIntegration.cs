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
			Dictionary<string, GameObject> objectTypes)
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
				objectTypes);
			return layout;
		}

		private static void FillLayout(
			BoardLayout layout,
			XmlNode terrainData,
			XmlNode objectsData,
			Dictionary<string, BoardCellTerrain> terrainTypes,
			Dictionary<string, GameObject> objectTypes)
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

			int nRows = terrainMatrix.Count;
			int nCols = terrainMatrix[0].Count;

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
					var terrain = terrainTypes[terrainId];
					var gameObject = objectTypes[objectId];

					UpdateLayout(
						layout,
						rowIndex,
						colIndex,
						nRows,
						nCols,
						terrain,
						gameObject);

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
			GameObject gameObject)
		{
			var nullableBoardPosition = CsvPositionToBoardPosition(
				rowIndex,
				colIndex,
				nRows,
				nCols);
			if (!nullableBoardPosition.HasValue)
				return;
			layout.nonDefaultTerrainPositions = null;
		}

		private static BoardPosition? CsvPositionToBoardPosition(
			int csvRow, int csvCol, int csvHeight, int csvWidth)
		{
			int centerRow = csvHeight / 2;
			int centerCol = csvWidth / 2;
			int distFromBottomEdge = csvHeight - csvRow - 1;

			int actualFirstCol = distFromBottomEdge / 2;
			int distFromLeftEdge = csvCol - actualFirstCol;
			if (distFromLeftEdge < 0)
				return null;

			int x = distFromLeftEdge - centerCol;
			int y = distFromBottomEdge - centerRow;
			return new BoardPosition(x, y);
		}

		private static IEnumerable<XmlNode> FindLayers(XmlDocument tmx)
		{
			return tmx.SelectNodes("layer").ToEnumerable();
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
				(XmlNode node) => node.Attributes["name"].Value == name;
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