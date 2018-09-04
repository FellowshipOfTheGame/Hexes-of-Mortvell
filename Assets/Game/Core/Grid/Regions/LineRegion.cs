using System.Collections.Generic;

namespace HexCasters.Core.Grid.Regions
{
	public static class BoardLineRegionExtension
	{
		public static IEnumerable<BoardCell> StraightLineTowards(
			this BoardCell origin,
			BoardCell dest,
			int? maxLength=null,
			bool includeOrigin=false,
			bool stopAtOccupiedCell=false)
		{
			var nullableDir = origin.Position
				.StraightLineDirectionTowards(dest.Position);
			if (!nullableDir.HasValue)
			{
				var list = new List<BoardCell>();
				if (includeOrigin)
					list.Add(origin);
				return list;
			}

			var direction = nullableDir.Value;
			var distToCell = origin.Position
				.ManhattanDistanceTo(dest.Position);
			if (!maxLength.HasValue || maxLength.Value > distToCell)
				maxLength = distToCell;
			return origin.StraightLineTowards(
				direction,
				maxLength: maxLength,
				includeOrigin: includeOrigin,
				stopAtOccupiedCell: stopAtOccupiedCell);
		}

		public static IEnumerable<BoardCell> StraightLineTowards(
			this BoardCell origin,
			Direction direction,
			int? maxLength=null,
			bool includeOrigin=false,
			bool stopAtOccupiedCell=false)
		{
			if (includeOrigin)
				yield return origin;

			var iterCell = origin;
			int distanceFromOrigin = 0;

			do
			{
				iterCell = iterCell.FindAdjacentCell(direction);

				// end of board
				if (iterCell == null)
					break;
				if (stopAtOccupiedCell && !iterCell.Empty)
					break;
				yield return iterCell;
				distanceFromOrigin++;

				// skip distance check if no limit
				if (!maxLength.HasValue)
					continue;
			}
			while (distanceFromOrigin < maxLength.Value);
		}
	}
}