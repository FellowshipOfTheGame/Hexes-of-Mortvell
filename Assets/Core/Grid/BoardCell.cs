using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexCasters.Core.Grid
{
	[RequireComponent(typeof(SpriteRenderer))]
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
		private SpriteRenderer spriteRenderer;

		[SerializeField]
		private BoardCellContent _content;
		public BoardCellContent Content
		{
			get { return GetContent(); }
			set { SetContent(value); }
		}

		[SerializeField]
		private BoardCellTerrain _terrain;
		public BoardCellTerrain Terrain
		{
			get { return GetTerrain(); }
			set { SetTerrain(value); }
		}

		public bool Empty
		{
			get { return this.Content == null; }
		}


		void Awake()
		{
			this.transform = GetComponent<Transform>();
			this.board = this.transform.parent.GetComponent<Board>();
			this.spriteRenderer = GetComponent<SpriteRenderer>();
		}

		void Start()
		{
			UpdateTerrainSprite();
		}

		public BoardCellContent GetContent()
		{
			return this._content;
		}

		public void SetContent(BoardCellContent content)
		{
			if (content != null)
				ErrorIfOccupied();
			this._content = content;
			this.Content?.SetCell(this);
		}

		public BoardPosition GetPosition()
		{
			return this._position;
		}

		public void SetPosition(BoardPosition position)
		{
			this._position = position;
			UpdateName();
			UpdateTransformPosition();
		}

		public BoardCellTerrain GetTerrain()
		{
			return this._terrain;
		}

		public void SetTerrain(BoardCellTerrain terrain)
		{
			this._terrain = terrain;
			UpdateTerrainSprite();
		}


		private void ErrorIfOccupied()
		{
			if (this._content != null)
				throw new InvalidOperationException("Cell is not empty");
		}

		void UpdateName()
		{
			this.gameObject.name = this.ToString();
		}

		void UpdateTerrainSprite()
		{
			this.spriteRenderer.sprite = this.Terrain.sprite;
		}

		public BoardCell FindAdjacentCell(Direction direction)
		{
			var adjacentPosition = this.Position + direction;
			try
			{
				return this.board[adjacentPosition];
			}
			catch (IndexOutOfRangeException)
			{
				return null;
			}
		}

		public IEnumerable<BoardCell> FindAdjacentCells()
		{
			return Direction.List
				.Select(
					(Direction direction) => FindAdjacentCell(direction))
				.Where(
					(BoardCell cell) => cell != null);
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

		public override string ToString()
		{
			return $"Cell @ {this.Position.ToString()}";
		}
	}
}