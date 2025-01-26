using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class States
{
    protected Brain brain;
    protected Legs legs;
    protected Eyes eyes;

    protected States(Brain brain, Legs legs, Eyes eyes)
    {
        this.brain = brain;
        this.legs = legs;
        this.eyes = eyes;
    }

    protected virtual void OnStart()
    {
    }

    protected virtual void OnUpdate()
    {
    }

    protected virtual void OnEnd()
    {
    }

    public abstract Vector2 SelectTarget();
}
