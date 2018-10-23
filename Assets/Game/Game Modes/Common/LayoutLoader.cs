using UnityEngine;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Loading;
using HexesOfMortvell.GameModes.Common;

namespace HexesOfMortvell.GameModes.Common
{
	public class LayoutLoader : MonoBehaviour
	{
		public Board board;
		public BoardLayout layout;

		void Start()
		{
			this.board.LoadLayout(this.layout);
		}
	}
}
