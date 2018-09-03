using System.Collections.Generic;

namespace HexCasters.Core.Grid
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

		public static readonly Direction PositiveY = new Direction(0, 1);
		public static readonly Direction PositiveX = new Direction(1, 0);
		public static readonly Direction ConstantZDown = new Direction(1, -1);
		public static readonly Direction NegativeY = new Direction(0, -1);
		public static readonly Direction NegativeX = new Direction(-1, 0);
		public static readonly Direction ConstantZUp = new Direction(-1, 1);

		public static readonly IEnumerable<Direction> List = new List<Direction>
		{
			PositiveY,
			PositiveX,
			ConstantZDown,
			NegativeY,
			NegativeX,
			ConstantZUp
		};

		private Direction(int dx, int dy)
		{
			this.dx = dx;
			this.dy = dy;
			this.dz = -(dx + dy);
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
}