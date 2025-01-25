using System;
using UnityEngine;

public enum SpawnPointObject
{
    VendingMachine,
    Coin,
    GumPlacePosition
}

public class SpawnPoint : MonoBehaviour
{
    [field: SerializeField] public SpawnPointObject SpawnPointObject { get; private set; }

    [field: SerializeField] public bool HasObject { get; set; } //Checks whether or not an object is at the spawn point -> don't spawn other object

    private static Action<SpawnPoint> onObjectCleared;

    public static event Action<SpawnPoint> OnObjectCleared { add =>  onObjectCleared += value; remove => onObjectCleared -= value; }

    public void RemoveSpawnPointObject() => onObjectCleared?.Invoke(this);
}
