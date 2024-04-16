using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ViewType : int
{
    FollowPlayer = 0,
    FirstPerson = 1,
    Top = 2
}

public class CameraSwitcher : MonoBehaviour
{
    public GameObject player;

    private Vector3[] offset = { new(0, 5, -10), new(0, 2, 1), new(0, 30, 10) };
    private ViewType viewType = ViewType.FollowPlayer;  // set follow player as default view

    // Update for check camera type
    void Update()
    {
        // If Right Shift down, change view to next
        if (Input.GetKeyDown(KeyCode.RightShift))
            viewType = (ViewType)(((int)viewType + 1) % Enum.GetNames(typeof(ViewType)).Length);
        // If Left Shift down, change view to previous
        else if (Input.GetKeyDown(KeyCode.LeftShift))
            viewType = (ViewType)(((int)viewType + Enum.GetNames(typeof(ViewType)).Length - 1) % Enum.GetNames(typeof(ViewType)).Length);
    }

    // LateUpdate is called after Update method, which allows to more smoothly follow the player
    void LateUpdate()
    {
        switch (viewType)
        {
            // following player view
            case ViewType.FollowPlayer:
                transform.position = player.transform.TransformPoint(offset[(int)ViewType.FollowPlayer]);
                transform.rotation = player.transform.rotation * Quaternion.Euler(5, 0, 0);
                return;

            // FPS view
            case ViewType.FirstPerson:
                transform.position = player.transform.TransformPoint(offset[(int)ViewType.FirstPerson]);
                transform.rotation = player.transform.rotation * Quaternion.Euler(5, 0, 0);
                return;

            // TOP view
            case ViewType.Top:
                transform.position = player.transform.TransformPoint(offset[(int)ViewType.Top]);
                transform.rotation = player.transform.rotation * Quaternion.Euler(90, 0, 0);
                return;

            default:
                return;
        }
    }
}
