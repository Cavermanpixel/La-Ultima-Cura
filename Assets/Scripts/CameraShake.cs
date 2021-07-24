using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
    private float shakeTime;
    private CinemachineVirtualCamera camaraGame;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        camaraGame = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float decayTime)

    {

        CinemachineBasicMultiChannelPerlin cinemachinePerlin = camaraGame.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachinePerlin.m_AmplitudeGain = intensity;
        shakeTime = decayTime;
        Debug.Log("LLame al batido");
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
            if (shakeTime <= 0)
            {
                CinemachineBasicMultiChannelPerlin cinemachinePerlin = camaraGame.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachinePerlin.m_AmplitudeGain = 0f;
            }
        }
    }
}
