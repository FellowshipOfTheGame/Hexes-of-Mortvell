using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace HexesOfMortvell.Core.Grid.Loading
{
	// [CreateAssetMenu(
	// 	fileName="New TMX Spawn Set",
	// 	menuName="HexesOfMortvell/TMX Spawn Set")]
	public class TmxSpawnSetOld : ScriptableObject
	{
		[Serializable]
		public struct TmxSpawnInfo
		{
			public string id;
			public BoardLayout.SpawnInformation spawnInfo;
		}

		public List<TmxSpawnInfo> spawnTypes;

		public Dictionary<string, BoardLayout.SpawnInformation> AsDict()
		{
			return spawnTypes.ToDictionary(s => s.id, s => s.spawnInfo);
		}
	}
}
