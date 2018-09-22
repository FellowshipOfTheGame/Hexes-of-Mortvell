using UnityEngine;
using HexesOfMortvell.Core.Units;

namespace HexesOfMortvell.GameModes.Common
{
	public class DestroyObjectOnDeath : MonoBehaviour
	{
		public DeathListener deathListener;

		void Start()
		{
			deathListener.deathEvent += DestroyDeadObject;
		}

		void DestroyDeadObject(HP objectHp)
		{
			Destroy(objectHp.gameObject);
		}
	}
}
