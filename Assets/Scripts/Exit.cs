using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Exit : MonoBehaviour
{
    public GameData gd;
    public CinemachineVirtualCamera cvc;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.SetActive(false);
            gd.LoadNewLevel();
            cvc.enabled = true;
            GetComponent<AudioSource>().Play();
        }
            
    }
}
