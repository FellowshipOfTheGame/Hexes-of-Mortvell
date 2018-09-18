using System;

namespace HexesOfMortvell.DesignPatterns.Observer
{
	/// <summary>
	/// Keeps track of and notifies registered observers when its wrapped value
	/// changes.
	/// </summary>
	/// <typeparam name="T">The type of the wrapped object.</typeparam>
	public class ObservableValue<T> : IObservable<T>, IDisposable
	{
		/// <summary>
		/// Event called when the value held by the observable has changed.
		/// </summary>
		public event Action<T> valueChangedEvent;
		/// <summary>
		/// Event called when the observable has been marked complete.
		/// </summary>
		public event Action completedEvent;
		/// <summary>
		/// Event called when the observable was assigned an illegal value
		/// according to its validator.
		/// </summary>
		public event Action<Exception> errorEvent;

		private readonly Predicate<T> validator;

		private T _value;

		/// <summary>
		/// The current value held by the observable.
		/// </summary>
		/// <value>Runs validation with the Completed property and the
		/// observable's validator.</value>
		public T Value
		{
			get
			{
				this.ErrorIfCompleted();
				return _value;
			}
			set
			{
				this.ErrorIfCompleted();
				if (validator != null && !validator(value))
					this.NotifyInvalidValue(value);
				else
					this.UpdateValue(value);
			}
		}

		/// <summary>
		/// Whether the object is marked complete or not.
		/// </summary>
		/// <value>true if the observable has been marked complete;
		/// false otherwise.</value>
		public bool Completed { get; private set; }

		/// <summary>
		/// Creates an ObservableValue with an initial value and validator.
		/// </summary>
		/// <param name="initialValue">
		/// The initial value for the observable.
		/// </param>
		/// <param name="validator">
		/// true if the new value is valid, false otherwise.
		/// </param>
		public ObservableValue(
			T initialValue = default(T),
			Predicate<T> validator = null)
		{
			this.Completed = false;
			this.validator = validator;
			this.Value = initialValue;
		}


		/// <summary>
		/// Marks the observable as complete.
		/// Once complete, any attempt to modify its value will throw an
		/// ObjectDisposedException.
		/// </summary>
		public void MarkComplete()
		{
			this.ErrorIfCompleted();
			this.Completed = true;
			this.completedEvent?.Invoke();
		}


		/// <summary>
		/// Marks the observable as complete.
		/// Once complete, any attempt to modify its value will throw an
		/// ObjectDisposedException.
		/// </summary>
		public void Dispose() => this.MarkComplete();


		/// <summary>
		/// Invokes value changed event with the current value
		/// of the observable.
		/// Intended to be used for values which can mutate without a
		/// direct attribution, such as Lists or Dictionaries.
		/// </summary>
		public void NotifyValueChange()
		{
			this.valueChangedEvent?.Invoke(_value);
		}


		/// <summary>
		/// Registers a new observer to be notified.
		/// </summary>
		/// <param name="observer">
		/// The object to be notified when the observable's value changes.
		/// </param>
		/// <returns>An IDisposable which, when disposed of, will unregister its
		/// respective observer from this observable.</returns>
		public IDisposable Subscribe(IObserver<T> observer)
		{
			return new ObservableValueSubscription(this, observer);
		}


		void UpdateValue(T validatedValue)
		{
			this._value = validatedValue;
			this.NotifyValueChange();
		}


		void NotifyInvalidValue(T invalidValue)
		{
			var message = $"Invalid value: {invalidValue}";
			var exception = new ArgumentException(message, nameof(Value));
			if (this.errorEvent == null)
				throw exception;
			else
				this.errorEvent(exception);
		}

		void ErrorIfCompleted()
		{
			var message = "Observable object has been marked complete";
			if (this.Completed)
				throw new ObjectDisposedException(message);
		}


		private class ObservableValueSubscription : IDisposable
		{

			ObservableValue<T> observable;
			Action<T> valueChangedEventHandler;
			Action completedEventHandler;
			Action<Exception> errorEventHandler;

			public ObservableValueSubscription(
				ObservableValue<T> observable,
				IObserver<T> observer)
			{
				this.observable = observable;
				this.AdaptHandlers(observer);
				this.RegisterHandlers(this.observable);
			}

			public void Dispose() => this.UnregisterHandlers(this.observable);

			void AdaptHandlers(IObserver<T> observer)
			{
				this.valueChangedEventHandler = (T t) => observer.OnNext(t);
				this.completedEventHandler = () => observer.OnCompleted();
				this.errorEventHandler = (Exception e) => observer.OnError(e);
			}

			void RegisterHandlers(ObservableValue<T> observable)
			{
				observable.valueChangedEvent += this.valueChangedEventHandler;
				observable.completedEvent += this.completedEventHandler;
				observable.errorEvent += this.errorEventHandler;
			}

			void UnregisterHandlers(ObservableValue<T> observable)
			{
				observable.valueChangedEvent -= this.valueChangedEventHandler;
				observable.completedEvent -= this.completedEventHandler;
				observable.errorEvent -= this.errorEventHandler;
			}
		}
	}
}
