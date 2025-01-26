using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roam : States
{
    public Roam(Brain brain, Legs legs, Eyes eyes) : base(brain, legs, eyes)
    {
    }

    public override Vector2 SelectTarget() => legs.CalculateNewWaypoint();
}
