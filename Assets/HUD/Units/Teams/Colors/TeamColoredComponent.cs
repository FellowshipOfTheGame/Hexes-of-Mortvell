using System;
using UnityEngine;
using HexCasters.DesignPatterns.Observer;
using HexCasters.Core.Units.Teams;

public abstract class TeamColoredComponent : MonoBehaviour
{

	public Team team;
	private TeamColor teamColor;
	private IDisposable subscription;

	protected void Start()
	{
		this.teamColor = team.GetComponent<TeamColor>();
		ErrorIfNoTeamColor();
		var handler = new ValueObserver<Color>(
			nextEventHandler: UpdateColor);
		teamColor.AsObservable.Subscribe(handler);
		UpdateColor(teamColor.Color);
	}

	protected void OnDestroy()
	{
		subscription?.Dispose();
	}

	void ErrorIfNoTeamColor()
	{
		if (this.teamColor == null)
			throw new ArgumentException(
				$"Team has no {nameof(TeamColor)} component");
	}

	public abstract void UpdateColor(Color color);
}