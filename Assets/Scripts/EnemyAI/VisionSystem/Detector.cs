using UnityEngine;

public class Detector : MonoBehaviour
{
    private float viewDistance;

    private Color color = Color.green;

    private void OnDrawGizmos() => Debug.DrawRay(transform.position, transform.forward.normalized * viewDistance, color);

    /// <summary>
    /// Shoots a ray straight out. If the player collides with the ray, the color of the ray changes and the method returns true. Otherwise false.
    /// </summary>
    /// <param name="hit">Information about the hit object</param>
    /// <returns>True, if an object with the tag "Player" was hit. Otherwise false.</returns>
    public bool TryDetectLayer(LayerMask layer, out RaycastHit2D hit)
    {
        var hitObject = Physics2D.Raycast(transform.position, transform.forward.normalized, viewDistance, layer);
        
        color = hitObject.collider ? Color.red : Color.green;

        hit = hitObject;
        return hitObject.collider ? true: false;
    }

    /// <summary>
    /// Sets the distance of the rays
    /// </summary>
    public void SetViewDistance(float viewDistance) => this.viewDistance = viewDistance;
}
