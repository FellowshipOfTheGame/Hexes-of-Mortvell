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
		private Board board;

		void Awake()
		{
			board = GameObject.FindObjectOfType<Board>().GetComponent<Board>();
			cellCount = (board.NumRows)*(board.NumCols);
			cellsCount = new List<WeatheredCellCount>();
		}
		
		void Update()
		{
		}

		/// <summary>
		/// Counts cells containing a weather on the board.
		/// </summary>
		public void CountWeatheredCells()
		{
			string weatherName;
			bool alreadyExists;
			foreach(var w in this.cellsCount){
				w.count = 0;
			}
			for (int x = board.MinX; x <= board.MaxX; x++)
				for (int y = board.MinY; y <= board.MaxY; y++)
				{
					var pos = new BoardPosition(x, y);
					weatherName = board.GetCell(pos).Weather?.uniqueName;
					if (weatherName != null)
					{
						alreadyExists = false;
						for(int i=0; i <this.cellsCount.Count; i++)
							if (cellsCount[i].weatherType.Equals(weatherName))
							{
								cellsCount[i].count++;
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
			bool thisWeather = false;
			float brute = 0;
			var name = GetComponent<BoardWeather>().uniqueName;
			CountWeatheredCells();
			foreach (var w in this.cellsCount){
				if(brute < w.count){
					brute = w.count;
					if(w.weatherType.Equals(name)){
						Debug.Log("entrou");
						thisWeather = true;
					}else{ 
						thisWeather = false;
					}
				}
			}
			if(thisWeather)
				return (brute)/cellCount;
			else
				return 0;
		}
	}
}
