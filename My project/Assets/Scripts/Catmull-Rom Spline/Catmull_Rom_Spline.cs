using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Catmull_Rom_Spline : MonoBehaviour
{
    public Transform[] controlPoints;
    public bool isLooping = true;
    [Range(0.0f, 1.0f)] 
    public float tension;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        // Draw spline between the points
        for (int i = 0; i < controlPoints.Length; i++)
        {
            // Skip drawing for endpoints if not looping
            if ((i == 0 || i == controlPoints.Length - 2 || i == controlPoints.Length - 1) && !isLooping)
            {
                continue;
            }

            DisplayCatmullRomSpline(i);
        }
    }

    void DisplayCatmullRomSpline(int pos)
    {
        // The four control points needed to form the spline segment
        Vector3 p0 = controlPoints[ClampListPos(pos - 1)].position;
        Vector3 p1 = controlPoints[pos].position;
        Vector3 p2 = controlPoints[ClampListPos(pos + 1)].position;
        Vector3 p3 = controlPoints[ClampListPos(pos + 2)].position;

        // Starting position of the spline segment
        Vector3 lastPos = p1;

        // Determines how many points along the spline are calculated and drawn.
        float resolution = 0.2f;
        int loops = Mathf.FloorToInt(1.0f / resolution);
        
        for (int i = 0; i <= loops; i++)
        {
            // Calculates the parameter t for the current segment
            float t = i * resolution;

            // Gets the position along the spline
            Vector3 newPos = CamtullCalculations(t, p0, p1, p2, p3);

            // Draws line segment
            Gizmos.DrawLine(lastPos, newPos);

            // Updates the pos
            lastPos = newPos;
        }
    }

    public int ClampListPos(int pos)
    {
        if(pos < 0)
        {
            pos = controlPoints.Length - 1; // Wrap to last index if negative
        }
        
        if(pos >= controlPoints.Length)
        {
            pos = isLooping ? pos % controlPoints.Length : controlPoints.Length - 1;  // Wrap or clamp
        }

        return pos;
    }

    public Vector3 CamtullCalculations(float t, Vector3 p0,  Vector3 p1, Vector3 p2, Vector3 p3)
    {
        // Compute tangents
        Vector3 t0 = (1 - tension) * 0.5f * (p2 - p0);
        Vector3 t1 = (1 - tension) * 0.5f * (p3 - p1);

        // Coefficients for cubic polynomial
        Vector3 a = 2f * p1 - 2f * p2 + t0 + t1;
        Vector3 b = -3f * p1 + 3f * p2 - 2f * t0 - t1;
        Vector3 c = t0;
        Vector3 d = p1;

        // Compute the spline position
        Vector3 pos = a * Mathf.Pow(t, 3) + b * Mathf.Pow(t, 2) + c * t + d;

        return pos;
    }
}
