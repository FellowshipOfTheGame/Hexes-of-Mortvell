using System;
using UnityEngine;
using HexCasters.Core.Units.Teams;
using HexCasters.DesignPatterns.Observer;

namespace HexCasters.Testing.GameModeTest
{
	public class GameModeTestTurn : MonoBehaviour
	{
		[SerializeField]
		private int startingTurn;
		private ObservableValue<int> observableValue;
		public int Turn
		{
			get { return this.observableValue.Value; }
			private set { this.observableValue.Value = value; }
		}

		public IObservable<int> AsObservable
		{
			get { return this.observableValue; }
		}
		public Team[] teams;

		void Awake()
		{
			this.observableValue = new ObservableValue<int>(startingTurn);
		}

		public void NextTeam()
		{
			this.Turn = (this.Turn + 1) % this.teams.Length;
		}
	}
}