using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HexesOfMortvell.GameModes
{
	public class IngameMenuLoader : MonoBehaviour
	{
		public GameObject ingameMenu;

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
				ingameMenu.SetActive(!ingameMenu.activeSelf);
		}

		public void Quit()
		{
			SceneManager.LoadScene("Main Menu");
		}
	}
}
