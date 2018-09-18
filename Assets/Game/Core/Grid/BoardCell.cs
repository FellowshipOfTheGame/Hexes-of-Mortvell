using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexesOfMortvell.Core.Grid
{
	/// <summary>
	/// A single hexagon in the board grid.
	/// </summary>
	[RequireComponent(typeof(SpriteRenderer))]
	[DisallowMultipleComponent]
	public class BoardCell : MonoBehaviour
	{
		[Header("Grid pixel positioning")]
		[Tooltip("Width of the sprite in units.")]
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

		// Overrides the default behaviour to GetComponent<Transform> on
		// every access.
		public Transform Transform
		{
			get;
			private set;
		}
		private SpriteRenderer spriteRenderer;

		[Header("Values")]

		[Tooltip("Added to base movement cost.")]
		public int movementCostModifier = 0;

		[Header("References")]

		[SerializeField]
		[Tooltip("The object this cell holds.")]
		private BoardCellContent _content;

		public GameObject eventListener;

		/// <summary>
		/// Retrieves the object the cell is currently holding.
		/// </summary>
		/// <value>The BoardCellContent of the held object.</value>
		public BoardCellContent Content
		{
			get { return GetContent(); }
			set { SetContent(value); }
		}

		private GameObject _weather;

		/// <summary>
		/// Retrieves the instance of the cell's current weather.
		/// </summary>
		public GameObject Weather
		{
			get { return GetWeather(); }
			private set { SetWeather(value); }
		}

		[SerializeField]
		[Tooltip("This cell's terrain type.")]
		private BoardCellTerrain _terrain;

		/// <summary>
		/// Gets or sets the cell's terrain type.
		/// </summary>
		/// <remarks>
		/// The cell's sprite will automatically change to the
		/// terrain's sprite
		/// </remarks>
		public BoardCellTerrain Terrain
		{
			get { return GetTerrain(); }
			set { SetTerrain(value); }
		}

		/// <summary>
		/// Checks whether or not the cell is holding an object.
		/// </summary>
		/// <value>true if there is no object; false otherwise.</value>
		public bool Empty
		{
			get { return this.Content == null; }
		}


		void Awake()
		{
			this.Transform = GetComponent<Transform>();
			this.board = this.Transform.parent.GetComponent<Board>();
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

		/// <summary>
		/// Sets the content of the cell.
		/// </summary>
		/// <param name="content">The new content for the cell.</param>
		/// <remarks>
		/// If the cell is already occupied, an InvalidOperationException will
		/// be thrown.
		/// </remarks>
		public void SetContent(BoardCellContent content)
		{
			if (content != null)
				ErrorIfOccupied();
			this._content = content;
			this.Content?.SetCell(this);
		}

		/// <summary>
		/// Transfers the contents from this cell to another.
		/// </summary>
		/// <param name="to">The transfer destination.</param>
		/// <remarks>
		/// <para>
		/// If this cell is empty, an InvalidOperationException will be thrown.
		/// </para>
		/// <para>
		/// If the destination cell is not empty, an InvalidOperationException
		/// will be thrown.
		/// </para>
		/// </remarks>
		public void MoveContentTo(BoardCell to)
		{
			ErrorIfEmpty();
			var content = GetContent();
			SetContent(null);
			to.SetContent(content);
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

		public GameObject GetWeather()
		{
			return this._weather;
		}

		public void SetWeather(GameObject weatherPrefab)
		{
			if (this.Weather != null)
				Destroy(this.Weather);
			this._weather = Instantiate(
				weatherPrefab, this.Transform, false);
		}

		void ErrorIfOccupied()
		{
			if (!this.Empty)
				throw new InvalidOperationException("Cell is not empty");
		}

		void ErrorIfEmpty()
		{
			if (this.Empty)
				throw new InvalidOperationException("Cell is empty");
		}

		void UpdateName()
		{
			this.gameObject.name = this.ToString();
		}

		void UpdateTerrainSprite()
		{
			this.spriteRenderer.sprite = this.Terrain.sprite;
		}

		/// <summary>
		/// Finds the cell adjacent to this one in a given direction.
		/// </summary>
		/// <param name="direction">The direction in which to look for a new cell.</param>
		/// <returns>The adjacent cell, if it exists; null otherwise.</returns>
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

		/// <summary>
		/// Enumerates over all cells adjacent to this one.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Directions in which this cell has no neighbor are ignored.
		/// </para>
		/// <para>
		/// The order of the cells returned is the same as the order of
		/// the directions in Direction.List.
		/// </para>
		/// </remarks>
		/// <returns>Returns an iterator over the adjacent cells.</returns>
		public IEnumerable<BoardCell> FindAdjacentCells()
		{
			return Direction.NonStayDirections
				.Select(
					(Direction direction) => FindAdjacentCell(direction))
				.Where(
					(BoardCell cell) => cell != null);
		}

		void UpdateTransformPosition()
		{
			this.Transform.localPosition = BoardPositionToWorldPosition();
		}
		Vector2 BoardPositionToWorldPosition()
		{
			float x = (this.Position.X + this.Position.Y/2.0f);
			float y = this.Position.Y * (1 - this.rowVerticalOverlap);
			return new Vector2(x, y);
		}

		public override string ToString()
		{
			return $"Cell @ {this.Position.ToString()}";
		}
	}
}