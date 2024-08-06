using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public ParticleSystem particles;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<AudioSource>().Play();
            ParticleSystem particles1 = Instantiate(particles, transform.position, Quaternion.identity);
            particles1.Play();
            Destroy(particles1.gameObject, 5f);
            AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position);
            GetComponent<AudioSource>().Play();
            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject, 5f);
        }
    }

}
