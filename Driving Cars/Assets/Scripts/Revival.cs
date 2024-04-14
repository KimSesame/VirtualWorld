using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revival : MonoBehaviour
{
    public int yLimit = -30;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < yLimit)
        {
            transform.position = new Vector3(0, 0, 0);
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }
}
