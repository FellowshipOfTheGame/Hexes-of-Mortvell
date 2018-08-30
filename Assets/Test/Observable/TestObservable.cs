using UnityEngine;
using System;
using HexCasters.DesignPatterns.Observer;

namespace HexCasters.Testing.ObserverTest
{
	public class TestObservable : MonoBehaviour
	{
		public ObservableValue<int> observable;

		void Start()
		{
			observable = new ObservableValue<int>(0, validator: CheckValue);
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.RightArrow))
				IncreaseValue();
			if (Input.GetKeyDown(KeyCode.LeftArrow))
				DecreaseValue();
			if (Input.GetKeyDown(KeyCode.Space))
				CompleteObservable();
		}

		void IncreaseValue()
		{
			observable.Value++;
		}

		void DecreaseValue()
		{
			observable.Value--;
		}

		void CompleteObservable()
		{
			observable.MarkComplete();
		}


		bool CheckValue(int value)
		{
			return Mathf.Abs(value) <= 5;
		}

	}
}
