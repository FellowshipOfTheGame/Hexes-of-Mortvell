using UnityEngine;
using HexCasters.Core.Actions;
using HexCasters.Core.Grid;
using HexCasters.Core.Grid.Regions;
using HexCasters.Core.Units;
using System.Collections.Generic;

namespace HexCasters.Testing.ActionsTest
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
	}
}
