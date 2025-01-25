using System.Collections;
using UnityEngine;
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Legs : MonoBehaviour
{
    [SerializeField] private float movementRange = 1f;
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float stunDuration = 5f;
    [SerializeField] private LayerMask blockingLayer;

    private Collider2D enemyCollider;
    private float enemyRadius;
    private Vector2 destination;

    private Coroutine movementOperation;

    private void Awake()
    {
        enemyCollider = GetComponent<Collider2D>();
        enemyRadius = enemyCollider.bounds.extents.x;
    }

    private void Start()
    {
        movementOperation = StartCoroutine(MoveTo());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(destination, radius: .1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    private IEnumerator MoveTo()
    {
        const float maxRotationDelta = 1f;

        while (true)
        {
            destination = CalculateNewWaypoint();

            while (Vector2.Distance(transform.position, destination) >= enemyRadius)
            {
                transform.position += ((Vector3)destination - transform.position).normalized * Time.deltaTime * movementSpeed;

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

        //var hit = Physics2D.Raycast(transform.position, randomPosition, Vector2.Distance(transform.position, randomPosition), blockingLayer);
        var hit = Physics2D.Linecast(transform.position, randomPosition, blockingLayer);

        if (hit.collider) return CalculateNewWaypoint();
        else return randomPosition;
    }

    public void OverrideDestination(Vector2 destination) => this.destination = destination;

    public void Stun()
    {
        StartCoroutine(StunTimerCO());
    }

    private IEnumerator StunTimerCO()
    {
        StopCoroutine(movementOperation);

        yield return new WaitForSeconds(stunDuration);

        movementOperation = StartCoroutine(MoveTo());
    }
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
        if (GUILayout.Button("Stun")) wander.Stun();
    }
}
#endif
