using UnityEngine;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Loading;

namespace HexesOfMortvell.GameModes
{
	public class TmxLayoutLoaderOld : MonoBehaviour
	{
		public Board board;
		public TmxBoardLayout tmxLayout;

		void Start()
		{
			this.board.LoadLayout(this.tmxLayout.ToBoardLayout());
		}
	}
}
