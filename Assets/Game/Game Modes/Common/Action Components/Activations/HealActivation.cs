using System.Linq;
using System.Collections.Generic;
using HexesOfMortvell.Core.Units;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;

namespace HexesOfMortvell.GameModes
{
	public class HealActivation : ActionActivation
	{
		public int healAmount;

		public override void Perform(
			BoardCellContent actor,
			IEnumerable<BoardCell> targets,
			IEnumerable<BoardCell> aoe)
		{
			var affectedHPs = aoe
				.Where(cell => !cell.Empty)
				.Select(cell => cell.Content)
				.Select(content => content.GetComponent<HP>())
				.Where(hp => hp != null);
			foreach (var hp in affectedHPs)
			{
				hp.Increase(this.healAmount);
			}
		}

		public override void Cleanup(IEnumerable<BoardCell> aoe)
		{
			var affectedHPs = aoe
				.Select(cell => cell.Content)
				.Select(content => content?.GetComponent<HP>())
				.Where(hp => hp != null);
			foreach (var hp in affectedHPs)
			{
				hp.Commit();
			}
		}
	}
}