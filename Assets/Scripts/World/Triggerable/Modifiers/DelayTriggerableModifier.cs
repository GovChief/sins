using UnityEngine;
using Triggerable.Editor;
using Triggerable.Source;

namespace Triggerable.Modifier {
	[AddComponentMenu(ComponentPath.DELAY)]
	public class DelayTriggerableModifier : TriggerableModifier {

		[Header(Headers.MODIFIER_SETTINGS)]
		[Tooltip(Tooltips.DELAY_TIME_TOOLTIP)]
		public double delayTime;

		private double timer;
		private TriggerableSource source;

		private void Update() {
			if (timer < delayTime) {
				timer += Time.deltaTime;
			} else {
				TriggerDestination(source);
				enabled = false;
			}
		}

		protected override void OnTrigger(TriggerableSource source) {
			timer = 0;
			enabled = true;
			this.source = source;
		}
	}
}
