using System;

namespace HexesOfMortvell.DesignPatterns.Observer
{
	public class ValueObserver<T> : IObserver<T>
	{
		private readonly Action<T> nextEventHandler;
		private readonly Action completeEventHandler;
		private readonly Action<Exception> errorEventHandler;

		/// <summary>
		/// Creates an observer with the given Actions as event handlers.
		/// </summary>
		/// <param name="nextEventHandler">
		/// The event handler for when the observed object changes values.
		/// </param>
		/// <param name="completeEventHandler">
		/// The event handler for when the observed object is marked complete.
		/// </param>
		/// <param name="errorEventHandler">
		/// The event handler for when the observed object notifies an invalid value.
		/// </param>
		public ValueObserver(
			Action<T> nextEventHandler = null,
			Action completeEventHandler = null,
			Action<Exception> errorEventHandler = null)
		{
			this.nextEventHandler = nextEventHandler;
			this.completeEventHandler = completeEventHandler;
			this.errorEventHandler = errorEventHandler;
		}

		/// <summary>
		/// Called when the observed object changes values.
		/// </summary>
		/// <param name="value">The new value for the observed object.</param>
		public void OnNext(T value) =>
			this.nextEventHandler?.Invoke(value);

		/// <summary>
		/// Called when the observed object is marked complete.
		/// </summary>
		public void OnCompleted() =>
			this.completeEventHandler?.Invoke();

		/// <summary>
		/// Called when the observed object notifies an invalid value.
		/// </summary>
		/// <param name="error">The error raised by the observed object.</param>
		public void OnError(Exception error) =>
			this.errorEventHandler?.Invoke(error);
	}
}