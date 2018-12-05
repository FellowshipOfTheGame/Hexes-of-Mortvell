using System.Linq;
using UnityEngine;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Regions;
using HexesOfMortvell.Core.Units;
using HexesOfMortvell.Core.Units.Teams;

namespace HexesOfMortvell.GameModes
{
	[RequireComponent(typeof(BoardCellContent), typeof(TeamMember), typeof(HP))]
	public class Skeleton : MonoBehaviour
	{
		public int selfDamage;
		public int damageToEnemies;
		public SkeletonBinding binding;

		private TeamMember asTeamMember;
		private HP hp;
		private BoardCellContent asCellContent;
		private EndTurnListener endTurnListener;

		void Start()
		{
			this.asCellContent = GetComponent<BoardCellContent>();
			this.asTeamMember = GetComponent<TeamMember>();
			this.hp = GetComponent<HP>();

			var cellEventListener = this.asCellContent.Cell.eventListener;
			this.endTurnListener = cellEventListener
				.GetComponent<EndTurnListener>();
			this.endTurnListener.turnEndedEvent += DamageAdjacentEnemies;
			this.endTurnListener.turnEndedEvent += DamageSelf;
		}

		void OnDestroy()
		{
			if (this.binding != null)
				Destroy(this.binding);
			this.endTurnListener.turnEndedEvent -= DamageAdjacentEnemies;
			this.endTurnListener.turnEndedEvent -= DamageSelf;
		}

		void DamageAdjacentEnemies()
		{
			var adjacentCells = this.asCellContent.Cell
				.Neighborhood(1)
				.Except(new[] { this.asCellContent.Cell });
			var adjacentThings = adjacentCells
				.Select(cell => cell.Content)
				.Where(content => content != null);
			var adjacentAlliedThings = adjacentThings.Where(IsEnemy);
			var adjacentAlliedHPs = adjacentAlliedThings
				.Select(content => content.GetComponent<HP>())
				.Where(hp => hp != null);
			foreach (var hp in adjacentAlliedHPs)
				hp.Decrease(this.damageToEnemies);
		}

		void DamageSelf()
		{
			this.hp.Decrease(this.selfDamage);
		}

		bool IsEnemy(BoardCellContent content)
		{
			var otherAsTeamMember = content.GetComponent<TeamMember>();
			if (otherAsTeamMember == null)
				return false;
			return otherAsTeamMember.team != this.asTeamMember.team;
		}
	}
}