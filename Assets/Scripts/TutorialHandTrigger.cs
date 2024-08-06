using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandTrigger : MonoBehaviour
{
    TutorialHand th;

    void Start()
    {
        th = GameObject.Find("Tutorial Hand").GetComponent<TutorialHand>();
    }
 
    void OnTriggerEnter()
    {
        th.enabled = true;
    }

    void OnTriggerExit()
    {
        th.enabled = false;
    }
}
