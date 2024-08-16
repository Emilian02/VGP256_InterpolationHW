using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineFollower : MonoBehaviour
{
    public Catmull_Rom_Spline spline;
    public float speed = 0.5f; 

    private float t = 0f; // Parameter for the spline position (0 to 1)

    void Update()
    {
        if (spline == null || spline.controlPoints.Length < 4)
            return;

        // Move along the spline
        t += Time.deltaTime * speed;

        // Wrap t to create a looping effect
        if (t > 1f)
        {
            t -= Mathf.Floor(t); // Wrap around
        }

        // Update the position of the GameObject
        Vector3 position = GetSplinePosition(t);
        transform.position = position;
    }

    Vector3 GetSplinePosition(float t)
    {
        int numPoints = spline.controlPoints.Length;

        // Compute the segment index
        float scaledT = t * (numPoints - (spline.isLooping ? 0 : 3));
        int segmentIndex = Mathf.FloorToInt(scaledT);

        // Handle looping
        if (spline.isLooping)
        {
            segmentIndex %= numPoints;
        }
        else
        {
            segmentIndex = Mathf.Clamp(segmentIndex, 0, numPoints - 4);
        }

        // Get the control points for this segment
        int p0Index = spline.ClampListPos(segmentIndex - 1);
        int p1Index = spline.ClampListPos(segmentIndex);
        int p2Index = spline.ClampListPos(segmentIndex + 1);
        int p3Index = spline.ClampListPos(segmentIndex + 2);

        // Compute the parameter for the current segment
        float localT = scaledT - segmentIndex;

        return spline.CamtullCalculations(localT,
            spline.controlPoints[p0Index].position,
            spline.controlPoints[p1Index].position,
            spline.controlPoints[p2Index].position,
            spline.controlPoints[p3Index].position);
    }
}

