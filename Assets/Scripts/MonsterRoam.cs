using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterRoam : MonoBehaviour
{
    public float leftX, rightX;
    Vector3 min, max;
    public int moveDirection = 1;
    public float rotateAngle = 180;

    void Start()
    {
        min = new Vector3(transform.position.x + leftX, transform.position.y, transform.position.z);
        max = new Vector3(transform.position.x + rightX, transform.position.y, transform.position.z); 
    }

    void Update()
    {
        transform.Translate( Time.deltaTime * Vector3.right);

        if (moveDirection == 1)
        {
            if (max.x - transform.position.x < 0)
            {
                transform.Rotate(0, rotateAngle, 0);
                moveDirection = -1;
            }
        }

        if (moveDirection == -1)
        {
            if (min.x - transform.position.x > 0)
            {
                transform.Rotate(0, rotateAngle, 0);
                moveDirection = 1;
            }
        }
    }
}
