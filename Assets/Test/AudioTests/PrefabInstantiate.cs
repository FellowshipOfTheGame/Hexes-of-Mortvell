using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabInstantiate : MonoBehaviour
{
	public GameObject prefab;
	
	
	public void Start(){
	}
	
	public void inst(){
		Instantiate(prefab);
	}
}
