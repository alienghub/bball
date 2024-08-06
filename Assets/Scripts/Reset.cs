using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Reset : MonoBehaviour
{
    public ParticleSystem particles;
    public ParticleSystem crashParticles;
    public AudioClip deathClip;
    private GameData gd;

    private void Start()
    {
        gd = GameObject.Find("Game Data").GetComponent<GameData>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<AudioSource>().clip = deathClip;
            GetComponent<AudioSource>().Play();
            StartCoroutine(Pause());
            other.gameObject.SetActive(false);
            ParticleSystem particles1 = Instantiate(particles, other.transform.position - new Vector3(0, 1, 0), Quaternion.identity);
            particles1.Play();
            Destroy(particles1.gameObject, 5f);
        }
        else if (other.tag == "Platform")
        {
            ParticleSystem particles1 = Instantiate(crashParticles, transform.position, Quaternion.identity);
            particles1.Play();
            GetComponent<AudioSource>().Play();
            Destroy(particles1.gameObject, 5f);
            transform.Translate(100000, 100000, 0); //if you hit the platform, move the object out of the way, can't disable cause pause coroutine might stop
            Destroy(gameObject, 5f);
        }
    }


    private void OnCollisionEnter(Collision col) //for falling down object
    {
        if (col.gameObject.tag == "Player")
        {
            GetComponent<AudioSource>().clip = deathClip;
            GetComponent<AudioSource>().Play();
            StartCoroutine(Pause());
            col.gameObject.SetActive(false);
            ParticleSystem particles1 = Instantiate(particles, col.gameObject.transform.position - new Vector3(0, 1, 0), Quaternion.identity);
            particles1.Play();
            Destroy(particles1.gameObject, 5f);
        }
        else if (col.gameObject.tag == "Platform")
        {
            transform.Translate(100000, 100000, 0); //if you hit the platform, move the object out of the way, can't disable cause pause coroutine might stop
            Destroy(gameObject, 5f);
        }
    }


    IEnumerator Pause()
    {
        
        //gd.pause.enabled = true;
        yield return new WaitForSeconds(gd.pauseTime);
        gd.completionPercentage.text = Mathf.Round(gd.sc.gameObject.GetComponent<Slider>().value / gd.sc.gameObject.GetComponent<Slider>().maxValue * 100).ToString() + "% Needed Shards Collected!";
        gd.retryScreen.SetActive(true);
        //gd.pause.enabled = false;
    }
}
