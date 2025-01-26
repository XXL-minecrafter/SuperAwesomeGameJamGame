using System.Collections.Generic;
using UnityEngine;

public class NodeGrid : MonoBehaviour
{
    public LayerMask unwalkableMask;
    public Node[,] nodeGrid;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    [SerializeField] public List<Node> Path;

    private float nodeDiameter;
    private int gridSizeX, gridSizeY;


    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        CreateNodeGrid();
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));

        float smallSubtraction = 0.1f; //For better visualization

        if (nodeGrid != null)
        {
            foreach (var node in nodeGrid)
            {
                Gizmos.color = node.walkable ? Color.green : Color.red;
                if (Path != null && Path.Contains(node))
                    Gizmos.color = Color.blue;
                Gizmos.DrawWireCube(node.worldPosition, Vector3.one * (nodeDiameter - smallSubtraction));
            }
        }
    }

    public Node NodeFromWorldPoint(Vector2 worldPosition)
    {
        float percentX = (worldPosition.x + gridSizeX / 2) / gridSizeX;
        float percentY = (worldPosition.y + gridSizeY / 2) / gridSizeY;

        //float percentX = worldPosition.x + gridSizeX 

        percentX = Mathf.Clamp01(percentX); //Handle outside coordinates (Out of Bounds exception)
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX); //0 based
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY); //0 based

        return nodeGrid[y, x];
    }

    //Use list instead of array, since neighbours may vary depending on node position (E.g. corner)
    public List<Node> GetNeighbours(Node forNode)
    {
        List<Node> neighbours = new();

        //3x3 search
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue; //Don't check forNode itself

                int checkXPos = (int)forNode.gridPosition.x + x;
                int checkYPos = (int)forNode.gridPosition.y + y;

                if (checkXPos < 0 || checkXPos >= gridSizeX) continue;
                if (checkYPos < 0 || checkYPos >= gridSizeY) continue;

                neighbours.Add(nodeGrid[checkYPos, checkXPos]);
            }
        }

        return neighbours;
    }

    private void CreateNodeGrid()
    {
        nodeGrid = new Node[gridSizeX, gridSizeY];

        float worldLeftPos = transform.position.x - gridWorldSize.x / 2f;
        float worldBottomPos = transform.position.y - gridWorldSize.y / 2f;

        Vector2 bottomLeftCorner = new Vector2(worldLeftPos, worldBottomPos);

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPoint =
                    bottomLeftCorner + new Vector2(x * nodeDiameter + nodeRadius, y * nodeDiameter + nodeRadius);

                bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, unwalkableMask));

                nodeGrid[y, x] = new Node(walkable, worldPoint, new Vector2(x, y));
            }
        }
    }
}
