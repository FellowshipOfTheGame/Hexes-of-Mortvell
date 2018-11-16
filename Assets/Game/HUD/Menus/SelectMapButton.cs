using UnityEngine;
using HexesOfMortvell.Core.Grid.Loading;

namespace HexesOfMortvell.GameModes
{
	public class SelectMapButton : MonoBehaviour
	{
		public string baseSceneName;
		public TmxBoardLayout layout;
		public GameObject referee;

		public void Load()
		{
			var loader = GameLoader.CreateLoader();
			loader.Load(
				this.baseSceneName,
				this.layout.ToBoardLayout(),
				this.referee);
		}
	}
}
