using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RoomLightSwitch : MonoBehaviour
{
    private Light2D roomLight;

    #region LightValues
    private float fadeTime = 1.5f;
    private float newIntensity = 0;
    private float initialIntensity;
    private float falloffDistance;
    private int colliderCount = 0;
    private float turnOffTimer = 0f;
    private float turnOffDelay = 2f;
    private bool isDelay = false;
    private bool isOn = false;
    #endregion

    #region Flicker
    private float maxInterval = 10f;
    private float maxFlicker = 0.2f;
    private float flickerTimer;
    private float flickerDelay;
    private bool isFlicker;
    private float burstFlickerInterval = 0.6f;
    private int flickerCount;
    private int currentMaxFlickers;
    private int minFlickers = 2;
    private int maxFlickers = 4;
    public bool CanFlicker = true;
    #endregion

    //Max values so light doesn't escalate
    private float maxIntensity = 0.5f;
    private float maxFalloffDistance = 1.0f;


    private void Awake()
    {
        roomLight = GetComponent<Light2D>();
        initialIntensity = roomLight.intensity;
        falloffDistance = roomLight.falloffIntensity;
        roomLight.enabled = true;
        roomLight.intensity = 0f;

        CheckMaxValues();
    }

    private void Update()
    {
        if (!isDelay)
        {
            roomLight.intensity = Mathf.MoveTowards(roomLight.intensity, newIntensity, fadeTime * Time.deltaTime);
        }
        else
        {
            turnOffTimer -= Time.deltaTime;
            if (turnOffTimer <= 0) isDelay = false;
        }

        if (isOn && CanFlicker)
        {
            flickerTimer += Time.deltaTime;
            if (flickerTimer > flickerDelay)
            {
                FlickerLight();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isOn = true;
            colliderCount++;
            newIntensity = initialIntensity;
            flickerDelay = 1f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            colliderCount--;

            if (colliderCount == 0)
            {
                if (roomLight.intensity < initialIntensity)
                {
                    StartCoroutine(DelayTurnOff());
                }
                else
                {
                    isDelay = true;
                    turnOffTimer = turnOffDelay;
                    newIntensity = 0;
                    isOn = false;
                }
            }
        }
    }

    /// <summary>
    /// Delays the Light turn off
    /// </summary>
    /// <returns></returns>
    private IEnumerator DelayTurnOff()
    {
        yield return new WaitForSeconds(turnOffDelay);

        isDelay = false;
        turnOffTimer = turnOffDelay;
        newIntensity = 0;
        isOn=false;
    }

    /// <summary>
    /// Flickers the Light in bursts
    /// </summary>
    private void FlickerLight()
    {
        isFlicker = !isFlicker;

        if (isFlicker)
        {
            roomLight.intensity = initialIntensity;
            if(flickerCount == currentMaxFlickers)
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
            roomLight.intensity = Random.Range(0f, initialIntensity -0.1f);
            flickerDelay = Random.Range(0, maxFlicker);
            flickerCount++;
        }

        flickerTimer = 0;
    }

    /// <summary>
    /// Save feature if someone fucked up creating the lights (probably me, Basti...)
    /// </summary>
    private void CheckMaxValues()
    {
        if (newIntensity > maxIntensity)
        {
            newIntensity = maxIntensity;
            initialIntensity = newIntensity;
        }

        if (falloffDistance > maxFalloffDistance)
        {
            falloffDistance = maxFalloffDistance;
        }
    }
}
