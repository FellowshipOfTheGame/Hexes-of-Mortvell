using System;
using UnityEngine;

namespace HexCasters.DesignPatterns.FSM
{
	public class FiniteStateMachine : MonoBehaviour
	{
		[SerializeField]
		private FsmState _state;
		public FsmState State
		{
			get
			{
				ErrorIfNotInitialized();
				return _state;
			}
			private set { this._state = value; }
		}

		public bool Started
		{
			get { return this._state != null; }
		}

		public void StartMachine(FsmState initialState)
		{
			EnterState(initialState);
		}

		public void Transition(FsmState nextState)
		{
			ErrorIfNotInitialized();
			ExitState();
			EnterState(nextState);
		}

		private void EnterState(FsmState nextState)
		{
			ErrorIfNullArgument(nextState, nameof(nextState));
			this.State = nextState;
			this.State.Enter();
		}

		private void ExitState()
		{
			this.State?.Exit();
			this.State = null;
		}

		private void ErrorIfNotInitialized()
		{
			if (!this.Started)
				throw new InvalidOperationException("FSM was not initialized");
		}

		private void ErrorIfNullArgument(object value, string paramName)
		{
			if (value == null)
				throw new ArgumentNullException(paramName);
		}
	}
}