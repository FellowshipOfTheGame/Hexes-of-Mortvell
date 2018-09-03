using UnityEngine;
using HexCasters.Core.Grid;
using HexCasters.GameModes.Common;

namespace HexCasters.GameModes.Common
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
