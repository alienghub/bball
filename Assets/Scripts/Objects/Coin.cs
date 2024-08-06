using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;
    public float speed;
    private GameData gd;
    public AudioClip clipPickup;
    public AudioClip clipCounter;
    private float coinRotateWhenPickedSpeedMultiplier = 4;
    public ParticleSystem particles;

    private void Start()
    {
        gd = GameObject.Find("Game Data").GetComponent<GameData>();
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            ParticleSystem particles1 = Instantiate(particles, transform.position, Quaternion.identity);
            particles1.Play();
            Destroy(particles1.gameObject, 5f);
            GetComponent<AudioSource>().clip = clipPickup;
            GetComponent<AudioSource>().Play();
            Destroy(GetComponent<Collider>());
            StartCoroutine(MoveCoin());
        }
    }

    IEnumerator MoveCoin()
    {
        GetComponent<Animation>()["CoinRotate"].speed *= coinRotateWhenPickedSpeedMultiplier;
        while((transform.position - gd.coinLogo3D.position).magnitude > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, gd.coinLogo3D.position, Time.deltaTime * speed);
            yield return null;
        }
        StartCoroutine(gd.coinLogo.GetComponent<CoinLogoController>().Wobble());
        int currentValue = System.Int32.Parse(gd.coinCounter.text);
        currentValue += coinValue ;
        gd.coinCounter.text = currentValue.ToString();
        GetComponent<AudioSource>().clip = clipCounter;
        GetComponent<AudioSource>().Play();
        
        //move out of screen
        gameObject.transform.position = new Vector3(100000, 100000, 0);
        Destroy(this.gameObject, 3f);
    }
}
