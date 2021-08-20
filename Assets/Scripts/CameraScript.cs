using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraScript : MonoBehaviour
{
    CinemachineBrain brain;
    // Start is called before the first frame update
    void Start()
    {
        brain = GetComponent<CinemachineBrain>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetBlendTime(float t)
    {
        brain.m_DefaultBlend.m_Time = t;
    }
}
