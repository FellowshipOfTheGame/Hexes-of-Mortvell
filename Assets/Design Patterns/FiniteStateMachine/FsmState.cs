using System;
using UnityEngine;

namespace HexCasters.DesignPatterns.FSM
{
	public class FsmState : MonoBehaviour
	{
		protected FiniteStateMachine fsm;

		protected void Awake()
		{
			fsm = GetComponent<FiniteStateMachine>();
			ErrorIfNoFsmAttached();
			enabled = false;
		}

		protected void Reset()
		{
			enabled = false;
		}

		public virtual void Enter() {}
		public virtual void Exit() {}

		void ErrorIfNoFsmAttached()
		{
			if (fsm == null)
				throw new InvalidOperationException(
					"Added an FSM script to an object without an FSM");
		}
	}
}