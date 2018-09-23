using UnityEngine;

namespace HexesOfMortvell.Hud
{
	[CreateAssetMenu(fileName="New Color", menuName="HexesOfMortvell/Color")]
	public class ColorReference : ScriptableObject
	{
		public Color value;
	}
}