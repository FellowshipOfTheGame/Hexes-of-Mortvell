using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexCasters.Core.Actions
{
	public class Actor : MonoBehaviour
	{
		IDictionary<string, MethodInfo> actions;

		public void ExampleAction() {}

		void Awake()
		{
			this.actions = new Dictionary<string, MethodInfo>();
			var actionExample = typeof(Actor)
				.GetMethod("ExampleAction");
			foreach (var method in FindActionMethods())
			{
				if (!MatchesSignature(method, actionExample))
					throw new ArgumentException(
						$"Method {method} does not match Action " +
						$"signature: {actionExample}");
				this.actions[method.Name] = method;
			}
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
				.Select(param => param.ParameterType).ToList();
			var expectedParameterTypes = expectedMethod.GetParameters()
				.Select(param => param.ParameterType).ToList();
			if (parameterTypes.Count != expectedParameterTypes.Count)
				return false;

			var matches = parameterTypes.Zip(
				expectedParameterTypes,
				(pType, expectedType) => pType == expectedType);
			return matches.All(x => x);
		}


		public void Perform(string actionName)
		{
			ErrorIfUnknownAction(actionName);
			this.actions[actionName].Invoke(this, null);
		}

		void ErrorIfUnknownAction(string actionName)
		{
			if (!this.actions.ContainsKey(actionName))
				throw new ArgumentException($"Unknown action: {actionName}");
		}
	}
}