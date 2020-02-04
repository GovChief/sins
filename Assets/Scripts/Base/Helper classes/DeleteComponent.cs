using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers.AttributeHelpers {
	[ExecuteInEditMode]
	public class DeleteComponent : MonoBehaviour {
		public Component componentReference;
		void Start() {
			if (componentReference != null)
				DestroyImmediate(componentReference);
			DestroyImmediate(this);
		}
	}
}
