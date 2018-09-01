using UnityEngine;

namespace HexCasters.Core.Actions
{
	[DisallowMultipleComponent]
	public abstract class Action : MonoBehaviour
	{
		public abstract void Perform();
	}
}