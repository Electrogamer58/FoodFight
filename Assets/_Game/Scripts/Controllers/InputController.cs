using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputController : MonoBehaviour
{
    public event Action PressedConfirm = delegate { };
    public event Action PressedCancel = delegate { };
    public event Action PressedLeft = delegate { };
    public event Action PressedRight = delegate { };
    public event Action PressedP = delegate { };

    public bool inputEnabled = true;

    // Update is called once per frame
    void Update()
    {
        if (inputEnabled)
        {
            DetectConfirm();
            DetectCancel();
            DetectLeft();
            DetectRight();
            DetectPause();
        }
    }

    private void DetectRight()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            PressedRight?.Invoke();
        }
    }

    private void DetectLeft()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PressedLeft?.Invoke();
        }
    }

    private void DetectCancel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PressedCancel?.Invoke();
        }
    }

    private void DetectConfirm()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            PressedConfirm?.Invoke();
        }
    }

    private void DetectPause()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PressedP?.Invoke();
        }
    }
}
