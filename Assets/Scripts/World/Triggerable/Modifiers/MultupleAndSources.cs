using System.Collections.Generic;
using Triggerable.Editor;
using Triggerable.Modifier;
using Triggerable.Source;
using UnityEngine;

namespace Triggerable.Destination {

	[AddComponentMenu(ComponentPath.MULTIPLE_AND_SOURCES)]
	public class MultupleAndSources: TriggerableModifier {

		[Tooltip(Tooltips.MULTIPLE_DESTINATIONS_TOOLTIP)]
		public List<TriggerableSource> sources;

		private Dictionary<TriggerableSource, bool> sourceStatusDict = new Dictionary<TriggerableSource, bool>();

		protected override void OnTrigger(TriggerableSource source) {
			sourceStatusDict[source] = !sourceStatusDict[source];
			if (!sourceStatusDict.ContainsValue(false)) {
				TriggerDestination(source);
			}
		}

		private void Start() {
			foreach (TriggerableSource source in sources) {
				sourceStatusDict.Add(source, false);
			}
		}
	}
}
