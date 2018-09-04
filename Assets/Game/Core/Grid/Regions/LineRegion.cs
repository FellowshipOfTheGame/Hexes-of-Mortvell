using System.Collections.Generic;

namespace HexCasters.Core.Grid.Regions
{
	public static class BoardLineRegionExtension
	{
		public static IEnumerable<BoardCell> LineTo(
			this BoardCell origin,
			BoardCell dest,
			int? maxLength=null,
			bool includeOrigin=false)
		{
			var nullableDir = origin.Position
				.StraightLineDirectionTowards(dest.Position);
			var line = new List<BoardCell>();

			if (includeOrigin)
				line.Add(origin);
			if (!nullableDir.HasValue)
				return line;

			var direction = nullableDir.Value;
			var iterCell = origin;
			int distanceFromOrigin = 0;

			do
			{
				iterCell = iterCell.FindAdjacentCell(direction);
				if (iterCell == null)
					break;
				line.Add(iterCell);
				distanceFromOrigin++;
				if (maxLength.HasValue && distanceFromOrigin >= maxLength.Value)
					break;
			}
			while (iterCell != dest);

			return line;
		}
	}
}