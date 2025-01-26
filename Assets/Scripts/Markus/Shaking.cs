using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class Shaking : MonoBehaviour
{
    [SerializeField][Range(0f, 1f)] private float shakeAmount = 0.1f; // Schüttel-Stärke
    [SerializeField] public float setShakeDuration; // Dauer des Schüttelns
    private Vector3 originalPosition;  // Originalposition des Objektes

    private void OnEnable()
    {
        VendingMachineBehaviour.OnInteract += Shake;
    }
    private void OnDisable()
    {
        VendingMachineBehaviour.OnInteract -= Shake;
    }
    private void Start()
    {
        originalPosition = transform.position; // Originalposition des Sprites merken damit man es wieder herstellen
    }

    private void Shake()
    {
        StartCoroutine(ShakeMachine());
    }
    private IEnumerator ShakeMachine()
    {
        float shakeDuration = setShakeDuration;
        while (shakeDuration > 0)
        {
            float shakeX = Random.Range(-shakeAmount, shakeAmount);
            float shakeY = Random.Range(-shakeAmount, shakeAmount);

            transform.position = new Vector3(originalPosition.x + shakeX, originalPosition.y + shakeY, originalPosition.z);

            // Zurück zur Originalposition
            yield return null;
            shakeDuration -= Time.deltaTime;
        }
        // Dauer verringern
        transform.position = originalPosition;
    }
} // End public class Shaking : MonoBehaviour
