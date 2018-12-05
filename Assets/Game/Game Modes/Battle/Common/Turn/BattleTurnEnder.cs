using UnityEngine;
using HexesOfMortvell.DesignPatterns.Fsm;

namespace HexesOfMortvell.GameModes.Battle
{
	public class BattleTurnEnder : MonoBehaviour
	{
		public FiniteStateMachine fsm;

		public void EndTurn()
		{
			this.fsm.Transition<BattleEndTurnState>();
		}
	}
}
