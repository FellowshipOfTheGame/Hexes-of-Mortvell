using System.Collections.Generic;
using System.Linq;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Regions;

namespace HexesOfMortvell.GameModes.Common
{
	public class QueueAoe : ActionAoe
	{
		public override IEnumerable<BoardCell> GetAoe(
			IEnumerable<BoardCell> targets)
		{
			var targetList = new List<BoardCell>(targets);
			var aoe = new List<BoardCell>();
      var adjacentCells = new List<BoardCell>(targetList[0].FindAdjacentCells());
			aoe.Add(targetList[0]);
      foreach (var cell in adjacentCells) {
        aoe.Add(cell);
      }

			return aoe;
		}
	}
}
