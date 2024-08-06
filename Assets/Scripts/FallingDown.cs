using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class FallingDown : MonoBehaviour
{
    public float minRangeLeft, maxRangeLeft, minRangeRight, maxRangeRight;
    public float timeIntervalbetweenSpawns;
    public float shakingDuration;
    public float[] stonesHeightLeft, stonesHeightRight;
    public GameObject fallingDownObject;

    public IEnumerator CrabAttack()
    {
        while (true)
        {

            CinemachineVirtualCamera vcam = (CinemachineVirtualCamera)Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera;
            CinemachineBasicMultiChannelPerlin noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            noise.m_AmplitudeGain = 1;
            Bounds b = GetComponent<Collider>().bounds;
                for (int i = 0; i < stonesHeightLeft.Length; i++)
                {
                Destroy(Instantiate(fallingDownObject, new Vector3(b.center.x - Random.Range(minRangeLeft, maxRangeLeft), b.center.y + stonesHeightLeft[i], 0), Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))), 10);

                }
                for (int i = 0; i < stonesHeightRight.Length; i++)
                {
                    Destroy(Instantiate(fallingDownObject, new Vector3(b.center.x + Random.Range(minRangeRight, maxRangeRight), b.center.y + stonesHeightRight[i], 0), Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))), 10);
                }
            yield return new WaitForSeconds(shakingDuration);
            noise.m_AmplitudeGain = 0;
            yield return new WaitForSeconds(timeIntervalbetweenSpawns-shakingDuration);
            
        }
    }

    private void OnDestroy()
    {
       // print("OnDestroy"); //object destruction does not destroy coroutines called outside this object, coroutines are attached to foreign monobehaviours
        StopAllCoroutines();
    }

    public void StupidCoroutineWrapper() //for StopAllCoroutines to work
    {
        StartCoroutine(CrabAttack());
    }

}
