using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossIntro : MonoBehaviour
{
    public GameObject bossCamera;
    public Animator bossAnimator;
    public CrabRoam cr;
    public FallingDown fd;
    public ParticleSystem particle;
    public AudioSource bossAudioSource;
    //public AudioClip bossIntroAudio;
 

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            this.GetComponent<Collider>().enabled = false;
            StartCoroutine(BossIntroAnimation());
        }
    }

    IEnumerator BossIntroAnimation()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        //rb.constraints = rb.constraints | RigidbodyConstraints.FreezePosition;

        bossCamera.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        CinemachineVirtualCamera vcam = bossCamera.GetComponent<CinemachineVirtualCamera>();
        CinemachineBasicMultiChannelPerlin noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain = 5;
        bossAnimator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.3f);
        ParticleSystem particle1 = Instantiate(particle, bossAnimator.gameObject.transform.position + new Vector3(0, 0, 5), particle.transform.rotation);
        yield return new WaitForSeconds(0.5f);
        //float oldVol = bossAudioSource.volume;
        //bossAudioSource.volume = 1;
        bossAudioSource.PlayOneShot(bossAudioSource.clip, 2.5f);
        //bossAudioSource.volume = oldVol;
        
        CinemachineTransposer t = vcam.GetCinemachineComponent<CinemachineTransposer>();
        t.m_FollowOffset = new Vector3(0, t.m_FollowOffset.y, -13); 
        yield return new WaitForSeconds(0.8f);
        t.m_FollowOffset = new Vector3(0, t.m_FollowOffset.y, -8);
        yield return new WaitForSeconds(0.8f);
        Destroy(particle1.gameObject);
        //rb.constraints = rb.constraints ^ RigidbodyConstraints.FreezePosition;
        rb.isKinematic = false;
        bossAnimator.SetBool("isWalking", true);
        cr.enabled = true;
        bossCamera.SetActive(false);
        fd.StupidCoroutineWrapper();
        //StartCoroutine(fd.CrabAttack());
    }

}

