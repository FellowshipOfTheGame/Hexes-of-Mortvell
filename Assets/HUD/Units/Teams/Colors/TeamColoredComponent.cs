using System;
using UnityEngine;
using HexCasters.DesignPatterns.Observer;
using HexCasters.Core.Units.Teams;

public abstract class TeamColoredComponent : MonoBehaviour
{

	public Team team;
	private TeamColor teamColor;

	protected void Awake()
	{
		this.teamColor = team.GetComponent<TeamColor>();
		ErrorIfNoTeamColor();
	}

	protected void Start()
	{
		var handler = new ValueObserver<Color>(
			nextEventHandler: UpdateColor);
		teamColor.AsObservable.Subscribe(handler);
		UpdateColor(teamColor.Color);
	}

	void ErrorIfNoTeamColor()
	{
		if (this.teamColor == null)
			throw new ArgumentException(
				$"Team has no {nameof(TeamColor)} component");
	}

	public abstract void UpdateColor(Color color);
}