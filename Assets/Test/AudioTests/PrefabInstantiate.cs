using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabInstantiate : MonoBehaviour
{
	public GameObject prefab;
	public float delay;
	
	private float timeLeft;
	private GameObject g;
	
	public void Start(){
		g = null;
	}
	
	public void inst(){
		if(g == null){
			g = Instantiate(prefab);
			timeLeft = delay;
		}
	}
	
	public void Update(){
		if(g != null){
			timeLeft -= Time.deltaTime;
			if(timeLeft <= 0){
				g.GetComponent<RockPrefabScript>().colide();
				g = null;
			}
		}
	}
}
