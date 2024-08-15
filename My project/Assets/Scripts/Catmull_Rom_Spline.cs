using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catmull_Rom_Spline : MonoBehaviour
{
    [SerializeField] Transform[] controlPoints;
    [SerializeField] Transform visualPoint;
    [SerializeField] float speed;
    [SerializeField, Range(0, 1)] float tension = 0.0f;

    private float t = 0.0f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    Vector3 CalculateCatmullRomPosition(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float tension)
    {
        float t2 = t * t;
        float t3 = t2 * t;

        float a = -tension * t + 2 * tension * t2 - tension * t3;
    }
}
