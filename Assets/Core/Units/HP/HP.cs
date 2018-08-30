using UnityEngine;
using System;
using HexCasters.DesignPatterns.Observer;

namespace HexCasters.Core.Units
{
	public class HP : MonoBehaviour
	{
		public int max;
		[SerializeField]
		private int uncommitedValue;
		public ObservableValue<int> commitedValue;

		public int Current
		{
			get { return Get(); }
			set { Set(value); }
		}

		public IObservable<int> AsObservable
		{
			get { return this.commitedValue; }
		}

		void Awake()
		{
			this.commitedValue = new ObservableValue<int>(this.max);
			this.uncommitedValue = this.Current;
		}

		void OnDestroy()
		{
			this.commitedValue.MarkComplete();
		}

		public int Get()
		{
			return this.commitedValue.Value;
		}

		public int GetUncommited()
		{
			return this.uncommitedValue;
		}

		public void Set(int newValue, bool clamp=false, bool commit=false)
		{
			if (clamp)
				ClampValueToRange(ref newValue);
			this.uncommitedValue = newValue;
			if (commit)
				Commit();
		}

		public void Increase(int amount, bool clamp=false, bool commit=false)
		{
			ErrorIfNegative(amount, nameof(amount));
			this.Set(
				this.uncommitedValue + amount,
				clamp: clamp,
				commit: commit);
		}

		public void Decrease(int amount, bool clamp=false, bool commit=false)
		{
			ErrorIfNegative(amount, nameof(amount));
			this.Set(this.uncommitedValue - amount, clamp, commit);
		}

		public void Clamp()
		{
			int currentValue = this.uncommitedValue;
			ClampValueToRange(ref currentValue);
			this.uncommitedValue = currentValue;
		}

		public void Commit()
		{
			Clamp();
			this.commitedValue.Value = this.uncommitedValue;
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