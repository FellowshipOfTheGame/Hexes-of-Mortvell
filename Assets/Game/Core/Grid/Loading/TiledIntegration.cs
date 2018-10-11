using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexesOfMortvell.Core.Grid.Loading
{
	public static class TiledIntegration
	{
		public static BoardLayout TmxToBoardLayout(string tmxContent)
		{
			var tmx = new XmlDocument();
			tmx.LoadXml(tmxContent);
			var layers = tmx.SelectNodes("layer").Cast<XmlNode>().ToList();
			var terrainLayer = layers.Single(NameEquals("Terrain"));
			var objectsLayer = layers.Single(NameEquals("Objects"));

			var layout = ScriptableObject.CreateInstance<BoardLayout>();
			FillLayout(layout, terrainLayer, objectsLayer);
			return layout;
		}

		private static void FillLayout(
			BoardLayout layout,
			XmlNode terrainLayer,
			XmlNode objectsLayer)
		{
			
		}

		private static Func<XmlNode, bool> NameEquals(string name)
		{
			Predicate<XmlNode> hasGivenName =
				(XmlNode node) => node.Attributes["name"].Value == name;
			return new Func<XmlNode, bool>(hasGivenName);
		}
	}
}