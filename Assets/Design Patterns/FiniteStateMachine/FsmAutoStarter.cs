using UnityEngine;

namespace HexCasters.DesignPatterns.FSM
{
	/// <summary>
	/// Automatically starts an FSM on Start().
	/// </summary>
	public class FsmAutoStarter : MonoBehaviour
	{
		[Tooltip("The finite state machine to start")]
		public FiniteStateMachine fsm;
		[Tooltip("The state with which to start the machin")]
		public FsmState initialState;

		void Start()
		{
			fsm.StartMachine(initialState);
		}
	}
}
