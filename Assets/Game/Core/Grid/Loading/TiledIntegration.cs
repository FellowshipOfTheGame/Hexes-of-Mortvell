using System;
using System.Xml;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace HexesOfMortvell.Core.Grid.Loading
{
	public static class TiledIntegration
	{
		public static BoardLayout TmxToBoardLayout(string tmxContent)
		{
			var tmx = new XmlDocument();
			tmx.LoadXml(tmxContent);

			var layers = FindLayers(tmx);
			var terrainData = FindLayerData(layers, "Terrain");
			var objectsData = FindLayerData(layers, "Objects");

			var layout = ScriptableObject.CreateInstance<BoardLayout>();
			FillLayout(layout, terrainData, objectsData);
			return layout;
		}

		private static void FillLayout(
			BoardLayout layout,
			XmlNode terrainData,
			XmlNode objectsData)
		{
			
		}

		private static Tuple<int, int> CsvPositionToBoardPosition()
		{
			return null;
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