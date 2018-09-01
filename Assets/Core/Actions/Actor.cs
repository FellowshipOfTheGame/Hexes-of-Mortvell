using UnityEngine;
using System.Collections.Generic;
using HexCasters.Core.Grid;

namespace HexCasters.Core.Actions
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(BoardCellContent))]
	public class Actor : MonoBehaviour
	{
		[SerializeField]
		private List<GameObject> knownActions;
		public Dictionary<string, GameObject> actionsByName;

		void Awake()
		{
			this.actionsByName = new Dictionary<string, GameObject>();
			foreach (var action in this.knownActions)
				this.actionsByName[action.name] = action;
		}

		public bool KnowsAction(string actionName)
		{
			return this.actionsByName.ContainsKey(actionName);
		}

		void ErrorIfUnknownAction(string actionName)
		{
			if (!KnowsAction(actionName))
				throw new System.ArgumentException(
					$"Unknown action: {actionName}");
		}
	}
}