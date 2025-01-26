using System.Collections;
using UnityEngine;
using UnityEditor;

public class Legs : MonoBehaviour
{
    [SerializeField] private float movementRange = 1f;
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float stunDuration = 5f;
    [SerializeField] private float roamingDuration = 20f;
    [SerializeField] private LayerMask blockingLayer;

    private Brain brain;
    private Collider2D enemyCollider;

    private Vector2 destination;
    private bool freezeMovement;

    private void Awake()
    {
        brain = GetComponent<Brain>();
        enemyCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        destination = brain.Next();

        // if the enemy partrols, use CalculateNewWaypoint() and pass it in Brain.FindPath()
        // else get a random waypoint from the waypoint class and pass it in Brain.FindPath()
    }
    private void OnEnable()
    {
        PlayerCaught.OnPlayerCaught += ToggleFreeze;
        CatchTransitionscript.OnFullBlackScreen += ToggleFreeze;
    }
    private void OnDisable()
    {
        PlayerCaught.OnPlayerCaught -= ToggleFreeze;
        CatchTransitionscript.OnFullBlackScreen -= ToggleFreeze;
    }

    private void Update()
    {
        if (freezeMovement) return;
        Move();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(destination, radius: .1f);
    }

    private void Move()
    {
        if (Vector2.Distance(transform.position, destination) >= .01f)
        {
            transform.position += ((Vector3)destination - transform.position).normalized * Time.deltaTime * movementSpeed;

            var currentRotation = transform.right;
            var targetRotation = (Vector3)destination - transform.position;
            transform.right = Vector3.RotateTowards(currentRotation, targetRotation, rotationSpeed * Time.deltaTime, 1f);

            return;
        }        
        else destination = brain.Next();
    }

    public void StartRoaming(System.Func<Vector2> callback = null) => StartCoroutine(RoamingCO(callback));

    private IEnumerator RoamingCO(System.Func<Vector2> callback)
    {
        brain.ToggleRoaming();

        yield return new WaitForSeconds(roamingDuration);

        brain.ToggleRoaming();
    }

    public void ToggleFreeze() => freezeMovement = !freezeMovement;

    public Vector2 CalculateNewWaypoint()
    {
        var randomPosition = Random.insideUnitCircle * movementRange;

        //var hit = Physics2D.Raycast(transform.position, randomPosition, Vector2.Distance(transform.position, randomPosition), blockingLayer);
        var hit = Physics2D.Linecast(transform.position, randomPosition, blockingLayer);
        
        if (hit.collider || !brain.NodeGrid.NodeFromWorldPoint(randomPosition).walkable) return CalculateNewWaypoint();
        else return randomPosition;
    }

    public void OverrideDestination(Vector2 destination) => brain.OverrideTarget(destination);

    public void Stun()
    {
        StartCoroutine(StunTimerCO());
    }

    private IEnumerator StunTimerCO()
    {
        ToggleFreeze();

        yield return new WaitForSeconds(stunDuration);

        ToggleFreeze();
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
