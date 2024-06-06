using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGraph : MonoBehaviour
{
    public static GridGraph inst = null;

    public int gridSize = 9;
    public int startCoordinate = -4;
    public int nodeSpacing = 1;
    public int[] startNodeIndex;
    public int[] goalNodeIndex;
    public GameObject nodePrefab;

    private Node[,] nodes;

    void Awake()
    {
        inst = this;
        GenerateGrid();
        ConnectNodes();
    }

    void Start()
    {
    }

    void GenerateGrid()
    {
        nodes = new Node[gridSize, gridSize];

        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                float xPos = startCoordinate + (x * nodeSpacing);
                float yPos = startCoordinate + (z * nodeSpacing);
                Vector3 position = new(xPos, 0, yPos);

                GameObject nodeObject = Instantiate(nodePrefab, position, Quaternion.identity);
                nodeObject.transform.parent = transform;
                nodes[x, z] = nodeObject.GetComponent<Node>();
            }
        }
    }

    void ConnectNodes()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                Node currentNode = nodes[x, z];

                if (x != gridSize - 1)
                    currentNode.neighbors.Add(nodes[x + 1, z]);
                if (z != gridSize - 1)
                    currentNode.neighbors.Add(nodes[x, z + 1]);
                if (x != 0)
                    currentNode.neighbors.Add(nodes[x - 1, z]);
                if (z != 0)
                    currentNode.neighbors.Add(nodes[x, z - 1]);
            }
        }
    }

    public void RemoveNode(int x, int z)
    {
        if (x < 0 || x >= gridSize || z < 0 || z >= gridSize)
        {
            Debug.LogError("Node coordinates out of range");
            return;
        }

        Node nodeToRemove = nodes[x, z];

        if (nodeToRemove == null)
        {
            Debug.LogError("Node already removed");
            return;
        }

        // Remove edge from node to remove
        foreach (Node neighbor in nodeToRemove.neighbors)
        {
            neighbor.neighbors.Remove(nodeToRemove);
        }

        // Destroy the node game object
        Destroy(nodeToRemove.gameObject);

        // Remove the node from the grid
        nodes[x, z] = null;
    }

    public Node GetNode(int x, int z)
    {
        return nodes[x, z];
    }
}

