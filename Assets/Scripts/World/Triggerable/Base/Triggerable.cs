using UnityEngine;
using Triggerable.Editor;

namespace Triggerable {
	public abstract class Triggerable: BaseMonoBehaviour {
		[Header(Headers.TRIGGERABLE_SETTINGS)]
		[Tooltip(Tooltips.TRIGGERABLE_INITIAL_STATE_TOOLTIP)]
		public bool isTriggerInitialyEnabled = true;

		protected bool isTriggerEnabled;

		private void Start() => isTriggerEnabled = isTriggerInitialyEnabled;

		public virtual void ToggleTriggerEnabled() => isTriggerEnabled = !isTriggerEnabled;

		public virtual void SetTriggerEnabled(bool enabled) => isTriggerEnabled = enabled;
	}
}
