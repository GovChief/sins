using UnityEngine;
using Triggerable.Editor;
using Triggerable.Source;

namespace Triggerable.Modifier {

	[AddComponentMenu(ComponentPath.REPEATER)]
	public class RepeaterModifier : TriggerableModifier {

		#region constants

		private const int MINIMAL_REPEAT_COUNT = 0;
		private const int MINIMAL_REPEAT_INTERVAL = 0;
		private const int DEFAULT_REPEAT_COUNT = 1;
		private const int DEFAULT_REPEAT_INTERVAL = 1;

		#endregion

		[Header(Headers.MODIFIER_SETTINGS)]
		[Tooltip(Tooltips.REPEAT_COUNT_TOOLTIP)]
		[Min(MINIMAL_REPEAT_COUNT)]
		public int repeatCount = DEFAULT_REPEAT_COUNT;

		[Tooltip(Tooltips.REPEAT_INTEVAL_TOOLTIP)]
		[Min(MINIMAL_REPEAT_INTERVAL)]
		public double repeatInterval = DEFAULT_REPEAT_INTERVAL;

		private double timer;
		private int repeatCounter;
		private TriggerableSource source;

		private void Reset() {
			enabled = false;
		}

		private void Update() {
			if (repeatCounter >= repeatCount) {
				repeatCounter = 0;
				timer = 0;
				enabled = false;
			} else if (timer < repeatInterval) {
				timer += Time.deltaTime;
			} else {
				timer = 0;
				repeatCounter++;
				TriggerDestination(source);
			}
		}

		protected override void OnTrigger(TriggerableSource source) {
			enabled = true;
			this.source = source;
		}
	}
}

