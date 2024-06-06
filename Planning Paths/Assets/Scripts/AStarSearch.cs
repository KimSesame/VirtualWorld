using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarSearch : MonoBehaviour
{
    private Node startNode;
    private Node goalNode;

    void Start()
    {
        startNode = GridGraph.inst.GetNode(GridGraph.inst.startNodeIndex[0], GridGraph.inst.startNodeIndex[1]);
        goalNode = GridGraph.inst.GetNode(GridGraph.inst.goalNodeIndex[0], GridGraph.inst.goalNodeIndex[1]);

        AStar();
    }

    public void AStar()
    {
        List<Node> openSet = new();
        HashSet<Node> closedSet = new();

        startNode.gCost = 0;
        startNode.hCost = Vector3.Distance(startNode.transform.position, goalNode.transform.position);
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FCost < currentNode.FCost || openSet[i].FCost == currentNode.FCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == goalNode)
            {
                RetracePath(startNode, goalNode);
                Debug.Log("Goal found!");
                return;
            }

            foreach (Node neighbor in currentNode.neighbors)
            {
                if (closedSet.Contains(neighbor))
                    continue;

                float newCostToNeighbor = currentNode.gCost + 1;
                if (newCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newCostToNeighbor;
                    neighbor.hCost = Vector3.Distance(neighbor.transform.position, goalNode.transform.position);
                    neighbor.parent = currentNode;

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        Debug.Log("Goal not found!");
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        // 경로 시각화 (선택 사항)
        foreach (Node node in path)
        {
            Debug.Log("Path: " + node.transform.position);
        }
    }
}