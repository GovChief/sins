using Triggerable.Destination;
using UnityEngine;
using Attributes;

namespace Triggerable.Source {

	[HasAutolinkFields]
	public abstract class TriggerableSource : Triggerable {

		[AutoLinkFieldSequential]
		public TriggerableDestination destination;

		public void TriggerDestination() => destination.Trigger(this);
	}
}
