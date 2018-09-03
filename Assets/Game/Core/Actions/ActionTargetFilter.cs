using UnityEngine;
using System.Collections.Generic;
using HexCasters.Core.Grid;

namespace HexCasters.Core.Actions
{
	/// <summary>
	/// The component of the action which dictates which cells can be targeted.
	/// </summary>
	/// <remarks>
	/// If multiple filter componentss are present, the resulting targets should
	/// be the intersection of the areas given.
	/// </remarks>
	public abstract class ActionTargetFilter : MonoBehaviour
	{
		/// <summary>
		/// The number of targets for the action.
		/// </summary>
		[Tooltip("The number of targets for the action.")]
		public int targetCount;

		/// <summary>
		/// Returns which cells can be used as targets.
		/// </summary>
		/// <param name="actor">The entity trying to perform the action.</param>
		/// <param name="partialTargets">The targets selected so far.</param>
		/// <returns>The cells which could be used as targets.</returns>
		public abstract IEnumerable<BoardCell> ValidTargets(
			BoardCellContent actor,
			IEnumerable<BoardCell> partialTargets);
	}
}