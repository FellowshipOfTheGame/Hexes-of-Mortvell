using System.Collections;
using UnityEngine;
using System;

public class TestObserverDirect : MonoBehaviour
{
	public string observerName;
	public KeyCode unsubscriptionKeycode;
	public TestObservable observableObject;

	private Action<int> valueChangedEventHandler;
	private Action<Exception> errorEventHandler;
	private Action completedEventHandler;

	void Start()
	{
		valueChangedEventHandler = (int value) =>
			Debug.Log($"{observerName}: {value}");
		errorEventHandler = (Exception e) => Debug.LogError(e);
		completedEventHandler = () => Debug.Log($"{observerName}: Completed");
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
		Register();
	}

	void Update()
	{
		if (Input.GetKeyDown(unsubscriptionKeycode))
			Unregister();
	}

	void Register()
	{
		observableObject.observable.valueChangedEvent +=
			valueChangedEventHandler;
		observableObject.observable.errorEvent +=
			errorEventHandler;
		observableObject.observable.completedEvent +=
			completedEventHandler;
	}

	void Unregister()
	{
		observableObject.observable.valueChangedEvent -= valueChangedEventHandler;
		observableObject.observable.errorEvent -= errorEventHandler;
		observableObject.observable.completedEvent -= completedEventHandler;
	}
}
