using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyFlashlight : MonoBehaviour
{
    private Light2D flashLight;

    private Vector3[] flashPointArray;

    private Vector3 lightPointFrontLeft;
    private Vector3 lightPointFrontRight;


    private void Awake()
    {
        flashLight = GetComponent<Light2D>();

        flashPointArray = flashLight.shapePath; //0 is left / 2 is right
        lightPointFrontLeft = flashPointArray[0];
        lightPointFrontRight = flashPointArray[2];

        flashLight.shapePath[0] = lightPointFrontLeft;
    }

    private void Update()
    {
        flashLight.shapePath[0].x += 0.1f;
        flashLight.shapePath[2].x += 0.1f;
    }

}
