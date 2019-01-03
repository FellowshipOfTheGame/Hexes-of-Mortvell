using UnityEngine;
using HexesOfMortvell.Core.Actions;

namespace HexesOfMortvell.GameModes.Menus
{
    public class ActionSelectMenu : MonoBehaviour
    {
        public GameObject actionButtonPrefab;

        public void Clear()
        {
            foreach (Transform child in this.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void LoadActionSet(ActionSet actionSet)
        {
            foreach (var action in actionSet.actions)
            {
                AddAction(action);
            }
        }

        void AddAction(GameObject action)
        {

        }
    }
}
