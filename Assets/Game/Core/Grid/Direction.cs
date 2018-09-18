using System;
using System.Collections.Generic;

namespace HexesOfMortvell.Core.Grid
{
	public struct Direction
	{
		/// <summary>
		/// Displacement along X axis.
		/// </summary>
		public readonly int dx;

		/// <summary>
		/// Displacement along Y axis.
		/// </summary>
		public readonly int dy;

		/// <summary>
		/// Displacement from the Z = 0 axis.
		/// </summary>
		public readonly int dz;

		public static readonly Direction Stay = new Direction(0, 0);
		public static readonly Direction PositiveY = new Direction(0, 1);
		public static readonly Direction PositiveX = new Direction(1, 0);
		public static readonly Direction ConstantZNegativeY = new Direction(1, -1);
		public static readonly Direction NegativeY = new Direction(0, -1);
		public static readonly Direction NegativeX = new Direction(-1, 0);
		public static readonly Direction ConstantZPositiveY = new Direction(-1, 1);

		public static readonly IEnumerable<Direction> NonStayDirections =
			new List<Direction>
				{
					PositiveY,
					PositiveX,
					ConstantZNegativeY,
					NegativeY,
					NegativeX,
					ConstantZPositiveY
				};

		public Direction(int dx, int dy)
		{
			this.dx = dx;
			this.dy = dy;
			this.dz = -(dx + dy);
		}

		public override bool Equals(object other)
		{
			var asDirection = other as Direction?;
			if (asDirection == null)
				return false;
			return asDirection == this;
		}

		public override int GetHashCode()
		{
			return Tuple.Create(this.dx, this.dy, this.dz).GetHashCode();
		}

		public static bool operator ==(
			Direction dir1, Direction dir2)
		{
			return dir1.dx == dir2.dx &&
				dir1.dy == dir2.dy;
		}

		public static bool operator !=(
			Direction dir1, Direction dir2)
		{
			return !(dir1 == dir2);
		}

		public static BoardPosition operator +(
			BoardPosition position, Direction direction)
		{
			return new BoardPosition(
				position.X + direction.dx,
				position.Y + direction.dy);
		}

		public static Direction operator +(
			Direction directionA,
			Direction directionB)
		{
			return new Direction(
				directionA.dx + directionB.dx,
				directionA.dy + directionB.dy);
		}

		public static Direction operator *(
			Direction direction, int n)
		{
			return new Direction(
				n * direction.dx,
				n * direction.dy);
		}

		public static Direction operator *(
			int n, Direction direction)
		{
			return direction * n;
		}
	}

	public static class DirectionCellExtensions
	{
		public static Direction? StraightLineDirectionTowards(
			this BoardPosition self,
			BoardPosition other)
		{
			if (self == other)
				return Direction.Stay;
			if (self.X == other.X)
			{
				if (other.Y < self.Y)
					return Direction.NegativeY;
				else
					return Direction.PositiveY;
			}
			else if (self.Y == other.Y)
			{
				if (other.X < self.X)
					return Direction.NegativeX;
				else
					return Direction.PositiveX;
			}
			else if (self.Z == other.Z)
			{
				if (other.Y < self.Z)
					return Direction.ConstantZNegativeY;
				else
					return Direction.ConstantZPositiveY;
			}
			return null;
		}
	}
}