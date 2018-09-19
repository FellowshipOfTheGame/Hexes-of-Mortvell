using UnityEngine;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Regions;
using System.Collections.Generic;
using System.Linq;

namespace HexesOfMortvell.GameModes.Common
{
	public class WeatherConnectedComponentAoe : ActionAoe
	{
		public GameObject expandingWeather;

		public override IEnumerable<BoardCell> GetAoe(
			IEnumerable<BoardCell> targets)
		{
			Debug.Log("GetAoe");
			return targets.SelectMany(
				target => FindWeatherConnectedComponentAoe(target));
		}

		IEnumerable<BoardCell> FindWeatherConnectedComponentAoe(BoardCell cell)
		{
			return new HashSet<BoardCell>(
				cell.Neighborhood(0, WeatherExpansionFunction));
		}

		int WeatherExpansionFunction(BoardCell from, BoardCell to)
		{
			bool fromHasWeather = from.HasWeather(this.expandingWeather);
			bool toHasWeather = to.HasWeather(this.expandingWeather);
			Debug.Log($"{from} [{from.Weather?.name}]; {to} [{to.Weather?.name}]");
			if (fromHasWeather && toHasWeather)
				return 0;
			return 1;
		}
	}
}
