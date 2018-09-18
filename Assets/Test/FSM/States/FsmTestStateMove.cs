using UnityEngine;
using HexesOfMortvell.DesignPatterns.Fsm;

namespace HexesOfMortvell.Testing.FsmTest
{
	public class FsmTestStateMove : FsmState
	{
		public FsmTestInputReader input;
		public Rigidbody movedObjectRigidbody;
		public float moveSpeed = 0.2f;

		[SerializeField]
		private Vector2 observedDirection;

		public override void Enter()
		{
			ObserveDirection();
		}

		public override void Exit()
		{
			IgnoreDirection();
		}

		void UpdateObservedDirection(Vector2 newDirection)
		{
			observedDirection = newDirection;
		}

		void Update()
		{
			movedObjectRigidbody.AddForce(moveSpeed * observedDirection);
			if (Input.GetKeyDown(KeyCode.Alpha2))
				fsm.Transition<FsmTestStateGravity>();
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
}