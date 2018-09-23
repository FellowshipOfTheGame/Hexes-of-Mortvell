using System;
using UnityEngine;
using HexesOfMortvell.DesignPatterns.Observer;
using HexesOfMortvell.Core.Units.Teams;

namespace HexesOfMortvell.Hud.Teams
{
	[RequireComponent(typeof(Team))]
	public class TeamColor : MonoBehaviour
	{
		public ColorReference color;
	}
}
