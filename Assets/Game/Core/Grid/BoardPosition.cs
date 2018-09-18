using UnityEngine;
using System;

namespace HexesOfMortvell.Core.Grid
{
	/// <summary>
	/// A position in the board.
	/// </summary>
	/// <remarks>
	/// (0, 0) is the board center, rounded to the lower left if it has an
	/// even number of rows and/or columns.
	/// </remarks>
	[System.Serializable]
	public struct BoardPosition
	{
		[SerializeField]
		private int _x;

		[SerializeField]
		private int _y;

		/// <summary>
		/// X axis displacement from the board center.
		/// </summary>
		public int X
		{
			get { return _x; }
			private set { _x = value; }
		}

		/// <summary>
		/// Y axis displacement from the board center.
		/// </summary>
		public int Y
		{
			get { return _y; }
			private set { _y = value; }
		}

		/// <summary>
		/// Displacement from the Z = 0 axis.
		/// </summary>
		public int Z
		{
			get { return -(this.X + this.Y); }
		}

		public BoardPosition(int x, int y)
		{
			this._x = x;
			this._y = y;
		}

		public int ManhattanDistanceTo(BoardPosition position)
		{
			var direction = position - this;
			if (Math.Sign(direction.dx) == Math.Sign(direction.dy))
				return Math.Abs(direction.dx + direction.dy);
			else
				return Math.Max(Math.Abs(direction.dx), Math.Abs(direction.dy));
		}

		public override bool Equals(object obj)
		{
			if (!(obj is BoardPosition))
				return false;
			var other = (BoardPosition) obj;
			return this.X == other.X && this.Y == other.Y;
		}

		public static Direction operator -(
			BoardPosition pos1, BoardPosition pos2)
		{
			return new Direction(pos2.X - pos1.X, pos2.Y - pos1.Y);
		}

		public static bool operator ==(
			BoardPosition pos1, BoardPosition pos2)
		{
			return pos1.Equals(pos2);
		}

		public static bool operator !=(
			BoardPosition dir1, BoardPosition dir2)
		{
			return !(dir1 == dir2);
		}

		public override int GetHashCode()
		{
			return Tuple.Create(this.X, this.Y, this.Z).GetHashCode();
		}

		public override string ToString()
		{
			return $"({this.X}, {this.Y}, {this.Z})";
		}
	}
}