using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SliderController : MonoBehaviour
{
    public GameData gd;
    public Slider sl;
    public TextMeshProUGUI currentLvlTxt, nextLvlTxt;
    public Image nextLvlImage;
    //public float wobbleStrength = 0.01f;

    public float maxScale;
    public float scaleIncrease;
    public float scaleDecrease;
    public float timeWaitInc;
    public float timeWaitDec;

    Vector3 originalScale;
    Coroutine cor;

    void Start()
    {
        SceneManager.sceneLoaded += SceneLoaded;
        originalScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    //reset bar shards count
    private void SceneLoaded(Scene scene, LoadSceneMode lsm)
    {
        sl.value = 0;
        sl.maxValue = gd.levels[scene.buildIndex].shardsToPass;
        nextLvlImage.color = new Color(0.2509804f, 0.2509804f, 0.2509804f);
    }

    public void Increase(int value)
    {
        sl.value += value;
        if(cor == null)
        {
            cor = StartCoroutine(Wobble());
        }
        if (sl.value >= sl.maxValue)
        {
            nextLvlImage.color = new Color(0.4666667f, 0.7490196f, 1f);
            gd.AllShardsCollected();
        }
    }

    /*IEnumerator Wobble()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 rnd = new Vector3(Random.Range(0, wobbleStrength), Random.Range(0, wobbleStrength), 0);
            gd.sc.gameObject.transform.position += rnd;
            yield return null;
            gd.sc.gameObject.transform.position -= rnd;
        }
    }*/

    public IEnumerator Wobble()
    {
        Vector3 scaleIncVec = new Vector3(scaleIncrease, scaleIncrease, 0);
        Vector3 scaleDecVec = new Vector3(scaleDecrease, scaleDecrease, 0);

        while (transform.localScale.x < maxScale)
        {
            transform.localScale += scaleIncVec;
            yield return new WaitForSeconds(timeWaitInc);
        }
        while (transform.localScale.x > originalScale.x)
        {
            transform.localScale += scaleDecVec;
            yield return new WaitForSeconds(timeWaitDec);
        }
        transform.localScale = originalScale;
        cor = null;
    }
}