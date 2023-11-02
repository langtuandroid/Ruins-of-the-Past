using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShaker : MonoBehaviour
{
    [Range(0f, 10f)][SerializeField] private float shakeIntensity = 1.5f;
    [Range(0f, 10f)] [SerializeField] private float shakeFrequency = 8f;
    [Range(0f, 10f)][SerializeField] private float shakeDuration = .3f;
    
    private CinemachineVirtualCamera cam;
    private CinemachineBasicMultiChannelPerlin noise;
    
    private float shakeTimer;
    
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        noise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    
    public void ShakeCamera()
    {
        noise.m_AmplitudeGain = shakeIntensity;
        noise.m_FrequencyGain = shakeFrequency;
        shakeTimer = shakeDuration;
    }

    void Update()
    {
        if (shakeTimer < 0f) return;
        shakeTimer -= Time.deltaTime;

        if (shakeTimer >= 0f) return;
        
        noise.m_AmplitudeGain = 0;
    }
}
