using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBall : MonoBehaviour
{
    public float startTimeOffset;
    public float repeatTime;
    public Vector3 force;
    public GameObject prefab;
    public float destroyAfter;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootBall());
    }

    IEnumerator ShootBall()
    {
        yield return new WaitForSeconds(startTimeOffset);

        while (true)
        {
            GameObject go = Instantiate(prefab, transform.position, transform.rotation);
            go.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
            Destroy(go, destroyAfter);
            yield return new WaitForSeconds(repeatTime);
        }
    }

}
