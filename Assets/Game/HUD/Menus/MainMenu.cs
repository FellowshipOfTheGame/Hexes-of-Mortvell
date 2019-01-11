using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HexesOfMortvell.Hud.Menus
{
	public class MainMenu : MonoBehaviour
	{
		public void PlayGame() {
			SceneManager.LoadScene("Select Map Menu");
		}
	}
}
