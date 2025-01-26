using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : States
{
    private Transform player;

    public Chase(Brain brain, Legs legs, Eyes eyes, Transform player) : base(brain, legs, eyes)
    {
        this.player = player;
    }

    public override Vector2 SelectTarget() => player.position;
}
