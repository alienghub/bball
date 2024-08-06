using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float bounce;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<Rigidbody>().velocity = new Vector3(0, -bounce, 0);
        }
    }
}
