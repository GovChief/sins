using UnityEngine;
using Triggerable.Source;
using Triggerable.Modifier;

namespace Inspector.Components {
	[ExecuteInEditMode]
	public class TriggerableComponentSorter : MonoBehaviour {
		private void Start() {
			var source = gameObject.GetComponent<TriggerableSource>();
			var triggerables = gameObject.GetComponents<TriggerableModifier>();
			var destination = gameObject.GetComponent<Triggerable.Destination.MultipleDestinationsModifier>();
			if (triggerables.Length > 0) {
				source.destination = triggerables[0];
			}
			for (int i = 1; i < triggerables.Length; i++) {
				triggerables[i - 1].destination = triggerables[i]; 
			}
			if (destination != null) {
				triggerables[triggerables.Length - 1].destination = destination;
			}
			DestroyImmediate(this);
		}
	}

}
