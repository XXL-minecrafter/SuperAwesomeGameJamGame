using UnityEngine;

public class Shaking : MonoBehaviour
{
    [SerializeField][Range(0f,1f)] private float shakeAmount = 0.1f; // Schüttel-Stärke
    [SerializeField] public float shakeDuration = 0.5f; // Dauer des Schüttelns
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
        // Dauer der Animations
        if (shakeDuration > 0)
        {
            float shakeX = Random.Range(-shakeAmount, shakeAmount);
            float shakeY = Random.Range(-shakeAmount, shakeAmount);

            transform.position = new Vector3(originalPosition.x + shakeX, originalPosition.y + shakeY, originalPosition.z);

            shakeDuration -= Time.deltaTime; // Dauer verringern
        }
        else
        {
            transform.position = originalPosition; // Zurück zur Originalposition
        }
    }
} // End public class Shaking : MonoBehaviour
