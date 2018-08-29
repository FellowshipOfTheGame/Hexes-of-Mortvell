using UnityEngine;
using HexCasters.DesignPatterns.FSM;

public class FsmTestStateGravity : FsmState {
	public Rigidbody sphereRigidbody;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
			fsm.Transition<FsmTestStateMove>();
	}

	public override void Enter()
	{
		sphereRigidbody.useGravity = true;
	}

	public override void Exit()
	{
		sphereRigidbody.useGravity = false;
	}
}
