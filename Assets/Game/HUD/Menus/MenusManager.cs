using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusManager : MonoBehaviour
{
	public void PlayGame() {
		// TODO
		SceneManager.LoadScene("2 Player Battle");
	}
}
