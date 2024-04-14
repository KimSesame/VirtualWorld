using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ViewType : int
{
    FollowPlayer = 0,
    FisrtPerson = 1,
    Top = 2
}

public class CameraSwitcher : MonoBehaviour
{
    public GameObject player;

    private Vector3[] offset = { new(0, 5, -10), new(0, 2, 1), new(0, 30, 10) };
    private ViewType viewType = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update for check camera type
    void Update()
    {
        // If Left or Right Shift down, change view type
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            viewType = (ViewType)(((int)viewType + 1) % Enum.GetNames(typeof(ViewType)).Length);
    }

    // LateUpdate is called after Update method, which allows to more smoothly follow the player
    void LateUpdate()
    {
        switch (viewType)
        {
            case ViewType.FollowPlayer:
                transform.position = player.transform.position + offset[(int)ViewType.FollowPlayer];
                transform.rotation = player.transform.rotation * Quaternion.Euler(5, 0, 0);
                return;

            case ViewType.FisrtPerson:
                transform.position = player.transform.position + offset[(int)ViewType.FisrtPerson];
                transform.rotation = player.transform.rotation * Quaternion.Euler(5, 0, 0);
                return;

            case ViewType.Top:
                transform.position = player.transform.position + offset[(int)ViewType.Top];
                transform.rotation = player.transform.rotation * Quaternion.Euler(90, 0, 0);
                return;

            default:
                return;
        }

    }
}
