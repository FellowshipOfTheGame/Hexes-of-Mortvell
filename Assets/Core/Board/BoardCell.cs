using System;
using UnityEngine;

namespace HexCasters.Core.Board
{
	public class BoardCell : MonoBehaviour
	{
		public float spriteWidth;

		[Tooltip("Overlap in units of two adjacent rows of cells.")]
		public float rowVerticalOverlap;

		[HideInInspector]
		public Board board;
		private BoardPosition _position;
		public BoardPosition Position
		{
			get { return GetPosition(); }
			set { SetPosition(value); }
		}

		[HideInInspector]
		public new Transform transform;

		[SerializeField]
		private BoardCellContent _content;
		public BoardCellContent Content
		{
			get { return GetContent(); }
			set { SetContent(value); }
		}


		void Awake()
		{
			this.transform = GetComponent<Transform>();
			this.board = this.transform.parent.GetComponent<Board>();
		}

		public BoardCellContent GetContent()
		{
			return this._content;
		}

		public void SetContent(BoardCellContent content)
		{
			ErrorIfOccupied();
			this._content = content;
			this.Content.SetCell(this);
		}

		public BoardPosition GetPosition()
		{
			return this._position;
		}

		public void SetPosition(BoardPosition position)
		{
			this._position = position;
			UpdateTransformPosition();
		}


		private void ErrorIfOccupied()
		{
			if (this._content != null)
				throw new InvalidOperationException("Cell is not empty");
		}

		private void UpdateTransformPosition()
		{
			this.transform.localPosition = BoardPositionToWorldPosition();
		}

		private Vector2 BoardPositionToWorldPosition()
		{
			float x = (this.Position.x + this.Position.y/2.0f);
			float y = this.Position.y * (1 - this.rowVerticalOverlap);
			return new Vector2(x, y);
		}
	}
}