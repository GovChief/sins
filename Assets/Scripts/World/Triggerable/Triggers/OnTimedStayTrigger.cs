using UnityEngine;
using Triggerable.Editor;

namespace Triggerable.Source {
	[AddComponentMenu(ComponentPath.ON_TIMED_STAY)]
	public class OnTimedStayTrigger : TriggerableSource {

		[Header(Headers.TRIGGER_SETTINGS)]
		[Tooltip(Tooltips.ONLY_PLAYER_TRIGGER)]
		public bool playerOnly = true;

		[Tooltip(Tooltips.TIME_TO_TRIGGER_TOOLTIP)]
		public double timeToTrigger;

		private double timer;

		private void Awake() => enabled = false;

		private void OnTriggerEnter2D(Collider2D collision) {
			if (playerOnly) {
				enabled |= collision.gameObject.tag == "Player";
			} else {
				enabled = true;
			}
		}

		private void Update() {
			if (timer < timeToTrigger) {
				timer += Time.deltaTime;
			} else {
				TriggerDestination();
				timer = 0;
				enabled = false;
			}
		}

		private void OnTriggerExit2D(Collider2D collision) {
			if (playerOnly) {
				if (collision.gameObject.tag == "Player") {
					enabled = false;
					timer = 0;
				}
			} else {
				enabled = false;
				timer = 0;
			}
		}
	}
}
