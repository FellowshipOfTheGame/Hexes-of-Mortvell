using System;
using UnityEngine;
using HexCasters.DesignPatterns.Obserable;

namespace HexCasters.DesignPatterns.FSM
{
	public class FiniteStateMachine : MonoBehaviour
	{
		[SerializeField]
		private FsmState _state;
		public FsmState State
		{
			get { return _state; }
			private set { this._state = value; }
		}

		public bool Started
		{
			get { return this.State != null; }
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
			this.State.enterEvent?.Invoke(this);
		}

		private void ExitState()
		{
			this.State?.exitEvent?.Invoke(this);
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