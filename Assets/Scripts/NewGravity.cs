using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGravity : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 gravity;
    // Update is called once per frame
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        rb.velocity += gravity;
    }
}
