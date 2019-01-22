using UnityEngine;
using UnityEngine.UI;
using HexesOfMortvell.Hud.Actions;

namespace HexesOfMortvell.GameModes.Menus
{
	public class ActionSelectButton : MonoBehaviour
	{
		private Text title;
		private Image image;

		void Start()
		{
			this.title = GetComponentInChildren<Text>();
			this.image = GetComponentInChildren<Image>();
		}

		public void SetAction(GameObject action)
		{
			this.title.text = action.name;
			this.image.sprite = action.GetComponent<ActionIcon>().icon;
		}
	}
}