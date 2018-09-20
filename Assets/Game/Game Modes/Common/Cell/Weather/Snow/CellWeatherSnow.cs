using UnityEngine;
using HexesOfMortvell.Core.Grid;

namespace HexesOfMortvell.GameModes.Common
{
	public class CellWeatherSnow : MonoBehaviour
	{
		[Header("Values")]
		public int movementCostModifier = 1;

		private BoardCell cell;

		void Awake()
		{
			this.cell = GetComponentInParent<BoardCell>();
			this.cell.movementCostModifier += this.movementCostModifier;
		}

		void OnDestroy()
		{
			this.cell.movementCostModifier -= this.movementCostModifier;
		}
	}
}
