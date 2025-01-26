using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Priority { High, Medium, Low }

[System.Serializable]
public class Waypoint
{
    [field: SerializeField] public GameObject WaypointObject { get; private set; }
    [field: SerializeField] public Priority WaypointPriority { get; private set; }
}

public class WaypointManager : MonoBehaviour
{
    [SerializeField] private List<Waypoint> wayPoints = new();

    [SerializeField] private float highPriorityChance = .5f;
    [SerializeField] private float mediumPriorityChance = .3f;
    [SerializeField] private float lowPriorityChance = .2f;

    public Vector2 FindRandomWaypointPosition()
    {
        float randomChance = Random.Range(0f, 1f);

        Waypoint foundWaypoint;

        if(randomChance <= lowPriorityChance)
        {
            foundWaypoint = FindPriorityWaypoint(Priority.Low);
        }
        else if (randomChance > lowPriorityChance && randomChance < lowPriorityChance + mediumPriorityChance)
        {
            foundWaypoint = FindPriorityWaypoint(Priority.Medium);
        }
        else
        {
            foundWaypoint = FindPriorityWaypoint(Priority.High);
        }

        if (foundWaypoint == null) return Vector2.zero;
        else return foundWaypoint.WaypointObject.transform.position;
    }

    private Waypoint FindPriorityWaypoint(Priority priority)
    {
        List<Waypoint> priorityWaypoints = wayPoints.Where(x => x.WaypointPriority == priority).ToList();

        if (priorityWaypoints.Count == 0) return default;

        int randomIndex = Random.Range(0, priorityWaypoints.Count);

        return priorityWaypoints[randomIndex];
    }
}
