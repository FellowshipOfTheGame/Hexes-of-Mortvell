using UnityEngine;
using HexCasters.DesignPatterns.FSM;

public class FsmTestStateMove : FsmState
{
	public FsmTestInputReader input;
	public Transform movedObjectTransform;
	public float moveSpeed = 0.2f;

	[SerializeField]
	private Vector2 observedDirection;

	public override void Enter()
	{
		enabled = true;
		ObserveDirection();
	}

	public override void Exit()
	{
		IgnoreDirection();
		enabled = false;
	}

	void UpdateObservedDirection(Vector2 newDirection)
	{
		observedDirection = newDirection;
	}

	void Update()
	{
		movedObjectTransform.Translate(moveSpeed * observedDirection);
		if (Input.GetKeyDown(KeyCode.Alpha2))
			fsm.Transition<FsmTestDummyState>();
	}


	void ObserveDirection()
	{
		input.direction.valueChangedEvent += UpdateObservedDirection;
	}

	void IgnoreDirection()
	{
		input.direction.valueChangedEvent -= UpdateObservedDirection;
	}
}
