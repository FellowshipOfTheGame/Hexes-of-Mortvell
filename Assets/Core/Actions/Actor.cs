using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexCasters.Core.Actions
{
	public class Actor : MonoBehaviour
	{
		List<MethodInfo> actions;

		public void ExampleAction(Actor actor) {}

		void Awake()
		{
			this.actions = new List<MethodInfo>();
			var actionExample = typeof(Actor)
				.GetMethod("ExampleAction");
			foreach (var method in FindActionMethods())
			{
				if (!MatchesSignature(method, actionExample))
					throw new ArgumentException(
						$"Method {method} does not match Action " +
						$"signature: {actionExample}");
				this.actions.Add(method);
			}
			Debug.Log(string.Join(", ", this.actions));
		}

		IEnumerable<MethodInfo> FindActionMethods()
		{
			var type = GetType();
			var typeMethods = type.GetMethods(
				BindingFlags.Instance |
				BindingFlags.Public |
				BindingFlags.NonPublic);
			var actionMethods = typeMethods
				.Where(method => method.IsDefined(typeof(ActionAttribute)));

			return actionMethods;
		}

		bool MatchesSignature(MethodInfo method, MethodInfo expectedMethod)
		{
			if (method.ReturnType != expectedMethod.ReturnType)
				return false;

			var parameterTypes = method.GetParameters()
				.Select(param => param.ParameterType);
			var expectedParameterTypes = expectedMethod.GetParameters()
				.Select(param => param.ParameterType);
			var matches = parameterTypes.Zip(
				expectedParameterTypes,
				(pType, expectedType) => pType == expectedType);
			return matches.All(x => x);
		}
	}
}