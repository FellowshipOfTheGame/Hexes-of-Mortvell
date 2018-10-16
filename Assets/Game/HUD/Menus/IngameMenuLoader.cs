using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameMenuLoader : MonoBehaviour
{
	public void LoadIngameMenu() {
		Scene loadedScene = SceneManager.GetSceneByName("In Game Menu");
		if (loadedScene.isLoaded) {
			//TODO: hide ingame menu
		}
		else {
			//TODO: disable game scene objects
			StartCoroutine(LoadIngameMenuScene());
		}
	}

	IEnumerator LoadIngameMenuScene() {
		SceneManager.LoadScene("In Game Menu", LoadSceneMode.Additive);
		yield return null;
		SceneManager.SetActiveScene(SceneManager.GetSceneByName("In Game Menu"));
	}
}
