using System.Collections.Generic;
using UnityEngine;
using Triggerable.Editor;
using Triggerable.Source;

namespace Triggerable.Destination {

	[AddComponentMenu(ComponentPath.MULTIPLE_DESTINATIONS)]
	public class MultipleDestinationsModifier : TriggerableDestination {

		[Header(Headers.MODIFIER_SETTINGS)]
		public List<TriggerableDestination> destinations;

		protected override void OnTrigger(TriggerableSource source) {
			foreach (TriggerableDestination destination in destinations) {
				destination.Trigger(source);
			}
		}
	}
}
