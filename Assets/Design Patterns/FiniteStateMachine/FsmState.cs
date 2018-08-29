using UnityEngine;
using UnityEngine.Events;

namespace HexCasters.DesignPatterns.FSM
{
	public class FsmState : MonoBehaviour
	{
		protected FiniteStateMachine fsm;

		void Awake()
		{
			fsm = GetComponent<FiniteStateMachine>();
		}

		public virtual void Enter() {}
		public virtual void Exit() {}
	}
}