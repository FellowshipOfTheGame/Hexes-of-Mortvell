using System.Collections.Generic;
using System.Linq;

namespace HexesOfMortvell.Core.Grid.Regions
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
			var line = InternalStraightLineTowards(origin, direction);
			if (maxLength.HasValue)
				line = line.Take(maxLength.Value);
			if (stopAtOccupiedCell)
				line = line.TakeWhile(cell => cell.Empty);
			foreach (var cell in line)
				yield return cell;
		}

		private static IEnumerable<BoardCell> InternalStraightLineTowards(
			BoardCell origin, Direction direction)
		{
			var iterCell = origin.FindAdjacentCell(direction);

			while (iterCell != null)
			{
				yield return iterCell;
				iterCell = iterCell.FindAdjacentCell(direction);
			}
		}
	}
}