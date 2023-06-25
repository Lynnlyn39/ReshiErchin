using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_DayNightCycle : MonoBehaviour
{

    public int rotationScale = 5;


    void Update()
    {
        transform.Rotate(rotationScale * Time.deltaTime, 0, 0);
    }
}
