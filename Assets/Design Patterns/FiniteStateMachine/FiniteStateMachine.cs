using System;
using System.Collections.Generic;
using UnityEngine;

namespace HexCasters.DesignPatterns.FSM
{
	public class FiniteStateMachine : MonoBehaviour
	{
		[SerializeField]
		private FsmState _state;
		private ISet<FsmState> knownStates;
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
			this.knownStates = new HashSet<FsmState>();
			foreach (var state in GetComponents<FsmState>())
				AcknowledgeState(state);
			ErrorIfUnknownState(initialState);
			EnterState(initialState);
		}

		void AcknowledgeState(FsmState state)
		{
			this.knownStates.Add(state);
			state.enabled = false;
		}

		public void StartMachine<T>() where T : FsmState
		{
			StartMachine(GetComponent<T>());
		}

		public void Transition(FsmState nextState)
		{
			ErrorIfNotInitialized();
			ErrorIfUnknownState(nextState);
			ExitState();
			EnterState(nextState);
		}

		public void Transition<T>() where T : FsmState
		{
			Transition(GetComponent<T>());
		}

		private void EnterState(FsmState nextState)
		{
			ErrorIfNullArgument(nextState, nameof(nextState));
			this.State = nextState;
			this.State.enabled = true;
			this.State.Enter();
		}

		private void ExitState()
		{
			if (this.State != null)
			{
				this.State.Exit();
				this.State.enabled = false;
			}
			this.State = null;
		}

		private void ErrorIfNotInitialized()
		{
			if (!this.Started)
				throw new InvalidOperationException("FSM was not initialized");
		}

		private void ErrorIfUnknownState(FsmState state)
		{
			if (!knownStates.Contains(state))
				throw new ArgumentException("Tried to switch to unkown state");
		}

		private void ErrorIfNullArgument(object value, string paramName)
		{
			if (value == null)
				throw new ArgumentNullException(paramName);
		}
	}
}