using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Wander : MonoBehaviour
{
    [SerializeField] private float movementRange = 1f;
    [SerializeField] private LayerMask blockingLayer;

    private Vector2 destination;

    private void Start()
    {
        StartCoroutine(MoveTo());
    }

    private IEnumerator MoveTo()
    {
        const float distanceMargin = .1f;

        while (true)
        {
            destination = CalculateNewWaypoint();

            // TODO: Needs to rotate too

            while (Vector2.Distance(transform.position, destination) >= distanceMargin)
            {
                transform.position += ((Vector3)destination - transform.position).normalized * Time.deltaTime;
                

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
[CustomEditor(typeof(Wander))]
public class WanderEditor : Editor
{
    private Wander wander;
    private Vector2 overrideDest;

    private void OnEnable()
    {
        wander = (Wander)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space(100);

        overrideDest = EditorGUILayout.Vector2Field("New Destination", overrideDest);
        if (GUILayout.Button("Override Destination")) wander.OverrideDestination(overrideDest);
    }
}
#endif
