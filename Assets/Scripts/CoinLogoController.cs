using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinLogoController : MonoBehaviour
{
    public GameData gd;
    public float maxScale;
    public float scaleIncrease;
    public float scaleDecrease;
    public float timeWaitInc;
    public float timeWaitDec;

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

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
    }
}
