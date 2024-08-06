using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class VirtualCameraManager : MonoBehaviour
{
    public float screenWidthInUnits = 12;
    public float verticalFOVInAngles = 60;
    public float horizontalMarginSize = 0.8f;
    public float verticalMarginSize = 0.4f;
    CinemachineVirtualCamera vcam;
    CinemachineFramingTransposer ft;

    private void Start()
    {
        SceneManager.sceneLoaded += SceneLoaded;
        vcam = GetComponent<CinemachineVirtualCamera>();
        vcam.m_Lens.FieldOfView = verticalFOVInAngles;
        float hFOV = 2 * Mathf.Atan(Camera.main.aspect * Mathf.Tan(verticalFOVInAngles * 0.5f * Mathf.Deg2Rad));  //getting hFOV from vFOV
        float cameraDistance = screenWidthInUnits * 0.5f / Mathf.Tan(hFOV * 0.5f);
        ft = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
        ft.m_CameraDistance = cameraDistance;
        ft.m_DeadZoneWidth = 1 - horizontalMarginSize;
        ft.m_DeadZoneHeight = 1 - verticalMarginSize;
        ft.m_SoftZoneWidth = 1 - horizontalMarginSize;
        ft.m_SoftZoneHeight = 1 - verticalMarginSize;
    }
    //On loaded scene, find and start following a player
    private void SceneLoaded(Scene scene, LoadSceneMode lsm)
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        if (target)
        {
            Vector3 playerPos = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
            ft.ForceCameraPosition(playerPos, Quaternion.identity); //undocumented function to force the vcam to center on the player
            vcam.Follow = target.transform;
        }
        //print(scene.name);
    }
}
