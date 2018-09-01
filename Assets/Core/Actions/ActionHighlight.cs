using UnityEngine;
using System.Collections.Generic;
using HexCasters.Core.Grid;

namespace HexCasters.Core.Actions
{
	[DisallowMultipleComponent]
	public abstract class ActionHigihlight : MonoBehaviour
	{
		public abstract Color GetColor(BoardCell cell);
	}
}
