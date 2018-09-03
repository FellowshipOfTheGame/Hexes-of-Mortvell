using UnityEngine;
using HexCasters.DesignPatterns.Fsm;


namespace HexCasters.Testing.FsmTest
{
	public class FsmTestStateGravity : FsmState
	{
		public Rigidbody sphereRigidbody;
		public float jumpSpeed = 4;
		public FsmTestInputReader input;

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Alpha1))
				fsm.Transition<FsmTestStateMove>();
		}

		public override void Enter()
		{
			sphereRigidbody.useGravity = true;
			ObserveJumpButton();
		}

		public override void Exit()
		{
			sphereRigidbody.useGravity = false;
			IgnoreJumpButton();
		}

		void ObserveJumpButton()
		{
			input.jumpButtonPressed.valueChangedEvent += SpaceBarStateChange;
		}

		void IgnoreJumpButton()
		{
			input.jumpButtonPressed.valueChangedEvent -= SpaceBarStateChange;
		}

		void SpaceBarStateChange(bool value)
		{
			if (value)
				Jump();
		}

		void Jump()
		{
			sphereRigidbody.AddForce(Vector2.up * jumpSpeed, ForceMode.Impulse);
		}
	}
}
