using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Attributes;
using Helpers.AttributeHelpers;
using Inspector.Dialogs;
using UnityEngine;

namespace Managers {
	public class SceneObjectManager {

		private const string AUTO_LINK_FIELDS_TYPE = "Auto field linking";

		public static SceneObjectManager globalManager = new SceneObjectManager();

		public static void OnNewItemAdded(GameObject gameObject, Component component) {
			Type type = component.GetType();
			List<Component> components = new List<Component>(gameObject.GetComponents<Component>());
			FieldInfo[] fieldInfos = type.GetFields();

			if (!CancelAddingDialogAttribute(gameObject, component, type)) {
				WarningMessageAttribute(type);
				var isLinkingAllowed = LinkingCheckAttribute(type);
				if (isLinkingAllowed) {
					AutoLinkFieldAttribute(component, fieldInfos, components, type);
					AutoLinkSequentialFields(component, gameObject);
				}
				RequiresComponentsAttribute(gameObject, fieldInfos, components, component, type);
				ComponentRequiresComponentAttribute(gameObject, component, components, type);
				AllowOnlyOneAttributes(gameObject, component, isLinkingAllowed, type);
			}
		}

		private static bool CancelAddingDialogAttribute(GameObject gameObject, Component component, Type componentType) {
			WithCancelAddingDialog withWarningDialog = componentType.GetCustomAttribute<WithCancelAddingDialog>();
			if (withWarningDialog != null) {
				if (EditorDialogs.WarningDialogWithCancel(withWarningDialog.WarningMessage)) {
					return false;
				}
				gameObject.AddComponent<DeleteComponent>().componentReference = component;
				return true;
			}
			return false;
		}

		private static bool LinkingCheckAttribute(Type componentType) {
			if (componentType.GetCustomAttribute<WithLinkingCheckDialog>() != null) {
				return EditorDialogs.AllowLinkingDialog();
			}
			return true;
		}

		private static void WarningMessageAttribute(Type componentType) {
			WithWarningDialog withWarningDialog = componentType.GetCustomAttribute<WithWarningDialog>();
			if (withWarningDialog != null) {
				EditorDialogs.WarningDialog(withWarningDialog.WarningMessage);
			}
		}

		private static void AllowOnlyOneAttributes(GameObject gameObject, Component component, bool isLinkingAllowed, Type componentType) {
			AllowOnlyOne allowOnlyOne = componentType.GetCustomAttribute<AllowOnlyOne>();
			if (allowOnlyOne != null) {
				Component OldItem = gameObject.GetComponent(componentType);
				if (OldItem != null && OldItem != component) {
					if (EditorDialogs.ReplaceComponentDialog()) {
						gameObject.AddComponent<DeleteComponent>().componentReference = OldItem;
						if (isLinkingAllowed && allowOnlyOne.HasLinks) {
							List<MonoBehaviour> components = new List<MonoBehaviour>(gameObject.GetComponents<MonoBehaviour>());
							for (int i = 0; i < components.Count; i++) {
								if (components[i] == component && (i + 1) < components.Count) {
									Debug.Log("AllowOnlyOne -> Links");
									//gameObject.AddComponent<LinkComponents>().Init(components[i]);
									return;
								}
							}
						}
					} else {
						gameObject.AddComponent<DeleteComponent>().componentReference = component;
					}
				}
			}
		}

		private static void AutoLinkFieldAttribute(Component component, FieldInfo[] fieldInfos, List<Component> components, Type componentType) {
			if (componentType.GetCustomAttribute<HasAutolinkFields>() != null) {
				foreach (var fieldInfo in fieldInfos) {
					if (fieldInfo.GetCustomAttribute<AutoLinkField>() != null) {
						Component matchingComponent = components.Find((Component eachComponent) =>
										IsSameOrSubtype(eachComponent.GetType(), fieldInfo.FieldType));
						if (matchingComponent != null) {
							fieldInfo.SetValue(component, matchingComponent);
							Helpers.Logger.LogLinking(AUTO_LINK_FIELDS_TYPE, component.name, fieldInfo.Name, matchingComponent.name);
						}
					} else if (fieldInfo.GetCustomAttribute<AutoLinkListField>() != null) {
						Type fieldType = fieldInfo.FieldType.GetGenericArguments()[0];
						var instancedList = CreateListOfType(fieldType);
						components.ForEach((Component eachComponent) => {
							if (IsSameOrSubtype(eachComponent.GetType(), fieldType)) {
								instancedList.Add(eachComponent);
							}
						});
						if (instancedList.Count > 0) {
							fieldInfo.SetValue(component, instancedList);
							Helpers.Logger.LogLinking(AUTO_LINK_FIELDS_TYPE, component.name, fieldInfo.Name, fieldType.Name);
						}
					}
				}
			}
		}

