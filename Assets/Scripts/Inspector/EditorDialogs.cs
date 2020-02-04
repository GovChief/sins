using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inspector.Dialogs {
	public static class EditorDialogs {
		public static bool BasicDialog(string title, string message) =>
			UnityEditor.EditorUtility.DisplayDialog(title, message, "Yes", "No");

		public static bool ReplaceComponentDialog() =>
			BasicDialog("Do you want to replace components?", "Only one component of this type can be assigned!");

		public static bool AutoAssignTriggerables() =>
			BasicDialog("Auto assign Triggerables?", "Doing this will connect Triggerables by order in gameObject!");

		public static void WarningDialog(string message) =>
			UnityEditor.EditorUtility.DisplayDialog("Warning!", message, "Ok");

		public static bool WarningDialogWithCancel(string message) =>
			BasicDialog("Are you sure you want to add this component?", message);

		public static bool AllowLinkingDialog() =>
			BasicDialog("Allow linking?", "Do you want allow linking features?");

		public static bool RequiresComponentDialog(string componentName) =>
			BasicDialog(componentName + " component required!",
				"Do you want to add required component? No will cancel adding your component.");

		public static bool RequiresMultipleComponentsDialog(string componentName, int countOfRequired, int totalCount) =>
			BasicDialog(countOfRequired + " " + componentName + " components required!",
				totalCount + " components of type " + componentName +
				" are required. Do you want to add required components? No will cancel adding your component.");
	}
}
