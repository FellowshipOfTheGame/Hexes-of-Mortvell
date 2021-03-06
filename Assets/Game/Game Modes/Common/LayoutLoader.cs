﻿using UnityEngine;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Loading;

namespace HexesOfMortvell.GameModes
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
