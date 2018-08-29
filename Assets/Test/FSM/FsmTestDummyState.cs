using UnityEngine;
using HexCasters.DesignPatterns.FSM;

public class FsmTestDummyState : FsmState {
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
			fsm.Transition<FsmTestStateMove>();
	}
}
