using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexesOfMortvell.Hud.Menus
{
	public class SavePlayerPrefs : MonoBehaviour
	{
		public void Save() {
			PlayerPrefs.Save();
		}
	}
}
