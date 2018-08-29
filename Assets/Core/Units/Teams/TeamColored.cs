using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace HexCasters.Core.Units.Teams
{
	public class TeamColored : MonoBehaviour
	{
		public Team team;
		public List<UnityEngine.Object> coloredObjects;

		private TeamColor teamColor;

		void Awake()
		{
			teamColor = this.team.GetComponent<TeamColor>();
			ErrorIfNoColorComponent();
		}

		public void UpdateColor()
		{
			foreach (var obj in coloredObjects)
				UpdateColor(obj);
		}

		void UpdateColor(UnityEngine.Object obj)
		{
			var bindingFlags = BindingFlags.Public | BindingFlags.Instance;
			bindingFlags |= BindingFlags.IgnoreCase;
			var field = obj.GetType().GetField("color", bindingFlags);
			ErrorIfFieldNotFound(field, obj.name);
		}

		void ErrorIfNoColorComponent()
		{
			if (teamColor == null)
				throw new ArgumentException(
					$"Team has no {nameof(TeamColor)} component");
		}

		void ErrorIfFieldNotFound(FieldInfo field, string objName)
		{
			if (field == null)
				throw new ArgumentException(
					$"Object \"{objName}\" has no \"color\" field");
		}
	}
}