using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraScript : MonoBehaviour
{

    CinemachineVirtualCamera mainCamera;

    Transform rotateTarget;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<CinemachineVirtualCamera>();

        rotateTarget = mainCamera.m_Follow;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
