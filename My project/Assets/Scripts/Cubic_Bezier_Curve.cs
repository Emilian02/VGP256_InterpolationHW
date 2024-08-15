using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubic_Bezier_Curve : MonoBehaviour
{
    [SerializeField] Transform[] controlPoints;
    [SerializeField] Transform visualPoint;
    [SerializeField] float speed;

    private float t = 0.0f;
    void Start()
    {
        
    }

    void Update()
    {
        if (t < 1.0f)
        {
            t += Time.deltaTime * speed;
            t = Mathf.Clamp01(t); // Ensure t doesn't exceed 1
            visualPoint.position = CubicBezierCalculation(t, controlPoints[0].position, controlPoints[1].position, controlPoints[2].position, controlPoints[3].position);
        }
        else
        {
            t = 0.0f; // Reset t for looping
        }
    }

    Vector3 CubicBezierCalculation(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        // B(t) = (1-t)^3 * P0 + 3(1-t)^2 * t * P1 + 3(1-t) * t^2 * P2 + t^3 * P3 
        
        float u = 1 - t;

        Vector3 p = (Mathf.Pow(u, 3) * p0) + (3 * Mathf.Pow(u, 2) * t * p1) + (3 * u * Mathf.Pow(t, 2) * p2) + (Mathf.Pow(t, 3) * p3);

        return p;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector2(controlPoints[0].position.x, controlPoints[0].position.y),
            new Vector2(controlPoints[1].position.x, controlPoints[1].position.y));

        Gizmos.DrawLine(new Vector2(controlPoints[1].position.x, controlPoints[1].position.y),
            new Vector2(controlPoints[2].position.x, controlPoints[2].position.y));

        Gizmos.DrawLine(new Vector2(controlPoints[2].position.x, controlPoints[2].position.y),
            new Vector2(controlPoints[3].position.x, controlPoints[3].position.y));

        for (float t = 0; t <= 1; t += 0.05f)
        {
            Vector3 point = CubicBezierCalculation(t, controlPoints[0].position, controlPoints[1].position, controlPoints[2].position, controlPoints[3].position);
            Gizmos.DrawSphere(point, 0.1f);
        }
    }
}
