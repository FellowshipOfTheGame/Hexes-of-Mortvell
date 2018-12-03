using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexesOfMortvell.Core.Grid
{
	public class WeatheredCellsCounter : MonoBehaviour
	{
		[Header("Values")]
		public List<WeatheredCellCount> cellsCount;
		private int cellCount;

		void Start()
		{
			var board = GameObject.FindObjectOfType<Board>();
			CountWeatheredCells();
			cellCount = (board.MaxX - board.MinX)*(board.MaxY-board.MinY);
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
		
		/// <summary>
		/// Calculates which weather is more common, and returns the percentage of cells that have such condition
		/// </summary>
		public float GetMostCommon(){
			int type = -1;
			float brute = 0;
			foreach (var w in this.cellsCount){
				if(brute < w.count)
					brute = w.count;
					if(w.weatherType.Equals("Fire")) type = 0;
					else if(w.weatherType.Equals("Rain")) type = 1;
					else if(w.weatherType.Equals("Snow")) type = 2;
			}
			return brute/cellCount + type;
		}
	}
}
