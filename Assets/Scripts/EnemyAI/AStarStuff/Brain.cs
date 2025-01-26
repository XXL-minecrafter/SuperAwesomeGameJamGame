using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private bool useTargetObject;
    private Vector3 targetPosition = Vector3.zero;

    private Legs legs;

    private NodeGrid nodeGrid;
    public NodeGrid NodeGrid => nodeGrid;
    private WaypointManager waypointManager;

    private List<Node> openNodes;
    private HashSet<Node> closedNodes;

    public bool IsChasing;
    public bool IsRoaming;

    private void Awake()
    {
        nodeGrid = FindObjectOfType<NodeGrid>();
        if (!nodeGrid) Debug.LogWarning("There was no World Grid found in the scene!");

        waypointManager = FindObjectOfType<WaypointManager>();
        if (!waypointManager) Debug.LogWarning("There was no Waypoint Manager found in the scene!");

        legs = GetComponent<Legs>();
    }

    private void Update()
    {
        if (!nodeGrid) return;

        targetPosition = SelectTargetPosition();
        ChangeState();

        FindPath(transform.position, targetPosition);
    }

    private void ChangeState()
    {
        if (!IsRoaming && !IsChasing && Vector2.Distance(transform.position, targetPosition) < 1f) legs.StartRoaming();
    }

    private Vector3 SelectTargetPosition()
    {
        // Use target object as new target position
        if (IsChasing) return player.position;

        else if (Vector2.Distance(transform.position, targetPosition) < 1f)
        {
            // Select random point inside current room
            if (IsRoaming) return legs.CalculateNewWaypoint();

            // Select new room if target reached and enemy is not roaming
            else return waypointManager.FindRandomWaypointPosition();
        }

        return targetPosition;
    }

    private void FindPath(Vector2 startPos, Vector2 endPos)
    {
        Node startNode = nodeGrid.NodeFromWorldPoint(startPos);
        Node endNode = nodeGrid.NodeFromWorldPoint(endPos);

        openNodes = new();
        closedNodes = new();

        openNodes.Add(startNode);

        while (openNodes.Count > 0) //Loop as long as there are still unchecked nodes
        {
            Node currentNode = openNodes[0];

            for (int i = 1; i < openNodes.Count; i++) 
            {
                if (openNodes[i].FCost < currentNode.FCost)
                {
                    currentNode = openNodes[i];
                }
                //Equal FCost -> check closest to end node (H cost)
                else if (openNodes[i].FCost == currentNode.FCost && openNodes[i].HCost < currentNode.HCost)
                {
                    currentNode = openNodes[i];
                }
            }

            openNodes.Remove(currentNode);
            closedNodes.Add(currentNode);

            //End condition - found path
            if (currentNode == endNode) //Retrace path using parents
            {
                RetracePath(startNode, endNode);
            }

            foreach (Node n in nodeGrid.GetNeighbours(currentNode)) 
            {
                if(!n.walkable || closedNodes.Contains(n))
                {
                    continue;   
                }

                int newMovementCostToNeighbour = currentNode.GCost + GetDistanceNode(currentNode, n); 
                if(newMovementCostToNeighbour < n.GCost || !openNodes.Contains(n))
                {
                    n.GCost = newMovementCostToNeighbour;
                    n.HCost = GetDistanceNode(n, endNode);
                    n.parentNode = currentNode;

                    if (!openNodes.Contains(n)) openNodes.Add(n);
                }
            }
        }
    }   


    private void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode) //Retrace steps until start node
        {
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
        }

        path.Reverse(); //Place endNode at end and start at beginning

        nodeGrid.Path = path;
    }
        
    private int GetDistanceNode(Node a, Node b)
    {
        int distanceX = (int) Mathf.Abs(a.gridPosition.x - b.gridPosition.x);
        int distanceY = (int) Mathf.Abs(a.gridPosition.y - b.gridPosition.y);   

        if(distanceX > distanceY)
            return 14 * distanceY + 10 * (distanceX - distanceY);
        else        
            return 14 * distanceX + 10 * (distanceY - distanceX);
    }

    public void OverrideTarget(Vector2 target) => targetPosition = target;

    public Vector2 Next() => nodeGrid.Path.Count > 0 ? nodeGrid.Path[0].worldPosition : transform.position;

    public void ToggleRoaming() => IsRoaming = !IsRoaming;
}
    