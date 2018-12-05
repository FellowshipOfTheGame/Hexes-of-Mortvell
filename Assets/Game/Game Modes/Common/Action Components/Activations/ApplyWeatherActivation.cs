using UnityEngine;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;
using System.Linq;
using System.Collections.Generic;

namespace HexesOfMortvell.GameModes
{
	public class ApplyWeatherActivation : ActionActivation
	{
		[Tooltip("Weather to be applied to the cells.")]
		public GameObject weatherApplied;
		private BoardWeather weatherComponent;

		public override void Perform(
			BoardCellContent actor,
			IEnumerable<BoardCell> targets,
			IEnumerable<BoardCell> aoe)
		{
			foreach (var cell in aoe)
				ApplyWeather(cell);
		}

		public override void Cleanup(IEnumerable<BoardCell> aoe) {}

		void OnValidate()
		{
			this.weatherComponent =
				this.weatherApplied?.GetComponent<BoardWeather>();
		}

		void ApplyWeather(BoardCell cell)
		{
			this.weatherComponent.ApplyTo(cell);
		}
	}
}
