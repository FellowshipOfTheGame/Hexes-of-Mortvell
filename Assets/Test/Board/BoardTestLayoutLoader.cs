using UnityEngine;
using HexCasters.Core.Board;

public class BoardTestLayoutLoader : MonoBehaviour
{
	public Board board;
	public BoardLayout testLayout;
	public GameObject testOrbPrefab;

	void Start()
	{
		this.board.LoadLayout(testLayout);
		this.board.Spawn(testOrbPrefab, new BoardPosition(0, 0));
	}
}
