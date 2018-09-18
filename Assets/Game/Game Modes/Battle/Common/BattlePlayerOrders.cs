using System.Collections.Generic;
using UnityEngine;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Units;

namespace HexesOfMortvell.GameModes.Battle.Common
{
	public class BattlePlayerOrders : MonoBehaviour
	{
		public Unit unit;
		public GameObject action;
		public List<BoardCell> actionTargets;
		public BoardCell movementOrigin;
		public BoardCell movementDestination;

		public void Clear()
		{
			this.unit = null;
			this.action  = null;
			this.actionTargets = new List<BoardCell>();
			this.movementOrigin = null;
			this.movementDestination = null;
		}
	}
}