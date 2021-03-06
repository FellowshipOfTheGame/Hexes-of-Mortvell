using UnityEngine;

namespace HexesOfMortvell.Core.Grid
{
	[DisallowMultipleComponent]
	public class BoardCellContent : MonoBehaviour
	{
		[SerializeField]
		private BoardCell _cell;

		[HideInInspector]
		public new Transform transform;

		public BoardCell Cell
		{
			get { return GetCell(); }
			private set { SetCell(value); }
		}

		void Awake()
		{
			this.transform = GetComponent<Transform>();
		}

		void OnDestroy()
		{
			this.Cell.Content = null;
		}

		public BoardCell GetCell()
		{
			return this._cell;
		}

		/// <summary>
		/// To be used only by BoardCell.
		/// Sets the reference of this content to a new cell.
		/// DOES NO FURTHER PROCESSING LIKE UPDATING THE CELL CONTENT.
		/// </summary>
		/// <param name="cell">The destination cell.</param>
		public void SetCell(BoardCell cell)
		{
			this._cell = cell;
			this.transform.SetParent(cell.Transform, false);
		}
	}
}