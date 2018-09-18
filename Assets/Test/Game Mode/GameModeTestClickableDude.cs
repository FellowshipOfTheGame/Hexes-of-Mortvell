using UnityEngine;

namespace HexesOfMortvell.Testing.GameModeTest
{
	public class GameModeTestClickableDude : MonoBehaviour
	{
		public DudeClickListener dudeClickListener;

		void OnMouseDown()
		{
			this.dudeClickListener.NotifyClick(this.gameObject);
		}
	}
}