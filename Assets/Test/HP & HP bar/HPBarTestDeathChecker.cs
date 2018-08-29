using System;
using UnityEngine;
using HexCasters.Core.Units;
using HexCasters.DesignPatterns.Observer;

public class HPBarTestDeathChecker : MonoBehaviour
{
	public HP hp;
	private ValueObserver<int> hpObserver;
	private IDisposable hpSubscription;

	void Awake()
	{
		hpObserver = new ValueObserver<int>(
			nextEventHandler: PrintIfDead);
	}

	void Start()
	{
		hpSubscription = hp.AsObservable.Subscribe(hpObserver);
	}

	void OnDestroy()
	{
		hpSubscription.Dispose();
	}

	void PrintIfDead(int hp)
	{
		if (hp == 0)
			Debug.Log("Dead");
	}
}
