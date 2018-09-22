using System;
using UnityEngine;
using HexesOfMortvell.DesignPatterns.Observer;
using HexesOfMortvell.Core.Units;
using HexesOfMortvell.Core.Grid;

namespace HexesOfMortvell.GameModes.Common
{
	[RequireComponent(typeof(HP), typeof(BoardCellContent))]
	public class DeathNotifyBoard : MonoBehaviour
	{
		private IDisposable hpSubscription;
		private HP hp;
		private DeathListener deathListener;

		void Start()
		{
			var handler = new ValueObserver<int>(NotifyBoardIfZeroHp);
			this.hp = GetComponent<HP>();
			this.hpSubscription = this.hp.AsObservable.Subscribe(handler);
			var asUnit = GetComponent<Unit>();
			var board = asUnit.AsCellContent.Cell.board;
			this.deathListener = board.GetComponent<DeathListener>();
		}

		void OnDestroy()
		{
			this.hpSubscription.Dispose();
		}

		void NotifyBoardIfZeroHp(int currentHp)
		{
			if (currentHp == 0)
				this.deathListener.Notify(this.hp);
		}
	}
}
