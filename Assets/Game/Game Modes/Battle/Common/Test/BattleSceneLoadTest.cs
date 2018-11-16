using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexesOfMortvell.Core.Grid.Loading;
using HexesOfMortvell.GameModes.Common;

namespace HexesOfMortvell.GameModes.Battle.Test
{
	public class BattleSceneLoadTest : MonoBehaviour
	{
		public string baseSceneName;
		public TmxBoardLayout layout;
		public GameObject referee;

		void Start()
		{
			var loaderObj = new GameObject();
			var loader = loaderObj.AddComponent<GameLoader>();
			loader.Load(
				this.baseSceneName,
				this.layout.ToBoardLayout(),
				this.referee);
		}
	}
}
