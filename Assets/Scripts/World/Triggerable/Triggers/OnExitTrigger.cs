﻿using UnityEngine;
using Triggerable.Editor;
using Attributes;

namespace Triggerable.Source {

	[AddComponentMenu(ComponentPath.ON_EXIT)]
	[ComponentRequiresComponent(typeof(BoxCollider2D))]
	public class OnExitTrigger : TriggerableSource {

		[Header(Headers.TRIGGER_SETTINGS)]
		[Tooltip(Tooltips.ONLY_PLAYER_TRIGGER)]
		public bool playerOnly = true;

		private void OnTriggerEnter2D(Collider2D collision) {
			if (playerOnly) {
				if (collision.gameObject.tag == "Player") {
					TriggerDestination();
				}
			} else {
				TriggerDestination();
			}
		}
	}
}
