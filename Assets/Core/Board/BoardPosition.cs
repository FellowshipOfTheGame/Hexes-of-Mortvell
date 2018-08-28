namespace HexCasters.Core.Board
{

	[System.Serializable]
	public struct BoardPosition
	{
		public readonly int x;
		public readonly int y;
		public readonly int z;

		public BoardPosition(int x, int y)
		{
			this.x = x;
			this.y = y;
			this.z = -(x + y);
		}
	}
}