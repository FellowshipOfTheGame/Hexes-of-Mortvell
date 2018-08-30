using System.Collections.Generic;

namespace HexCasters.Core.Grid
{
	public struct Direction
	{
		public readonly int dx;
		public readonly int dy;
		public readonly int dz;

		public static readonly Direction UpRight = new Direction(0, 1);
		public static readonly Direction Right = new Direction(1, 0);
		public static readonly Direction DownRight = new Direction(1, -1);
		public static readonly Direction DownLeft = new Direction(0, -1);
		public static readonly Direction Left = new Direction(-1, 0);
		public static readonly Direction UpLeft = new Direction(-1, 1);

		public static readonly IEnumerable<Direction> List = new List<Direction>
		{
			UpRight,
			Right,
			DownRight,
			DownLeft,
			Left,
			UpLeft
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
				position.x + direction.dx,
				position.y + direction.dy);
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