using UnityEngine;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Regions;
using HexesOfMortvell.Core.Units;
using System.Collections.Generic;

namespace HexesOfMortvell.Testing.ActionsTest
{
	public class ActionstestMeleeActivation : ActionActivation
	{
		public int damage;

		public override void Perform(
			BoardCellContent actor,
			IEnumerable<BoardCell> targets,
			IEnumerable<BoardCell> aoe)
		{
			foreach (var cell in aoe)
			{
				Debug.Log(cell);
				Debug.Log(cell.Content);
				var hp = cell.Content?.GetComponent<HP>();
				if (hp == null)
					continue;
				hp.Decrease(this.damage);
				hp.Commit(); // don't actually do this in the real thing
			}
		}

		public override void Cleanup(IEnumerable<BoardCell> aoe) {}
	}
}
