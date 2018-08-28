using UnityEngine;
using UnityEngine.Events;

namespace HexCasters.DesignPatterns.FSM
{
	[CreateAssetMenu(
		fileName="New FSM State",
		menuName="HexCasters/FSM/State")]
	public class FsmState : ScriptableObject
	{
		[System.Serializable]
		public class FsmEvent : UnityEvent<FiniteStateMachine> {}

		public FsmEvent enterEvent;
		public FsmEvent exitEvent;
	}
}