using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Monster : MonoBehaviour
{
    [System.Serializable]
    public struct ParticleStruct
    {
        public ParticleSystem particle;
        public Vector2 particleOffset;
    }

    public int hp = 1;
    public float bounce;
    public GameObject[] shards;
    public float numberOfShardsToInstantiate;
    public float shardMinRadius;
    public float shardMaxRadius;
    public ParticleStruct[] hitParticles;
    public ParticleStruct[] deathParticles;
    public AudioClip clipSplat;
    public float timeFreezeTime = 0;
    public float timeFreezeAfter;

    protected void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            col.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, -bounce, 0);
            ProcessCollision();
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, -bounce, 0);
            ProcessCollision();
        }

    }
    IEnumerator ShardsMoveTo(GameObject go, Vector3 to)
    {
        yield return new WaitForSeconds(0.05f);
        Vector3 velocity = Vector3.zero;
        while ((go.transform.position - to).magnitude > 0.01)
        {
            go.transform.position = Vector3.SmoothDamp(go.transform.position, to, ref velocity, 0.1f);
            yield return null;
        }
        Shard sp = go.GetComponent<Shard>();
        sp.enabled = true;
    }



    private void PlayHitEffects()
    {
        if (timeFreezeTime > 0)
            StartCoroutine(TimeFreeze());
        Bounds b = gameObject.GetComponent<Collider>().bounds;
        for (int i = 0; i < hitParticles.Length; i++)
        {
            Vector3 where = new Vector3(b.center.x + hitParticles[i].particleOffset.x, b.center.y + hitParticles[i].particleOffset.y, b.min.z);
            Destroy(Instantiate(hitParticles[i].particle, where, Quaternion.identity), 5);
        }
    }

    private void PlayDeathEffects()
    {
        Bounds b = gameObject.GetComponent<Collider>().bounds;
        for (int i = 0; i < deathParticles.Length; i++)
        {
            Destroy(Instantiate(deathParticles[i].particle, b.center, Quaternion.identity), 5f);
        }
        for (int i = 0; i < numberOfShardsToInstantiate; i++)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(-45, 45));

            GameObject go = Instantiate(shards[Random.Range(0, shards.Length)], b.center + new Vector3(0, 0, -2), rotation);

            Vector3 displacement = new Vector3(Random.Range(shardMinRadius, shardMaxRadius), Random.Range(shardMinRadius, shardMaxRadius), 0);
            StartCoroutine(ShardsMoveTo(go, go.transform.position + displacement));

            Shard sp = go.GetComponent<Shard>();
            sp.totalOffsetTime = sp.baseOffsetTime + sp.offsetTimeInterval * i;
            sp.enabled = false;
        }
    }

    private void ProcessCollision()
    {
        PlayHitEffects();
        hp--;
        GetComponent<AudioSource>().clip = clipSplat;
        GetComponent<AudioSource>().Play();
        if (hp == 0)
        {
            PlayDeathEffects();

            //move out of screen
            gameObject.transform.position = new Vector3(100000, 100000, 0);

            Destroy(this.gameObject, 3f);
        }
    }
    
    IEnumerator TimeFreeze()
    {
        yield return new WaitForSeconds(timeFreezeAfter);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(timeFreezeTime);
        Time.timeScale = 1;
    }
}