		private static void AutoLinkSequentialFields(Component component, GameObject gameObject) =>
			gameObject.AddComponent<LinkComponentsSequential>().Init(component);

		private static void RequiresComponentsAttribute(GameObject gameObject, FieldInfo[] fieldInfos, List<Component> components, Component component, Type componentType) {
			if (componentType.GetCustomAttribute<RequiresComponents>() != null) {
				foreach (FieldInfo fieldInfo in fieldInfos) {
					if (fieldInfo.GetCustomAttribute<FieldRequiresComponent>() != null) {
						Component matchingComponent = FindComponentOfType(components, fieldInfo.FieldType);
						if (matchingComponent != null) {
							fieldInfo.SetValue(component, matchingComponent);
						} else {
							if (EditorDialogs.RequiresComponentDialog(fieldInfo.FieldType.Name)) {
								fieldInfo.SetValue(component, gameObject.AddComponent(fieldInfo.FieldType));
							} else {
								gameObject.AddComponent<DeleteComponent>().componentReference = component;
								return;
							}
						}
					} else {
						FieldListRequiresComponent attribute = fieldInfo.GetCustomAttribute<FieldListRequiresComponent>();
						if (attribute != null) {
							Type fieldType = fieldInfo.FieldType.GetGenericArguments()[0];
							var listOfType = CreateListOfType(fieldType);
							ForEachTypeComponent(components, fieldType, (Component eachComponent) => listOfType.Add(eachComponent));
							if (attribute.RequireAtLeast > listOfType.Count) {
								if (EditorDialogs.RequiresMultipleComponentsDialog(fieldInfo.FieldType.Name,
									attribute.RequireAtLeast - listOfType.Count, attribute.RequireAtLeast)) {
									for (int i = 0; i < attribute.RequireAtLeast; i++) {
										listOfType.Add(gameObject.AddComponent(fieldInfo.FieldType));
									}
								} else {
									gameObject.AddComponent<DeleteComponent>().componentReference = component;
									return;
								}
							}
							fieldInfo.SetValue(component, listOfType);
						}
					}
				}
			}
		}

		private static void ComponentRequiresComponentAttribute(GameObject gameObject, Component component, List<Component> components, Type componentType) {
			List<ComponentRequiresComponent> attributes = new List<ComponentRequiresComponent>();
			attributes.AddRange(componentType.GetCustomAttributes<ComponentRequiresComponent>());
			foreach (ComponentRequiresComponent attribute in attributes) {
				var requiredComponentType = attribute.requiredComponentType;
				Component matchingComponent = FindComponentOfType(components, requiredComponentType);
				if (matchingComponent == null) {
					if (EditorDialogs.RequiresComponentDialog(requiredComponentType.Name)) {
						gameObject.AddComponent(requiredComponentType);
					} else {
						gameObject.AddComponent<DeleteComponent>().componentReference = component;
						return;
					}
				}
			}
		}


		private static bool IsSameOrSubtype(Type typeOfProperty, Type other) =>
				other == typeOfProperty || other.IsSubclassOf(typeOfProperty);

		private static void ForEachTypeComponent(List<Component> components, Type type, Action<Component> doOnMatchingComponent) {
			components.ForEach((Component component) => {
				if (IsSameOrSubtype(component.GetType(), type)) {
					doOnMatchingComponent.Invoke(component);
				}
			});
		}

		private static Component FindComponentOfType(List<Component> components, Type type) =>
			components.Find((Component component) => IsSameOrSubtype(component.GetType(), type));

		private static IList CreateListOfType(Type type) =>
			(IList)typeof(List<>).MakeGenericType(type).GetConstructor(Type.EmptyTypes).Invoke(null);

	}

}
