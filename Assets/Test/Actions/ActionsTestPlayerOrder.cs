using UnityEngine;
using HexCasters.Core.Grid;

namespace HexCasters.Testing.ActionsTest
{
	public class ActionsTestPlayerOrder : MonoBehaviour
	{
		// use origin cell instead when implementing for real
		// will make undo easier
		public BoardCellContent selectedUnit;
		public GameObject action;
	}
}
