using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shard : MonoBehaviour
{
    public int shardValue = 1;
    public float speed;
    public float baseOffsetTime;
    public float offsetTimeInterval;
    public float totalOffsetTime { get; set; }
    private GameData gd;
    public AudioClip shardClip;
    public bool isCollectible = false;
    public ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        gd = GameObject.Find("Game Data").GetComponent<GameData>();
        if (!isCollectible)
        {
            StartCoroutine(MoveShard(totalOffsetTime));
        }
    }

    IEnumerator MoveShard(float whenToMove)
    {
        yield return new WaitForSeconds(whenToMove);
        //ParticleSystem particles1 = Instantiate(particles, Vector3.zero, Quaternion.identity, gameObject.transform);
        //particles1.Play();
        GetComponent<MeshRenderer>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(true);
        while ((transform.position - gd.bar3D.transform.position).magnitude > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, gd.bar3D.position, Time.deltaTime * speed);
            yield return null;
        }
        gd.sc.Increase(shardValue);
        GetComponent<AudioSource>().clip = shardClip;
        GetComponent<AudioSource>().Play();

        gameObject.transform.position = new Vector3(100000, 100000, 0);
        Destroy(this.gameObject, 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(MoveShard(0)); //immediately
    }
}
