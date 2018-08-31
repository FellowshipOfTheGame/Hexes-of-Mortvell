using UnityEngine;
using HexCasters.Core.Grid;
using HexCasters.Core.Actions;

namespace HexCasters.Testing.ActorTest
{
	public class ActorTestImplementation : Actor
	{
		[Action]
		void TestAction1(Actor actor)
		{
			Debug.Log("Action 1!");
		}


		[Action]
		void TestAction2(Actor actor) {}

		[Action]
		void Attack(Actor actor) {}


		void Move(BoardCell dest)
		{
			Debug.Log($"Moving to {dest}");
		}
	}
}