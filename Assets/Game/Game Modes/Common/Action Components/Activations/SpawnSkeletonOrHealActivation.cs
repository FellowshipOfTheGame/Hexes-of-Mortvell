using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using HexesOfMortvell.Core.Units;
using HexesOfMortvell.Core.Units.Teams;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;

namespace HexesOfMortvell.GameModes
{
	public class SpawnSkeletonOrHealActivation : ActionActivation
	{
		public GameObject skeletonPrefab;
		public int healAmount;

		public override void Perform(
			BoardCellContent actor,
			IEnumerable<BoardCell> targets,
			IEnumerable<BoardCell> aoe)
		{
			var target = targets.First();
			if (target.Empty)
			{
				TrySpawnSkeleton(actor, target.Position);
			}
			else
			{
				var affectedHPs = aoe
					.Where(cell => !cell.Empty)
					.Select(cell => cell.Content)
					.Select(content => content.GetComponent<HP>())
					.Where(hp => hp != null);
				foreach (var hp in affectedHPs)
				{
					hp.Increase(this.healAmount);
				}
			}
		}

		public override void Cleanup(IEnumerable<BoardCell> aoe)
		{
			var affectedHPs = aoe
				.Select(cell => cell.Content)
				.Select(content => content?.GetComponent<HP>())
				.Where(hp => hp != null);
			foreach (var hp in affectedHPs)
			{
				hp.Commit();
			}
		}

		void TrySpawnSkeleton(BoardCellContent master, BoardPosition position)
		{
			if (master.GetComponent<SkeletonBinding>() != null)
				return;
			var board = master.Cell.board;
			var skeletonObj = board.Spawn(this.skeletonPrefab, position);
			var skeleton = skeletonObj.GetComponent<Skeleton>();
			var skeletonTeamMembership = skeleton.GetComponent<TeamMember>();
			var masterTeamMembership = master.GetComponent<TeamMember>();
			skeletonTeamMembership.team = masterTeamMembership.team;

			var binding = master.gameObject.AddComponent<SkeletonBinding>();
			binding.servant = skeleton;
			skeleton.binding = binding;
		}
	}
}
