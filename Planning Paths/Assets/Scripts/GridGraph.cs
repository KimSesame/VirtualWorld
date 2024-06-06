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
    }

    void Start()
    {
        GenerateGrid();
        ConnectNodes();
    }

    void GenerateGrid()
    {
        nodes = new Node[gridSize, gridSize];

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                float xPos = startCoordinate + (x * nodeSpacing);
                float yPos = startCoordinate + (y * nodeSpacing);
                Vector3 position = new(xPos, 0, yPos);

                GameObject nodeObject = Instantiate(nodePrefab, position, Quaternion.identity);
                nodeObject.transform.parent = transform;
                nodes[x, y] = nodeObject.GetComponent<Node>();
            }
        }
    }

    void ConnectNodes()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Node currentNode = nodes[x, y];

                if (x != gridSize - 1)
                    currentNode.neighbors.Add(nodes[x + 1, y]);
                if (y != gridSize - 1)
                    currentNode.neighbors.Add(nodes[x, y + 1]);
                if (x != 0)
                    currentNode.neighbors.Add(nodes[x - 1, y]);
                if (y != 0)
                    currentNode.neighbors.Add(nodes[x, y - 1]);
            }
        }
    }

    public Node GetNode(int x, int y)
    {
        return nodes[x, y];
    }
}

