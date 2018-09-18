using System;
using UnityEngine;
using HexesOfMortvell.Core.Units;
using HexesOfMortvell.DesignPatterns.Observer;

namespace HexesOfMortvell.Testing.HPTest
{
	public class HPBarTestPrinter : MonoBehaviour
	{
		public HP hp;
		private ValueObserver<int> hpObserver;
		private IDisposable hpSubscription;

		void Awake()
		{
			hpObserver = new ValueObserver<int>(
				nextEventHandler: PrintValue);
		}

		void Start()
		{
			hpSubscription = hp.AsObservable.Subscribe(hpObserver);
		}

		void OnDestroy()
		{
			hpSubscription.Dispose();
		}

		void PrintValue(int value)
		{
			Debug.Log($"HP: {value}");
		}
	}
}
