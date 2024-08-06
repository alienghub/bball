using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : Monster
{
    public float crabFlashTime = 0.1f;


    private new void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            base.OnTriggerEnter(other);
            StartCoroutine(Flash());
        }
    }

    IEnumerator Flash()
    {
        foreach(Transform t in transform.GetChild(0))
        {
            //print(t.name);
            SkinnedMeshRenderer r = t.GetComponent<SkinnedMeshRenderer>();
            if (r != null)
            {
                //print(r.material.GetColor("_EmissionColor"));
                r.material.SetColor("_EmissionColor", Color.white);
                r.material.EnableKeyword("_EMISSION");
               // print(r.material.GetColor("_EmissionColor"));
                //r.enabled = false;
            }
        }
        yield return new WaitForSeconds(crabFlashTime);
        foreach (Transform t in transform.GetChild(0))
        {
            //print(t.name);
            SkinnedMeshRenderer r = t.GetComponent<SkinnedMeshRenderer>();
            if (r != null)
            {
                r.enabled = true;
                r.material.SetColor("_EmissionColor", Color.black);
            }
        }
    }
}
