using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HexesOfMortvell.Hud.Menus
{
	public class ColorBlindSet : MonoBehaviour
	{
		public string param;
		public ColorReference t1;
		public ColorReference t2;
		private Toggle toggle;

		void Start()
		{
			toggle = GetComponent<Toggle>();
			toggle.isOn = PlayerPrefs.GetInt(param) == 1 ? true : false;
		}

		public void ChangeColorBlindToggle()
		{
			t1.SetMode(toggle.isOn);
			t2.SetMode(toggle.isOn);
			PlayerPrefs.SetInt(param, toggle.isOn ? 1 : 0);
		}
	}
}
