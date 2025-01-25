using UnityEngine;

[System.Serializable]
public class Node
{
    public bool walkable;
    public Vector2 worldPosition;
    public Vector2 gridPosition;

    public Node parentNode;

    public int GCost;
    public int HCost;
    public int FCost => GCost + HCost;

    public Node(bool walkable, Vector2 worldPosition, Vector2 gridPosition)
    {
        this.walkable = walkable;
        this.worldPosition = worldPosition;
        this.gridPosition = gridPosition;
    }
}
