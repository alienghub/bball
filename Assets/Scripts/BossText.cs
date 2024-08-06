using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossText : MonoBehaviour
{
    public string bossString = "CRAB'S LAIR";
    public float bossStringStayDuration = 3;
    public float bossFadeOutSize = 0.01f;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            this.GetComponent<Collider>().enabled = false;
            StartCoroutine(BossTextAnimation());
        }
    }

    IEnumerator BossTextAnimation()
    {
        GameData gd = GameObject.Find("Game Data").GetComponent<GameData>();
        gd.bossText.text = "{offset}" + bossString + "{/offset}";
        gd.bossText.gameObject.SetActive(true);
        yield return new WaitForSeconds(bossStringStayDuration);
        Color col = gd.bossText.color;
        while (gd.bossText.color.a > 0)
        {
            col.a -= bossFadeOutSize;
            gd.bossText.color = col;
            yield return null;
        }
        gd.bossText.gameObject.SetActive(false);
        col.a = 1;
        gd.bossText.color = col;
    }
}
