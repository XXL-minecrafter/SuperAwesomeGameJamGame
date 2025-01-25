using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class Legs : MonoBehaviour
{
    [SerializeField] private float movementRange = 1f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private LayerMask blockingLayer;

    private Collider2D enemyCollider;
    private float enemyRadius;
    private Vector2 destination;

    private void Awake()
    {
        enemyCollider = GetComponent<Collider2D>();
        enemyRadius = enemyCollider.bounds.extents.x;
    }

    private void Start()
    {
        StartCoroutine(MoveTo());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(destination, radius: .1f);
    }

    private IEnumerator MoveTo()
    {
        const float maxRotationDelta = 1f;

        while (true)
        {
            destination = CalculateNewWaypoint();

            while (Vector2.Distance(transform.position, destination) >= enemyRadius)
            {
                transform.position += ((Vector3)destination - transform.position).normalized * Time.deltaTime;

                var currentRotation = transform.right;
                var targetRotation = (Vector3)destination - transform.position;
                transform.right = Vector3.RotateTowards(currentRotation, targetRotation, rotationSpeed * Time.deltaTime, maxRotationDelta);

                yield return null;
            }
        }
    }

    public Vector2 CalculateNewWaypoint()
    {
        var randomPosition = Random.insideUnitCircle * movementRange;

        var hit = Physics2D.Raycast(transform.position, randomPosition, Vector2.Distance(transform.position, randomPosition), blockingLayer);

        if (hit.collider) return CalculateNewWaypoint();
        else return randomPosition;
    }

    public void OverrideDestination(Vector2 destination) => this.destination = destination;
}

#if UNITY_EDITOR
[CustomEditor(typeof(Legs))]
public class LegsEditor : Editor
{
    private Legs wander;
    private Vector2 overrideDest;

    private void OnEnable()
    {
        wander = (Legs)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Separator();

        overrideDest = EditorGUILayout.Vector2Field("New Destination", overrideDest);
        if (GUILayout.Button("Override Destination")) wander.OverrideDestination(overrideDest);
    }
}
#endif
