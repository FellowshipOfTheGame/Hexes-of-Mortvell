using UnityEngine;
using System;
using System.Collections;
using HexCasters.DesignPatterns.Obserable;

public class TestObserver : MonoBehaviour
{
	public string observerName;
	public KeyCode unsubscriptionKeycode;

	private IObserver<int> observer;
	public TestObservable observableObject;
	IDisposable observerSubscription;

	void Start()
	{
		observer = new ValueObserver<int>(
			nextEventHandler: (int value) =>
				Debug.Log($"{observerName}: {value}"),
			completeEventHandler: () => Debug.Log($"{observerName}: Completed"),
			errorEventHandler: (Exception e) =>
				Debug.LogError(e));
		RegisterObserver();
	}

	void RegisterObserver()
	{
		StartCoroutine(WaitForObservable());
	}

	IEnumerator WaitForObservable()
	{
		while (observableObject.observable == null)
			yield return null;
		observerSubscription = observableObject.observable.Subscribe(observer);
	}

	void Update()
	{
		if (Input.GetKeyDown(unsubscriptionKeycode))
			observerSubscription.Dispose();
	}
}
