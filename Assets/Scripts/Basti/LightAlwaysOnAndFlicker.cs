using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightAlwaysOnAndFlicker : MonoBehaviour
{
    private Light2D roomLight;

    private float initialIntensity;

    private float maxInterval = 4f;
    private float maxFlicker = 0.2f;
    private float flickerTimer;
    private float flickerDelay;
    private bool isFlicker;
    private float burstFlickerInterval = 0.6f;
    private int flickerCount;
    private int currentMaxFlickers;
    private int minFlickers = 2;
    private int maxFlickers = 4;


    private void Awake()
    {
        roomLight = GetComponent<Light2D>();
        initialIntensity = roomLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        flickerTimer += Time.deltaTime;
        if (flickerTimer > flickerDelay)
        {
            FlickerLight();
        }
    }
    private void FlickerLight()
    {
        isFlicker = !isFlicker;

        if (isFlicker)
        {
            roomLight.intensity = initialIntensity;
            if (flickerCount == currentMaxFlickers)
            {
                flickerDelay = Random.Range(0, maxInterval);
                flickerCount = 0;
                currentMaxFlickers = Random.Range(minFlickers, maxFlickers);
            }
            else
            {
                flickerDelay = Random.Range(0, burstFlickerInterval);
            }
        }
        else
        {
            roomLight.intensity = Random.Range(0f, initialIntensity - 0.1f);
            flickerDelay = Random.Range(0, maxFlicker);
            flickerCount++;
        }

        flickerTimer = 0;
    }


}
