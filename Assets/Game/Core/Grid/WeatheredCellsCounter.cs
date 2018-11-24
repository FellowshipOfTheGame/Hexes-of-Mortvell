using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexesOfMortvell.Core.Grid
{
	public class WeatheredCellsCounter : MonoBehaviour
	{
		[Header("Values")]
		public List<WeatheredCellCount> cellsCount;

		void Start()
		{
			CountWeatheredCells();
		}

		void Update()
		{
			CountWeatheredCells();
		}

		/// <summary>
		/// Counts cells containing a weather on the board.
		/// </summary>
		public void CountWeatheredCells()
		{
			var board = GameObject.FindObjectOfType<Board>();
			string weatherName;
			bool alreadyExists;
			for (int x = board.MinX; x <= board.MaxX; x++)
				for (int y = board.MinY; y <= board.MaxY; y++)
				{
					var pos = new BoardPosition(x, y);
					weatherName = board.GetCell(pos).Weather?.uniqueName;
					if (weatherName != null)
					{
						alreadyExists = false;
						foreach (var w in this.cellsCount)
							if (w.weatherType.Equals(weatherName))
							{
								w.count++;
								alreadyExists = true;
							}
						if (!alreadyExists)
							this.cellsCount.Add(
								new WeatheredCellCount(weatherName, 1));
					}
				}
		}
	}
}
