using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float push;
    Rigidbody rb;
    GameData gd;
    TutorialHand th;

    private void Start()
    {
        gd = GameObject.Find("Game Data").GetComponent<GameData>();
        th = GameObject.Find("Tutorial Hand").GetComponent<TutorialHand>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
          
        if (Input.GetKey("right") || gd.rightButtonisPressed)
        {
            rb.velocity = new Vector3(push, rb.velocity.y, 0);
            gd.timePassedSincePress = 0;
            th.StopAnimation();
        }
        else if(Input.GetKey("left") || gd.leftButtonisPressed)
        {
            rb.velocity = new Vector3(-push, rb.velocity.y, 0);
            gd.timePassedSincePress = 0;
            th.StopAnimation();
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            gd.timePassedSincePress += Time.deltaTime;
        }
    }

}
