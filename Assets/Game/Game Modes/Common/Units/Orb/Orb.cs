using System.Linq;
using UnityEngine;
using HexesOfMortvell.Core.Units;
using HexesOfMortvell.Core.Units.Teams;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Regions;

namespace HexesOfMortvell.GameModes.Common
{
	[RequireComponent(typeof(BoardCellContent), typeof(TeamMember))]
	public class Orb : MonoBehaviour
	{
		public int healAmount;
		private EndTurnListener endTurnListener;
		private BoardCellContent asCellContent;
		private TeamMember asTeamMember;

		void Start()
		{
			this.asCellContent = GetComponent<BoardCellContent>();
			this.asTeamMember = GetComponent<TeamMember>();

			var cellEventListener = this.asCellContent.Cell.eventListener;
			this.endTurnListener = cellEventListener
				.GetComponent<EndTurnListener>();
			this.endTurnListener.turnEndedEvent += HealAlliedAdjacentUnits;
		}

		void OnDestroy()
		{
			this.endTurnListener.turnEndedEvent -= HealAlliedAdjacentUnits;
		}

		void HealAlliedAdjacentUnits()
		{
			var adjacentCells = this.asCellContent.Cell
				.Neighborhood(1)
				.Except(new[] { this.asCellContent.Cell });
			var adjacentThings = adjacentCells
				.Select(cell => cell.Content)
				.Where(content => content != null);
			var adjacentAlliedThings = adjacentThings.Where(IsAllied);
			var adjacentAlliedHPs = adjacentAlliedThings
				.Select(content => content.GetComponent<HP>())
				.Where(hp => hp != null);
			foreach (var hp in adjacentAlliedHPs)
				hp.Increase(this.healAmount);
		}

		bool IsAllied(BoardCellContent content)
		{
			if (this.asTeamMember?.team == null)
				return false;
			var otherAsTeamMember = content.GetComponent<TeamMember>();
			if (otherAsTeamMember == null)
				return false;
			return otherAsTeamMember.team == this.asTeamMember.team;
		}
	}
}
