using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Follow : MonoBehaviour
{
    //public float leftMarginInPx, rightMarginInPx, bottomMarginInPx, topMarginInPx;
    public float leftMargin01, rightMargin01, bottomMargin01, topMargin01;
    public float boundGlobalLeft, boundGlobalRight, boundGlobalTop, boundGlobalBot;
    private GameObject target;

    float leftMarginDistanceFromCamera, rightMarginDistanceFromCamera, bottomMarginDistanceFromCamera, topMarginDistanceFromCamera;

    private void Start()
    {
        SceneManager.sceneLoaded += SceneLoaded;

        //Calculate margins' positions in the world space
        /*
        Vector3 leftMargin  = Camera.main.ScreenToWorldPoint(new Vector3(leftMarginInPx, 0, Mathf.Abs(transform.position.z)));
        Vector3 rightMargin = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - rightMarginInPx, 0, Mathf.Abs(transform.position.z)));
        Vector3 bottomMargin = Camera.main.ScreenToWorldPoint(new Vector3(0, bottomMarginInPx, Mathf.Abs(transform.position.z)));
        Vector3 topMargin = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height - topMarginInPx, Mathf.Abs(transform.position.z)));
        */
        Vector3 leftMargin = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * leftMargin01, 0, Mathf.Abs(transform.position.z)));
        Vector3 rightMargin = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - rightMargin01 * Screen.width, 0, Mathf.Abs(transform.position.z)));
        Vector3 bottomMargin = Camera.main.ScreenToWorldPoint(new Vector3(0, bottomMargin01 * Screen.height, Mathf.Abs(transform.position.z)));
        Vector3 topMargin = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height - bottomMargin01 * Screen.height, Mathf.Abs(transform.position.z)));
        leftMarginDistanceFromCamera = leftMargin.x - transform.position.x;
        rightMarginDistanceFromCamera = rightMargin.x - transform.position.x;
        bottomMarginDistanceFromCamera = bottomMargin.y - transform.position.y;
        topMarginDistanceFromCamera = topMargin.y - transform.position.y;
    }

    //Focus on a new target
    private void SceneLoaded(Scene scene, LoadSceneMode lsm)
    {
        target = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }

    void Update()
    {
        if (target)
        {
            if ((target.transform.position.x - transform.position.x > rightMarginDistanceFromCamera) &&
                !(transform.position.x > boundGlobalRight))
            {
                Vector3 vel = target.GetComponent<Rigidbody>().velocity;
                if (vel.x > 0)
                    transform.Translate(vel.x * Time.deltaTime, 0, 0);

            }
            else if ((target.transform.position.x - transform.position.x < leftMarginDistanceFromCamera) &&
                !(transform.position.x < boundGlobalLeft))
            {
                Vector3 vel = target.GetComponent<Rigidbody>().velocity;
                if (vel.x < 0)
                    transform.Translate(vel.x * Time.deltaTime, 0, 0);
            }

            if ((target.transform.position.y - transform.position.y > topMarginDistanceFromCamera) &&
                !(transform.position.y > boundGlobalTop))
            {
                Vector3 vel = target.GetComponent<Rigidbody>().velocity;
                if (vel.y > 0)
                    transform.Translate(0, vel.y * Time.deltaTime, 0);
            }
            else if ((target.transform.position.y - transform.position.y < bottomMarginDistanceFromCamera) &&
                !(transform.position.y < boundGlobalBot))
            {
                Vector3 vel = target.GetComponent<Rigidbody>().velocity;
                if (vel.y < 0)
                    transform.Translate(0, vel.y * Time.deltaTime, 0);
            }
        }
    }
}


