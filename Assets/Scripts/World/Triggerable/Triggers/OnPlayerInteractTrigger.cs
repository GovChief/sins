using UnityEngine;
using Triggerable.Editor;
using Attributes;

namespace Triggerable.Source {

	[AddComponentMenu(ComponentPath.ON_PLAYER_INTERACT)]
	[ComponentRequiresComponent(typeof(BoxCollider2D))]
	public class OnPlayerInteractTrigger : TriggerableSource {

		[Header(Headers.TRIGGER_SETTINGS)]
		[Tooltip(Tooltips.ON_PLAYER_INTERACT_DEBOUNCE)]
		public double debounce = 1;

		private double timeFromInteraction;

		private void Awake() {
			timeFromInteraction = debounce;
		}

		private void OnTriggerEnter2D(Collider2D collision) {
			collision.attachedRigidbody.sleepMode = RigidbodySleepMode2D.NeverSleep;
		}

		private void OnTriggerExit2D(Collider2D collision) {
			collision.attachedRigidbody.sleepMode = RigidbodySleepMode2D.StartAwake;
		}

		private void OnTriggerStay2D(Collider2D collision) {
			if (collision.gameObject.tag == "Player" &&
				Input.GetAxis("Interact") > 0 &&
				timeFromInteraction >= debounce) {

				TriggerDestination();
				timeFromInteraction = 0;
			}

			if (timeFromInteraction < debounce) {
				timeFromInteraction += Time.deltaTime;
			}
		}
	}
}
