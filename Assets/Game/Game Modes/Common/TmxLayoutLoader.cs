using UnityEngine;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Loading;

namespace HexesOfMortvell.GameModes.Common
{
	public class TmxLayoutLoader : MonoBehaviour
	{
		public Board board;
		public TmxBoardLayout tmxLayout;

		void Start()
		{
			this.board.LoadLayout(tmxLayout.ToBoardLayout());
		}
	}
}
