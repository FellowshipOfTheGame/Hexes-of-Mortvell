using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexesOfMortvell.Test
{
	public class InstantiateOnStart : MonoBehaviour
	{
		public GameObject prefab;
		private TimedWhiteNoise whiteNoise;

		void Start()
		{
			Instantiate(prefab);
			whiteNoise = prefab.GetComponent<TimedWhiteNoise>();
			whiteNoise.startSound();
		}
	}
}
