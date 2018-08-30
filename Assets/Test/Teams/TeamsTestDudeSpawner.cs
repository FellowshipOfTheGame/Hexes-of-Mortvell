using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HexCasters.Core.Units.Teams;

public class TeamsTestDudeSpawner : MonoBehaviour
{
	public List<Team> teams;
	public List<Transform> teamParents;
	public Image hoverImage;
	public GameObject dudePrefab;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
			Spawn(0);
		if (Input.GetKeyDown(KeyCode.Alpha2))
			Spawn(1);
		if (Input.GetKeyDown(KeyCode.Alpha3))
			Spawn(2);
		if (Input.GetKeyDown(KeyCode.Alpha4))
			Spawn(3);
	}

	void Spawn(int idx)
	{
		var team = this.teams[idx];
		var parent = this.teamParents[idx];
		var randomPos = new Vector3();
		randomPos.x = Random.value;
		randomPos.y = Random.value;
		randomPos *= 2;
		var dude = Instantiate(
			dudePrefab, randomPos, Quaternion.identity, parent);
		var teamMember = dude.GetComponent<TeamMember>();
		teamMember.team = team;
		var hoverable = dude.GetComponent<TeamsTestHoverable>();
		hoverable.image = this.hoverImage;
	}
}
