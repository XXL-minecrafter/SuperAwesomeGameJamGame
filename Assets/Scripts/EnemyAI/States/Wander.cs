using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Wander : State
{
    private float movementRange;

    public Wander(EnemyController enemy, NavMesh2D navMesh, float movementRange) : base(enemy, navMesh)
    {
        this.movementRange = movementRange;
    }

    public override void OnFinalize()
    {
        throw new System.NotImplementedException();
    }

    public override void OnStart()
    {
        throw new System.NotImplementedException();
    }

    public override void OnUpdate()
    {
        enemy.transform.position = CalculateNewWaypoint();
    }

    public Vector2 CalculateNewWaypoint()
    {
        for (int i = 0; i < 1; i++)
        {
            // Get random point around entity in range
            var randomPointAroundEntity = Random.insideUnitCircle * movementRange + (Vector2)enemy.transform.position;

            // Check if random point is on NavMesh
            bool isPointOnMesh = navMesh.Mesh.OverlapPoint(randomPointAroundEntity);

            Debug.Log($"{randomPointAroundEntity} : {isPointOnMesh}");

            if (isPointOnMesh)
            {
                Debug.Log($"{GetType()}: Waypoint of {enemy.name} set to {randomPointAroundEntity}.");
                return randomPointAroundEntity;
            }
        }

        Debug.LogWarning($"{GetType()}: Waypoint of {enemy.name} could not be calculated.");
        return enemy.transform.position;
    }
}
