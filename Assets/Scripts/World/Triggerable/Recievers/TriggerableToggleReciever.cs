using Triggerable.Source;
using UnityEngine;

namespace Triggerable.Destination {
	public abstract class TriggerableToggleReciever : TriggerableDestination {
		public bool isInitiallyToggled;

		private bool isToggled;

		protected override void OnTrigger(TriggerableSource source) {
			Debug.Log("test");
			isToggled = !isToggled;
			OnToggled(isToggled);
		}

		private void Awake() => isToggled = isInitiallyToggled;

		protected abstract void OnToggled(bool isToggled);
	}

}
