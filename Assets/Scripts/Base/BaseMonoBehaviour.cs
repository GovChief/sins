using UnityEngine;
using Managers;

public abstract class BaseMonoBehaviour : MonoBehaviour {

#if UNITY_EDITOR

	private void Reset() {
		SceneObjectManager.OnNewItemAdded(gameObject, this);
	}
#endif
}
