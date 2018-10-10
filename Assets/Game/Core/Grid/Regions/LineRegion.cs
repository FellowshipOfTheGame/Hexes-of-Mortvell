using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexesOfMortvell.Core.Grid.Regions
{
	public static class BoardLineRegionExtension
	{
		public enum OccupiedCellBehaviour
		{
			StopBefore,
			StopButInclude,
			Ignore
		}

		public static IEnumerable<BoardCell> StraightLineTowards(
			this BoardCell origin,
			BoardCell dest,
			int? maxLength=null,
			bool includeOrigin=false,
			OccupiedCellBehaviour occupiedCellBehaviour=OccupiedCellBehaviour.Ignore)
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
				occupiedCellBehaviour: occupiedCellBehaviour);
		}

		public static IEnumerable<BoardCell> StraightLineTowards(
			this BoardCell origin,
			Direction direction,
			int? maxLength=null,
			bool includeOrigin=false,
			OccupiedCellBehaviour occupiedCellBehaviour=OccupiedCellBehaviour.Ignore)
		{
			bool stopAtOccupiedCell =
				occupiedCellBehaviour != OccupiedCellBehaviour.Ignore;
			bool includeFirstOccupiedCell =
				occupiedCellBehaviour != OccupiedCellBehaviour.StopBefore;
			bool ignoreCellContent =
				occupiedCellBehaviour == OccupiedCellBehaviour.Ignore;
			IEnumerable<BoardCell> line = new BoardCell[] {};
			if (includeOrigin)
				line = line.Concat(new[] { origin });
			line = line.Concat(InternalStraightLineTowards(origin, direction));
			line = line.ToList();
			if (maxLength.HasValue)
				line = line.Take(maxLength.Value);
			if (stopAtOccupiedCell)
				line = line.TakeWhile(cell => cell.Empty);
			line = line.ToList();

			bool stoppedEarly = !maxLength.HasValue
				|| line.Count() < maxLength.Value;
			var farthestCell = line.LastOrDefault() ?? origin;
			if (!ignoreCellContent && includeFirstOccupiedCell && stoppedEarly)
			{
				var neighbor = farthestCell.FindAdjacentCell(direction);
				if (neighbor != null)
					line = line.Concat(new[] { neighbor });
			}
			return line;
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