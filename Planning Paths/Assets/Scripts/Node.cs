using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> neighbors = new();
    public float gCost = Mathf.Infinity;
    public float hCost;
    public float FCost { get { return gCost + hCost; } }
    public Node parent;
}
