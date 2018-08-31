using UnityEngine;
using System;
using HexCasters.DesignPatterns.Observer;
using HexCasters.Core.Units;

namespace HexCasters.Hud
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
			this.subscription = hp.AsObservable.Subscribe(handler);
			UpdateBarLength(hp.Current);
		}

		void OnDestroy()
		{
			subscription.Dispose();
		}

		void UpdateBarLength(int newValue)
		{
			var scale = barTransform.localScale;
			scale.x = (float) newValue / hp.max;
			barTransform.localScale = scale;
		}
	}
}
