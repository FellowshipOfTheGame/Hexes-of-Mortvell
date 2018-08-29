using UnityEngine;
using System;
using HexCasters.DesignPatterns.Observable;

namespace HexCasters.Core.Units
{
	public class HPBar : MonoBehaviour
	{
		public HP hp;
		public Transform barTransform;

		private IObserver<int> handler;
		private IDisposable subscription;

		void Awake()
		{
			handler = new ValueObserver<int>(
				nextEventHandler: UpdateBarLength);
		}

		void Start()
		{
			this.subscription = hp.CurrentAsObservable.Subscribe(handler);
			UpdateBarLength(hp.Current);
		}

		void Destroy()
		{
			subscription.Dispose();
		}

		void UpdateBarLength(int newValue)
		{
			barTransform.localScale = (float) newValue / hp.max * Vector2.right;
		}
	}
}
