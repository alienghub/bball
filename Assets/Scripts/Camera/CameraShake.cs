using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public float shakeTime = 3.0f;
    public float shakeTimeOffset = 0f;
    public float shakeAmplitude = 1.0f;
    private CinemachineVirtualCamera vcam;
    private CinemachineBasicMultiChannelPerlin noise;
    static private Coroutine shake;

    void OnTriggerEnter(Collider other)
    {
        vcam = (CinemachineVirtualCamera)Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera;
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (shake == null)
        {
            shake = StartCoroutine(Shake());
        }
    }

    void OnCollisionEnter(Collision other)
    {
        vcam = (CinemachineVirtualCamera)Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera;
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (shake == null)
        {
            shake = StartCoroutine(Shake());
        }
    }
    IEnumerator Shake()
    {
        yield return new WaitForSeconds(shakeTimeOffset);
        noise.m_AmplitudeGain = shakeAmplitude;
        yield return new WaitForSeconds(shakeTime);
        noise.m_AmplitudeGain = 0;
        shake = null;
    }



}
