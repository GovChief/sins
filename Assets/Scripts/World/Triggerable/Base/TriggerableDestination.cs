using Triggerable.Source;

namespace Triggerable.Destination {

	public abstract class TriggerableDestination : Triggerable {
		public void Trigger(TriggerableSource source) {
			if (isTriggerEnabled) {
				OnTrigger(source);
			}
		}

		protected abstract void OnTrigger(TriggerableSource source);
	}
}
