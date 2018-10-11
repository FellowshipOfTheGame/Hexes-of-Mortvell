using System;
using System.Linq;
using UnityEngine;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Loading;

namespace HexesOfMortvell.Core.Units.Teams
{
	public class LayoutTeamAssigner : MonoBehaviour
	{
		public TeamGroup teamGroup;
		public Board board;

		public event Action doneAssigningTeams;

		void Awake()
		{
			this.board.doneLoadingEvent += AttributeTeams;
		}

		void AttributeTeams(Board board, BoardLayout layout)
		{
			var spawns = layout.spawnPositions
				.Zip(
					layout.spawnInfo,
					Tuple.Create);
			foreach (var spawn in spawns)
			{
				var spawnPos = spawn.Item1;
				var spawnInfo = spawn.Item2;
				var content = board.Spawn(spawnInfo.prefab, spawnPos);
				if (spawnInfo.HasTeam)
					this.teamGroup.teams[spawnInfo.teamIndex]
						.Add(content.gameObject);
			}
			this.doneAssigningTeams?.Invoke();
		}
	}
}