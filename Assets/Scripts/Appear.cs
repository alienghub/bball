using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Appear : MonoBehaviour
{
    public float appearAfterParticles;
    public float appearAfterPlayer;
    public ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        GetComponent<Rigidbody>().useGravity = false;
        StartCoroutine(makeParticlesAppear());
    }

    IEnumerator makeObjectAppear()
    {
        yield return new WaitForSeconds(appearAfterPlayer);
        transform.GetChild(0).gameObject.SetActive(true);
        GetComponent<Renderer>().enabled = true;
        GetComponent<Rigidbody>().useGravity = true;
    }
    
    IEnumerator makeParticlesAppear()
    {   
        yield return new WaitForSeconds(appearAfterParticles);
        ParticleSystem particles1 = Instantiate(particles, transform.position, Quaternion.identity);
        particles1.Play();
        GetComponent<AudioSource>().Play();
        StartCoroutine(makeObjectAppear());
        Destroy(particles1, 5f);
    }
}
