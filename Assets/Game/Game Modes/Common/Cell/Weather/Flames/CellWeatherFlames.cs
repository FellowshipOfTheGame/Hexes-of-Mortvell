using UnityEngine;
using HexesOfMortvell.Core.Units;
using HexesOfMortvell.Core.Grid;

namespace HexesOfMortvell.GameModes.Common
{
	public class CellWeatherFlames : MonoBehaviour
	{
		[Header("Values")]
		public int damagePerTurn;

		[Header("References")]
		public BoardCell cell;
		private EndTurnListener endTurnListener;

		void Awake()
		{
			this.cell = this.transform.parent.GetComponent<BoardCell>();
			var eventListener = this.cell.eventListener;
			this.endTurnListener =
				eventListener.GetComponent<EndTurnListener>();
			this.endTurnListener.turnEndedEvent += DamageCellContent;
		}

		void OnDestroy()
		{
			this.endTurnListener.turnEndedEvent -= DamageCellContent;
		}

		void DamageCellContent()
		{
			var hp = this.cell?.Content?.GetComponent<HP>();
			hp?.Decrease(this.damagePerTurn);
		}
	}
}
