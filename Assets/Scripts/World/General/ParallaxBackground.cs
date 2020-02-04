using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace World.General {
	public class ParallaxBackground : MonoBehaviour {
		private const float SLOW_DOWN_FACTOR = 0.1f;

		[Tooltip("Set to 0 to freeze parallax effect on X axis")]
		public float parallaxSpeedHorizonal = 1f;

		[Tooltip("Set to 0 to freeze parallax effect on Y axis")]
		public float parallaxSpeedVertical = 1f;

		private float parallaxScale;
		private Transform cam;
		private Vector3 previousCamPosition;

		private void Awake () {
			cam = Camera.main.transform;
		}

		void Start () {
			previousCamPosition = cam.position;

			parallaxScale = transform.position.z;
		}

	void Update () {
			Vector3 backgroundTargetPosition =
				new Vector3(calculateBackgroundTargetPositionX(),
					calculateBackgroundTargetPositionY(),
					transform.position.z);

			transform.position = Vector3.Lerp(transform.position, backgroundTargetPosition, Time.deltaTime);

			previousCamPosition = cam.position;
		}

		float calculateBackgroundTargetPositionX () {
			float parallaxX = (previousCamPosition.x - cam.position.x)
				* parallaxScale * parallaxSpeedHorizonal * SLOW_DOWN_FACTOR;

			return transform.position.x + parallaxX;
		}

		float calculateBackgroundTargetPositionY () {
			float parallaxY = (previousCamPosition.y - cam.position.y)
				* parallaxScale * parallaxSpeedVertical * SLOW_DOWN_FACTOR;

			return transform.position.y - parallaxY;
		}
	}
}
