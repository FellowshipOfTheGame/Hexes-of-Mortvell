using System.Collections.Generic;
using UnityEngine;

namespace HexesOfMortvell.Core.Grid.Loading
{
	[CreateAssetMenu(
		menuName="HexesOfMortvell/TMX Spawn Set")]
	public class TmxSpawnSet : ScriptableObject
	{
		public List<BoardLayout.SpawnInformation> elements;
	}
}