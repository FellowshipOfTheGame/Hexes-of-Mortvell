using System;
using UnityEngine;
using HexCasters.DesignPatterns.Observer;
using HexCasters.Core.Units.Teams;

[RequireComponent(typeof(Team))]
public class TeamColor : MonoBehaviour
{
	[SerializeField]
	private Color _color;
	private ObservableValue<Color> observableColor;

	public Color Color
	{
		get { return GetColor(); }
		set { SetColor(value); }
	}

	public IObservable<Color> AsObservable
	{
		get { return observableColor; }
	}

	void Awake()
	{
		observableColor = new ObservableValue<Color>();
	}

	void Start()
	{
		SetColor(_color);
	}

	public Color GetColor()
	{
		return this._color;
	}

	public void SetColor(Color color)
	{
		this._color = color;
		observableColor.Value = color;
	}
}