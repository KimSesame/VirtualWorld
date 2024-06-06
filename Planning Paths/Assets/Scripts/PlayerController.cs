using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private List<Node> path;
    private int targetIndex;
    private Vector3[] catmullRomPoints;
    private bool isPathSet = false;

    void Update()
    {
        if (!isPathSet) return;

        // Target the node to move in path
        if (Vector3.Distance(transform.position, catmullRomPoints[targetIndex]) < 0.1f)
        {
            targetIndex++;
            if (targetIndex >= catmullRomPoints.Length)
            {
                isPathSet = false;
                return;
            }
        }

        Vector3 targetPosition = catmullRomPoints[targetIndex];
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Move
        transform.SetPositionAndRotation(Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime), Quaternion.LookRotation(direction));
    }

    public void SetPath(List<Node> path)
    {
        this.path = path;
        if (path.Count > 1)
        {
            GenerateCatmullRomPoints();
            targetIndex = 0;
            isPathSet = true;
        }
    }

    void GenerateCatmullRomPoints()
    {
        List<Vector3> points = new();
        for (int i = 0; i < path.Count; i++)
        {
            points.Add(path[i].transform.position);
        }
        // 추가 노드를 추가하여 경로의 시작과 끝을 다듬는다.
        points.Insert(0, points[0]);
        points.Add(points[points.Count - 1]);

        List<Vector3> catmullRomPointsList = new();

        for (int i = 0; i < points.Count - 3; i++)
        {
            for (float t = 0; t <= 1; t += 0.1f)
            {
                catmullRomPointsList.Add(CatmullRom(points[i], points[i + 1], points[i + 2], points[i + 3], t));
            }
        }
        catmullRomPoints = catmullRomPointsList.ToArray();
    }

    Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 a = 2f * p1;
        Vector3 b = p2 - p0;
        Vector3 c = 2f * p0 - 5f * p1 + 4f * p2 - p3;
        Vector3 d = -p0 + 3f * p1 - 3f * p2 + p3;

        return 0.5f * (a + (t * b) + (t * t * c) + (t * t * t * d));
    }
}