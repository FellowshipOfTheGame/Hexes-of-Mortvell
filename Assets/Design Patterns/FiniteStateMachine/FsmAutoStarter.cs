using UnityEngine;

namespace HexCasters.DesignPatterns.FSM
{
	public class FsmAutoStarter : MonoBehaviour
	{
		public FiniteStateMachine fsm;
		public FsmState initialState;

		void Start()
		{
			fsm.StartMachine(initialState);
		}
	}
}
