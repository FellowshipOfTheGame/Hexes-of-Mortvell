using UnityEngine;
using HexCasters.DesignPatterns.Observable;

public class FsmTestInputReader : MonoBehaviour
{
	public ObservableValue<Vector2> direction;
	public ObservableValue<bool> jumpButtonPressed;

	public KeyCode jumpButtonKeyCode;

	void Awake()
	{
		direction = new ObservableValue<Vector2>(Vector2.zero);
		jumpButtonPressed = new ObservableValue<bool>(false);
	}

	void Update()
	{
		UpdateDirection();
		UpdatePrintButton();
	}

	void UpdateDirection()
	{
		var horizontal = Input.GetAxisRaw("Horizontal");
		var vertical = Input.GetAxisRaw("Vertical");
		direction.Value = new Vector2(horizontal, vertical).normalized;
	}

	void UpdatePrintButton()
	{
		var button = Input.GetKeyDown(jumpButtonKeyCode);
		if (this.jumpButtonPressed.Value != button)
			this.jumpButtonPressed.Value = button;
	}
}
