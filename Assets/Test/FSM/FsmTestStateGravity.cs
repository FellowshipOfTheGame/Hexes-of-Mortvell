using UnityEngine;
using HexCasters.DesignPatterns.FSM;

public class FsmTestStateGravity : FsmState {
	public Rigidbody sphereRigidBody;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
			fsm.Transition<FsmTestStateMove>();
	}

	public override void Enter()
	{
		sphereRigidBody.useGravity = true;
	}

	public override void Exit()
	{
		sphereRigidBody.useGravity = false;
	}
}
