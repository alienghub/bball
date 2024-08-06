using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreenTrigger : MonoBehaviour
{
    //TutorialScreen ts;
    GameObject ts;

    void Start()
    {
        ts = GameObject.Find("Tutorial Screen");//.GetComponent<TutorialHand>();
    }

    void OnTriggerEnter()
    {
        ts.SetActive(true);
    }

    void OnTriggerExit()
    {
        //th.enabled = false;
    }
}
