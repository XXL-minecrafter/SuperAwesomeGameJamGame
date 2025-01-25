using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class NavMesh2D : MonoBehaviour
{
    public PolygonCollider2D Mesh => mesh;

    private PolygonCollider2D mesh;

    private void Awake()
    {
        mesh = GetComponent<PolygonCollider2D>();
    }
}
