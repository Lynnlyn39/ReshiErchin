using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_DayNightCycle : MonoBehaviour
{

    public float rotationScale = 0.25f;


    void Update()
    {
        transform.Rotate(rotationScale * Time.deltaTime, 0, 0);
    }
}
