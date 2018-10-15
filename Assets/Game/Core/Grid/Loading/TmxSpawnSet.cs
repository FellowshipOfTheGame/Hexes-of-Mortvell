using System;
using System.Collections.Generic;
using UnityEngine;

namespace HexesOfMortvell.Core.Grid.Loading
{
	public class TmxSpawnSet : ScriptableObject
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
			// TODO
			return null;
		}
	}
}
