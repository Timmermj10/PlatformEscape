using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResolution : MonoBehaviour
{
    void Start()
    {
        // Set the resolution to 960 x 960 and make the game run in windowed mode.
        Screen.SetResolution(960, 960, false);
    }
}
