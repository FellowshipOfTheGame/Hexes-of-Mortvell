using UnityEngine;
using HexCasters.DesignPatterns.Observable;

public class FsmTestInputReader : MonoBehaviour
{
	public ObservableValue<Vector2> direction;
	public ObservableValue<bool> printButtonPressed;

	public KeyCode printButtonKeyCode;

	void Awake()
	{
		direction = new ObservableValue<Vector2>(Vector2.zero);
		printButtonPressed = new ObservableValue<bool>(false);
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
		var button = Input.GetKeyDown(printButtonKeyCode);
		if (printButtonPressed.Value != button)
			printButtonPressed.Value = button;
	}
}
