using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    public float width = 7;
    void Start()
    {
        float hFOV = 2 * Mathf.Atan(Camera.main.aspect * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad));  //getting hFOV from vFOV
        float cameraDistance = width * 0.5f / Mathf.Tan(hFOV * 0.5f);
        transform.position = new Vector3(transform.position.x, transform.position.y, -cameraDistance);
    }

}
