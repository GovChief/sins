using UnityEngine;

namespace Helpers {
	public static class Logger {
		public static void LogLinking(string linkingType, string firstComponent, string fieldName, string secondComponent) {
			Debug.Log("LINKING(" + linkingType + "): '" + fieldName + "' field of '" +
				firstComponent + "' component and '" + secondComponent + "' component.");
		}
	}

}
