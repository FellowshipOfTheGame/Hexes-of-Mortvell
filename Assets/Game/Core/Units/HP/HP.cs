using UnityEngine;
using System;
using HexesOfMortvell.DesignPatterns.Observer;

namespace HexesOfMortvell.Core.Units
{
	/// <summary>
	/// HP system for units and destructable objects.
	/// </summary>
	public class HP : MonoBehaviour
	{
		/// <summary>
		/// Maximum value for HP.
		/// </summary>
		public int max;

		[SerializeField]
		/// <summary>
		/// Temporary value which allows for threshold trespassing for
		/// calculations and corrections.
		/// </summary>
		private int uncommitedValue;

		/// <summary>
		/// Observable actual HP value.
		/// </summary>
		private ObservableValue<int> commitedValue;

		/// <summary>
		/// Retrieves the current commited HP.
		/// </summary>
		public int Current
		{
			get { return Get(); }
		}

		/// <summary>
		/// An Observable that will notify HP value commits.
		/// </summary>
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

		/// <summary>
		/// Retrieves the current commited value.
		/// </summary>
		public int Get()
		{
			return this.commitedValue.Value;
		}

		/// <summary>
		/// Retrieves the uncommited temporary value.
		/// </summary>
		public int GetUncommited()
		{
			return this.uncommitedValue;
		}

		/// <summary>
		/// Sets the uncommited value.
		/// </summary>
		/// <param name="newValue">The new uncommited value.</param>
		/// <param name="clamp">Whether the new value should be clamped to [0, max].</param>
		public void Set(int newValue, bool clamp=false)
		{
			if (clamp)
				newValue = ClampValueToRange(newValue);
			this.uncommitedValue = newValue;
		}

		/// <summary>
		/// Increases the uncommited value by a given amount.
		/// </summary>
		/// <param name="amount">How much to increase the value by.</param>
		/// <param name="clamp">Whether the new value should be clamped to [0, max].</param>
		public void Increase(int amount, bool clamp=false)
		{
			ErrorIfNegative(amount, nameof(amount));
			this.Set(
				this.uncommitedValue + amount,
				clamp: clamp);
		}

		/// <summary>
		/// Decreases the uncommited value by a given amount.
		/// </summary>
		/// <param name="amount">How much to increase the value by.</param>
		/// <param name="clamp">Whether the new value should be clamped to [0, max].</param>
		public void Decrease(int amount, bool clamp=false)
		{
			ErrorIfNegative(amount, nameof(amount));
			this.Set(
				this.uncommitedValue - amount,
				clamp: clamp);
		}


		/// <summary>
		/// Limits the current uncommited value to the range [0, max].
		/// </summary>
		public void Clamp()
		{
			this.uncommitedValue = ClampValueToRange(this.uncommitedValue);
		}

		/// <summary>
		/// Commits the current uncommited value.
		/// </summary>
		/// <remarks>
		/// <para>
		/// This should only be done after all relevant calculations in an
		/// atomic operation are completed.
		/// </para>
		/// <para>
		/// Calling this method will notify all objects observing the
		/// HP.AsObservable object.
		/// </para>
		/// </remarks>
		public void Commit()
		{
			Clamp();
			this.commitedValue.Value = this.uncommitedValue;
		}

		int ClampValueToRange(int value)
		{
			if (value < 0)
				value = 0;
			else if (value > this.max)
				value = max;
			return value;
		}

		void ErrorIfNegative(int value, string paramName)
		{
			if (value < 0)
				throw new ArgumentException(
					"Value cannot be negative",
					paramName);
		}
	}
}