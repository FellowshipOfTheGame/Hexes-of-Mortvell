using UnityEngine;
using System.Collections.Generic;
using HexCasters.Core.Grid;

namespace HexCasters.Core.Actions
{
	[DisallowMultipleComponent]
	public abstract class ActionHighlight : MonoBehaviour
	{
		public abstract Color GetColor(BoardCell cell);
	}
}
