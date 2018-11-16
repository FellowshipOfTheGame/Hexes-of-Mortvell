using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Loading;

namespace HexesOfMortvell.GameModes
{
	public class GameLoader : MonoBehaviour
	{
		private BoardLayout layout;
		private GameObject referee;

		void Awake()
		{
			DontDestroyOnLoad(this.gameObject);
		}

		public void Load(
			string sceneName,
			BoardLayout layout,
			GameObject referee)
		{
			this.layout = layout;
			this.referee = referee;

			SceneManager.sceneLoaded += InstantiateGame;
			SceneManager.LoadScene(sceneName);
		}

		void InstantiateGame(Scene scene, LoadSceneMode loadSceneMode)
		{
			SceneManager.sceneLoaded -= InstantiateGame;
			StartCoroutine(WaitForFrameAndLoad());
		}

		IEnumerator WaitForFrameAndLoad()
		{
			yield return null;
			InstantiateGame();
			Destroy(this.gameObject);
		}

		void InstantiateGame()
		{
			var board = GameObject.FindObjectOfType<Board>();
			board.LoadLayout(this.layout);
			Instantiate(this.referee);
		}
	}
}
