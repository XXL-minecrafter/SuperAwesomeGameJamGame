using UnityEngine;

public class Detector : MonoBehaviour
{
    private float viewDistance;

    private Color color = Color.green;

    private bool hasHitPlayer;

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward * viewDistance, color);
    }

    /// <summary>
    /// Shoots a ray straight out. If the player collides with the ray, the color of the ray changes and the method returns true. Otherwise false.
    /// </summary>
    /// <param name="hit">Information about the hit object</param>
    /// <returns>True, if an object with the tag "Player" was hit. Otherwise false.</returns>
    public bool TryDetectPlayer(out RaycastHit hit)
    {
        _ = Physics.Raycast(transform.position, transform.forward.normalized, out var hitObject, viewDistance);

        if (hitObject.collider) hasHitPlayer = hitObject.transform.CompareTag("Player");
        else hasHitPlayer = false;

        if (hasHitPlayer) color = Color.red;
        else color = Color.green;

        hit = hitObject;
        return hasHitPlayer;
    }

    /// <summary>
    /// Sets the distance of the rays
    /// </summary>
    public void SetViewDistance(float viewDistance) => this.viewDistance = viewDistance;
}
