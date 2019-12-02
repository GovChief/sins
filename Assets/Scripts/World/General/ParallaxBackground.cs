using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float smoothing = 10f;

    private float parallaxScale;
    private Transform cam;
    private Vector3 previousCamPosition;

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    void Start()
    {
        previousCamPosition = cam.position;

        parallaxScale = transform.position.z;
    }

    void Update()
    {
        float parallaxX = (previousCamPosition.x - cam.position.x) * parallaxScale;
        float parallaxY = (previousCamPosition.y - cam.position.y) * parallaxScale;

        float backgroundTargetPositionX = transform.position.x + parallaxX;
        float backgroundTargetPositionY = transform.position.y - parallaxY;

        Vector3 backgroundTargetPosition = new Vector3(backgroundTargetPositionX, backgroundTargetPositionY, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, backgroundTargetPosition, Time.deltaTime / smoothing);

        previousCamPosition = cam.position;
    }
}
