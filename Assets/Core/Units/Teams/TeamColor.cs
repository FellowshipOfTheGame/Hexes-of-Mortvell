using System;
using UnityEngine;
using HexCasters.DesignPatterns.Observer;

namespace HexCasters.Core.Units.Teams
{
	[RequireComponent(typeof(Team))]
	public class TeamColor : MonoBehaviour
	{
		public Color color;
	}
}