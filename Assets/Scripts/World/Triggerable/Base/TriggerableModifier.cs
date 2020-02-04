using Triggerable.Destination;
using Triggerable.Source;
using Attributes;

namespace Triggerable.Modifier {

	[HasAutolinkFields]
	public abstract class TriggerableModifier : TriggerableDestination {

		[AutoLinkFieldSequential]
		public TriggerableDestination destination;

		public void TriggerDestination(TriggerableSource source) => destination.Trigger(source);
	}
}
