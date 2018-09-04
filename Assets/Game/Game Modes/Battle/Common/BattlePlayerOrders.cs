using System.Collections.Generic;
using UnityEngine;
using HexCasters.Core.Grid;
using HexCasters.Core.Units;

namespace HexCasters.GameModes.Battle.Common
{
	public class BattlePlayerOrders : MonoBehaviour
	{
		public Unit unit;
		public GameObject action;
		public List<BoardCell> actionTargets;
		public BoardCell movementOrigin;
		public BoardCell movementDestination;
	}
}