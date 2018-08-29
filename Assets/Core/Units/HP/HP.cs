using UnityEngine;
using System;
using HexCasters.DesignPatterns.Observer;

namespace HexCasters.Core.Units
{
	public class HP : MonoBehaviour
	{
		public int max;
		public ObservableValue<int> _current;

		public int Current
		{
			get { return Get(); }
			set { Set(value); }
		}

		public IObservable<int> CurrentAsObservable
		{
			get { return this._current; }
		}

		void Awake()
		{
			this._current = new ObservableValue<int>(this.max);
		}

		public int Get()
		{
			return this._current.Value;
		}

		public void Set(int newValue)
		{
			this._current.Value = newValue;
		}

		public void ClampedSet(int newValue)
		{
			ClampValueToRange(ref newValue);
			Set(newValue);
		}

		public void Increase(int amount)
		{
			ErrorIfNegative(amount, nameof(amount));
			this.Current += amount;
		}

		public void Decrease(int amount)
		{
			ErrorIfNegative(amount, nameof(amount));
			this.Current -= amount;
		}

		public void Clamp()
		{
			int currentValue = this.Current;
			ClampValueToRange(ref currentValue);
			this.Current = currentValue;
		}

		private void ClampValueToRange(ref int value)
		{
			if (value < 0)
				value = 0;
			else if (value > this.max)
				value = max;
		}

		private void ErrorIfNegative(int value, string paramName)
		{
			if (value < 0)
				throw new ArgumentException(
					"Value cannot be negative",
					paramName);
		}
	}
}