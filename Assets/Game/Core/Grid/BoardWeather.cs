using System.Collections.Generic;
using UnityEngine;

namespace HexesOfMortvell.Core.Grid
{
	public class BoardWeather : MonoBehaviour
	{
		public string uniqueName;
		public List<BoardWeather> replaceableWeathers;
		public List<BoardWeather> neutralizableWeathers;

		public override bool Equals(object other)
		{
			if (base.Equals(other))
				return true;
			var asWeather = other as BoardWeather;
			return this.uniqueName.Equals(asWeather?.uniqueName);
		}

		public override int GetHashCode()
		{
			return this.uniqueName.GetHashCode();
		}

		public void ApplyTo(BoardCell cell)
		{
			if (cell.Weather == null ||
				this.replaceableWeathers.Contains(cell.Weather))
			{
				cell.InstantiateWeather(this.gameObject);
			}
			else if (this.neutralizableWeathers.Contains(cell.Weather))
			{
				cell.InstantiateWeather(null);
			}
		}
	}
}
