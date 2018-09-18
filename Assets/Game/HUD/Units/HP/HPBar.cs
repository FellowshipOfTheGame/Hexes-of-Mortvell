using UnityEngine;
using System;
using HexesOfMortvell.DesignPatterns.Observer;
using HexesOfMortvell.Core.Units;

namespace HexesOfMortvell.Hud
{
	public class HPBar : MonoBehaviour
	{
		public HP hp;
		public Transform barTransform;

		private IObserver<int> handler;
		private IDisposable subscription;

		void Awake()
		{
			this.handler = new ValueObserver<int>(
				nextEventHandler: UpdateBarLength);
		}

		void Start()
		{
			this.subscription = this.hp.AsObservable.Subscribe(handler);
			UpdateBarLength(hp.Current);
		}

		void OnDestroy()
		{
			this.subscription.Dispose();
		}

		void UpdateBarLength(int newValue)
		{
			var scale = this.barTransform.localScale;
			scale.x = (float) newValue / hp.max;
			this.barTransform.localScale = scale;
		}
	}
}
