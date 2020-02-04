using System;
using System.Collections.Generic;
using System.Reflection;
using Attributes;
using UnityEngine;

namespace Helpers.AttributeHelpers {
	[ExecuteInEditMode]
	public class LinkComponentsSequential : MonoBehaviour {
		private int WAIT_FOR_UPDATES = 1;
		private string LINKING_TYPE = "Sequenital field linking";

		private int currentUpdates;

		private Component component;
		private List<Component> components = new List<Component>();

		public void Init(Component componentToLink) {
			component = componentToLink;
		}

		private void Update() {
			if (currentUpdates == WAIT_FOR_UPDATES) {
				components.AddRange(gameObject.GetComponents<Component>());
				LinkSequential();
				DestroyImmediate(this);
			}
			currentUpdates++;
		}

		private void LinkSequential() {
			var thisComponentIndex = components.IndexOf(component);
			if (components.Count > thisComponentIndex + 1) {
				var componentAfter = components[thisComponentIndex + 1];
				if (componentAfter.GetType() != GetType()) {
					AutoLinkSequential(component, componentAfter, component.GetType().GetFields());
				}
			}
			if (thisComponentIndex - 1 >= 0) {
				var componentBefore = components[thisComponentIndex - 1];
				if (componentBefore.GetType() != GetType()) {
					AutoLinkSequential(componentBefore, component, componentBefore.GetType().GetFields());
					AutoLinkSequentialInverse(componentBefore, component, component.GetType().GetFields());
				}
			}
		}

		private void AutoLinkSequential(Component componentBefore, Component componentAfter, FieldInfo[] fieldInfos) {
			if (componentBefore.GetType().GetCustomAttribute<HasAutolinkFields>() != null) {
				foreach (FieldInfo fieldInfo in fieldInfos) {
					if (fieldInfo.GetCustomAttribute<AutoLinkFieldSequential>() != null) {
						if (IsSameOrSubtype(componentAfter.GetType(), fieldInfo.FieldType)) {
							fieldInfo.SetValue(componentBefore, componentAfter);
							Logger.LogLinking(LINKING_TYPE,
							componentBefore.GetType().Name,
							fieldInfo.Name,
							componentAfter.GetType().Name);
						}
					}
				}
			}
		}

		private void AutoLinkSequentialInverse(
			Component componentBefore,
			Component componentAfter,
			FieldInfo[] fieldInfos) {

			if (componentAfter.GetType().GetCustomAttribute<HasAutolinkFields>() != null) {
				foreach (FieldInfo fieldInfo in fieldInfos) {
					var autoLinkSequentialAttribute = fieldInfo.GetCustomAttribute<AutoLinkFieldSequential>();
					if (autoLinkSequentialAttribute != null) {
						if (autoLinkSequentialAttribute.IsInverse &&
							IsSameOrSubtype(componentBefore.GetType(), fieldInfo.FieldType)) {

							fieldInfo.SetValue(componentAfter, componentBefore);
							Logger.LogLinking(LINKING_TYPE,
							componentBefore.GetType().Name,
							fieldInfo.Name,
							componentAfter.GetType().Name);
						}
					}
				}
			}
		}

		private bool IsSameOrSubtype(Type typeOfProperty, Type other) =>
			other == typeOfProperty || other.IsSubclassOf(typeOfProperty);
	}

}
