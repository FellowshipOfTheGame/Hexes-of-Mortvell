using UnityEngine;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;
using System.Collections.Generic;

namespace HexesOfMortvell.GameModes.Common
{
	public class SpreadWeatherActivation : ApplyWeatherActivation
	{
		public override void Perform(
			BoardCellContent actor,
			IEnumerable<BoardCell> targets,
			IEnumerable<BoardCell> aoe)
		{
      var targetList = new List<BoardCell>(targets);
      //weatherApplied = targetList[0].GetWeather();
      //weathersNeutralized = targetList[0].
      //strongerWeathers = targetList[0].
      foreach (var cell in aoe)
        cell.SetWeather(targetList[0].GetWeather());
		}
	}
}
