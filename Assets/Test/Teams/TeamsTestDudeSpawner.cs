using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HexCasters.Core.Units.Teams;

public class TeamsTestDudeSpawner : MonoBehaviour
{
	public List<Team> teams;
	public Image hoverImage;
	public GameObject dudePrefab;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
			Spawn(this.teams[0]);
		if (Input.GetKeyDown(KeyCode.Alpha2))
			Spawn(this.teams[1]);
		if (Input.GetKeyDown(KeyCode.Alpha3))
			Spawn(this.teams[2]);
		if (Input.GetKeyDown(KeyCode.Alpha4))
			Spawn(this.teams[3]);
	}

	void Spawn(Team team)
	{
		var randomPos = new Vector3();
		randomPos.x = Random.value;
		randomPos.y = Random.value;
		randomPos *= 2;
		var dude = Instantiate(dudePrefab, randomPos, Quaternion.identity);
		var teamMember = dude.GetComponent<TeamMember>();
		teamMember.team = team;
		var teamColored = dude.GetComponent<TeamColoredSpriteRenderer>();
		teamColored.team = team;
		var hoverable = dude.GetComponent<TeamsTestHoverable>();
		hoverable.image = this.hoverImage;
	}
}
