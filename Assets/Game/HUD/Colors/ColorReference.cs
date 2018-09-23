using System;
using UnityEngine;
using HexesOfMortvell.DesignPatterns.Observer;

namespace HexesOfMortvell.Hud
{
	[CreateAssetMenu(fileName="New Color", menuName="HexesOfMortvell/Color")]
	public class ColorReference : ScriptableObject
	{
		// TODO accessibility

		[SerializeField]
		private Color _color = Color.white;
		public Color Value
		{
			get { return GetValue(); }
			set { SetValue(value); }
		}
		private ObservableValue<Color> observable
			= new ObservableValue<Color>();

		public IObservable<Color> AsObservable
		{
			get { return this.observable; }
		}

		void OnEnable()
		{
			SetValue(this._color);
		}

		void OnValidate()
		{
			SetValue(this._color);
		}

		public Color GetValue()
		{
			return this._color;
		}

		public void SetValue(Color value)
		{
			this._color = value;
			this.observable.Value = value;
		}
	}
}