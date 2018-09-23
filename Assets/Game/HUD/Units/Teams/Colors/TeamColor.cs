using System;
using UnityEngine;
using HexesOfMortvell.DesignPatterns.Observer;
using HexesOfMortvell.Core.Units.Teams;

namespace HexesOfMortvell.Hud.Teams
{
	[RequireComponent(typeof(Team))]
	public class TeamColor : MonoBehaviour
	{
		[SerializeField]
		private ColorReference _color;
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
			SetColor(this._color);
		}

		public Color GetColor()
		{
			return this._color.value;
		}

		public void SetColor(ColorReference colorReference)
		{
			this._color = colorReference;
			this.observableColor.Value = this._color.value;
		}

		public void SetColor(Color color)
		{
			this._color = null;
			this.observableColor.Value = color;
		}
	}
}
