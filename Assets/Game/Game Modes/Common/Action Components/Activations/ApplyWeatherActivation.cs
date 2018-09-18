using UnityEngine;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;
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

		public override void Perform(
			BoardCellContent actor,
			IEnumerable<BoardCell> targets,
			IEnumerable<BoardCell> aoe)
		{
			foreach (var cell in aoe)
				ApplyWeather(cell);
		}

		public override void Cleanup(IEnumerable<BoardCell> aoe) {}

		void ApplyWeather(BoardCell cell)
		{
			if (this.weathersNeutralized.Contains(cell.Weather))
				cell.SetWeather(null);
			else
				cell.SetWeather(this.weatherApplied);
		}
	}
}
