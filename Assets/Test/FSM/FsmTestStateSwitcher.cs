using UnityEngine;
using HexCasters.DesignPatterns.FSM;

public class FsmTestStateSwitcher : MonoBehaviour {
	public FiniteStateMachine fsm;

	void Start()
	{
		fsm.StartMachine<FsmTestDummyState>();
	}
}
