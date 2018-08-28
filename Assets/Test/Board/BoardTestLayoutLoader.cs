using UnityEngine;
using HexCasters.Core.Board;

public class BoardTestLayoutLoader : MonoBehaviour
{
	public Board board;
	public BoardLayout testLayout;

	void Start()
	{
		board.LoadLayout(testLayout);
	}
}
