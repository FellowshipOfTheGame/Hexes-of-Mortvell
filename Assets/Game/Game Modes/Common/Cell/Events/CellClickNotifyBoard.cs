using UnityEngine;
using UnityEngine.EventSystems;
using HexesOfMortvell.Core.Grid;

namespace HexesOfMortvell.GameModes
{
	public class CellClickNotifyBoard : MonoBehaviour, IPointerClickHandler
	{
		public BoardCell cell;
		private CellClickListener listener;

		void Awake()
		{
			var board = this.cell.board;
			this.listener = board.GetComponent<CellClickListener>();
		}

		//public void Click()
		//{
		//	this.listener.Notify(this.cell);
		//}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left)
				this.listener.Notify(this.cell);
		}
	}
}
