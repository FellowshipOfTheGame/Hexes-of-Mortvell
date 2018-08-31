using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HexCasters.Core.Actions
{
	[AttributeUsage(
		AttributeTargets.Method,
		AllowMultiple=false)]
	public class ActionAttribute : Attribute {  }
}