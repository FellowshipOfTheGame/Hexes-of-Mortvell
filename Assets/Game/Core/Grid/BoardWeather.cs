using UnityEngine;

namespace HexesOfMortvell.Core.Grid
{
	public class BoardWeather : MonoBehaviour
	{
		public string uniqueName;

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
	}
}
