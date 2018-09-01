using System;
using UnityEngine;

namespace HexCasters.DesignPatterns.Fsm
{
	/// <summary>
	/// A state for a Finite State Machine.
	/// </summary>
	/// <typeparam name="S">(Optional) The shared storage type used for this state.</typeparam>
	public abstract class FsmState<S> : MonoBehaviour
		where S : FsmStateSharedMemory
	{
		/// <summary>
		/// A reference to this state's FSM.
		/// </summary>
		protected FiniteStateMachine fsm;

		/// <summary>
		/// An independent component used to share information between states.
		/// </summary>
		protected S sharedMemory;

		// Protected to avoid overrides or overshadowing
		protected void Awake()
		{
			this.fsm = GetComponent<FiniteStateMachine>();
			this.sharedMemory = GetComponent<S>();
			ErrorIfNoFsmAttached();
			this.enabled = false;
		}

		// Protected to avoid overrides or overshadowing
		protected void Reset()
		{
			this.enabled = false;
		}

		/// <summary>
		/// Called when the FSM transitions into this state.
		/// </summary>
		public abstract void Enter();

		/// <summary>
		/// Called when the FSM transitions out of this state.
		/// </summary>
		public abstract void Exit();

		void ErrorIfNoFsmAttached()
		{
			if (this.fsm == null)
				throw new InvalidOperationException(
					"Added an FSM script to an object without an FSM");
		}
	}

	/// <summary>
	/// A state for a Finite State Machine.
	/// </summary>
	public abstract class FsmState : FsmState<FsmStateSharedMemory> {}
}