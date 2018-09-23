using System;
using UnityEngine;
using HexesOfMortvell.DesignPatterns.Observer;

namespace HexesOfMortvell.Hud
{
	[CreateAssetMenu(fileName="New Color", menuName="HexesOfMortvell/Color")]
	public class ColorReference : ScriptableObject
	{
		[SerializeField]
		private Color defaultValue = Color.white;
		[SerializeField]
		private Color alternateValue = Color.white;

		[SerializeField]
		private bool useAlternate = false;

		public Color Value
		{
			get { return GetValue(); }
		}

		private ObservableValue<Color> observable
			= new ObservableValue<Color>();

		public IObservable<Color> AsObservable
		{
			get { return this.observable; }
		}

		void OnEnable()
		{
			UpdateValues();
		}

		void OnValidate()
		{
			UpdateValues();
		}

		public void SetMode(bool useAlternate)
		{
			this.useAlternate = useAlternate;
			UpdateValues();
		}

		public Color GetValue()
		{
			if (this.useAlternate)
				return this.alternateValue;
			else
				return this.defaultValue;
		}

		void UpdateValues()
		{
			this.observable.Value = GetValue();
		}
	}
}