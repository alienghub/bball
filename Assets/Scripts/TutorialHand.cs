using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialHand : MonoBehaviour
{
    public float timeBeforeAppears = 5;
    public float startScale = 1;
    public float endScale = 0.2f;
    public float scalingSpeed = 0.05f;
    public float lingerTime = 1;

    GameData gd;
    Coroutine cor;

    void Start()
    {
        gd = GameObject.Find("Game Data").GetComponent<GameData>();
        transform.localScale = Vector3.one * startScale;
    }

    void Update()
    {
        if(timeBeforeAppears < gd.timePassedSincePress && cor == null)
        {
            gd.timePassedSincePress = 0;
            cor = StartCoroutine(Animate());
        }
    }

    IEnumerator Animate()
    {
        while (true)
        {
            float tempScale = startScale;
            GetComponent<Image>().enabled = true;
            while (tempScale > endScale)
            {
                tempScale -= scalingSpeed;
                transform.localScale = Vector3.one * tempScale;
                yield return null;
            }
            yield return new WaitForSeconds(lingerTime);
        }
    }

    public void StopAnimation()
    {
        if(cor != null)
        {
            StopCoroutine(cor);
            cor = null;
            GetComponent<Image>().enabled = false;
            transform.localScale = Vector3.one * startScale;
        }
    }
}
