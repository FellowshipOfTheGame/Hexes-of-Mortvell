using System;
using UnityEngine;

namespace HexCasters.DesignPatterns.FSM
{
	/// <summary>
	/// A state for a finite state machine.
	/// </summary>
	public class FsmState : MonoBehaviour
	{
		/// <summary>
		/// A reference to this state's FSM.
		/// </summary>
		protected FiniteStateMachine fsm;

		// Protected to avoid overrides or overshadowing
		protected void Awake()
		{
			fsm = GetComponent<FiniteStateMachine>();
			ErrorIfNoFsmAttached();
			enabled = false;
		}

		// Protected to avoid overrides or overshadowing
		protected void Reset()
		{
			enabled = false;
		}

		/// <summary>
		/// Called when the FSM transitions into this state.
		/// </summary>
		public virtual void Enter() {}

		/// <summary>
		/// Called when the FSM transitions out of this state.
		/// </summary>
		public virtual void Exit() {}

		void ErrorIfNoFsmAttached()
		{
			if (fsm == null)
				throw new InvalidOperationException(
					"Added an FSM script to an object without an FSM");
		}
	}
}