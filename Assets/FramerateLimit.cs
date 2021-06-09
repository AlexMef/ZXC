using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramerateLimit : MonoBehaviour
{
    
    private int target = 60;
    private void Awake()
    {
        Application.targetFrameRate = target;
        QualitySettings.vSyncCount = 2;
    }

    private void Update()
    {
        if (Application.targetFrameRate != target)
        {
            Application.targetFrameRate = target;
        }
    }
}
