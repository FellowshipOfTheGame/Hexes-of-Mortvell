using System.Collections.Generic;
using UnityEngine;

namespace HexesOfMortvell.Core.Grid.Loading
{
	[CreateAssetMenu(
		menuName="HexesOfMortvell/TMX Weather Set")]
	public class TmxWeatherSet : ScriptableObject
	{
		public List<GameObject> elements;
	}
}