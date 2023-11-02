using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShaker : MonoBehaviour
{
    [Range(0f, 10f)][SerializeField] private float shakeIntensity = 1f;
    [Range(0f, 10f)] [SerializeField] private float shakeFrequency = 1f;
    [Range(0f, 10f)][SerializeField] private float shakeDuration = 1f;
    
    private CinemachineVirtualCamera cam;
    private float shakeTimer;
    
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }
    
    [ContextMenu("Shake")]
    public void ShakeCamera()
    {
        var perlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = shakeIntensity;
        perlin.m_FrequencyGain = shakeFrequency;
        shakeTimer = shakeDuration;
    }

    void Update()
    {
        if (shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                ResetCamera();
            }
        }
    }
    
    private void ResetCamera()
    {
        var perlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = 0;
    }
}
