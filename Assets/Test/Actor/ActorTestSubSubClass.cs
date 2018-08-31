using HexCasters.Core.Actions;

namespace HexCasters.Testing.ActorTest
{
	public class ActorTestSubSubClass : ActorTestImplementation
	{
		[Action]
		void SubClassAction(Actor actor) {}
	}
}