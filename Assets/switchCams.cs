using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchCams : MonoBehaviour
{

    public Camera camera1;
    public Camera camera2;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            camera1.enabled = false;
            camera2.enabled = true;

        }
        else
        {
            camera2.enabled = false;
            camera1.enabled = true;
        }
    }
}
