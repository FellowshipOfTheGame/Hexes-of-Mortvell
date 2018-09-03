using UnityEngine;
using UnityEngine.EventSystems;
using HexCasters.Core.Grid;

namespace HexCasters.GameModes.Common
{
	[RequireComponent(typeof(EventTrigger))]
	[System.Obsolete("Maybe you dont need this; if not, delete")]
	public class CellNotifyOnHover : MonoBehaviour
	{
		public BoardCell cell;
		public CellHoverListener listener;

		public void HoverEnter()
		{
			this.listener.Notify(this.cell);
		}
	}
}
