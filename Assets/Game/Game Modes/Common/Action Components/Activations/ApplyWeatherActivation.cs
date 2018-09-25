using UnityEngine;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;
using System.Linq;
using System.Collections.Generic;

namespace HexesOfMortvell.GameModes.Common
{
	public class ApplyWeatherActivation : ActionActivation
	{
		[Tooltip("Weather to be applied to the cells.")]
		public GameObject weatherApplied;

		[Tooltip("Weathers which this spell will neutralize. " +
		"Spells not in this list will be replaced by the applied weather.")]
		public List<GameObject> weathersNeutralized = new List<GameObject>();
		private List<BoardWeather> weathersNeutralizedComponents;

		[Tooltip("Weathers which this spell cannot replace.")]
		public List<GameObject> strongerWeathers = new List<GameObject>();
		private List<BoardWeather> strongerWeatherComponents;

		public override void Perform(
			BoardCellContent actor,
			IEnumerable<BoardCell> targets,
			IEnumerable<BoardCell> aoe)
		{
			GetWeathersFromPrefabs();
			foreach (var cell in aoe)
				ApplyWeather(cell);
		}

		public override void Cleanup(IEnumerable<BoardCell> aoe) {}

		void GetWeathersFromPrefabs()
		{
			this.weathersNeutralizedComponents =
				this.weathersNeutralized.Select(
					obj => obj.GetComponent<BoardWeather>())
				.ToList();
			this.strongerWeatherComponents =
				this.strongerWeathers.Select(
					obj => obj.GetComponent<BoardWeather>())
				.ToList();
		}

		void ApplyWeather(BoardCell cell)
		{
			if (this.strongerWeatherComponents.Contains(cell.Weather))
				return;
			if (this.weathersNeutralizedComponents.Contains(cell.Weather))
			{
				cell.ReplaceWeather(null);
			}
			else
			{
				cell.ReplaceWeather(this.weatherApplied);
			}
		}
	}
}
