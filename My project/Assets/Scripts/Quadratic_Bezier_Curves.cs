using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quadratic_Bezier_Curves : MonoBehaviour
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
            visualPoint.position = QuadraticBezierCalculation(t, controlPoints[0].position, controlPoints[1].position, controlPoints[2].position);
        }
        else
        {
            t = 0.0f; // Reset t for looping
        }
    }

    Vector3 QuadraticBezierCalculation(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        // B(t) = (1-t)^2 * P0 + 2(1-t) * t * P1 + t^2 * P2 

        float u = 1 - t;

        Vector3 p = (Mathf.Pow(u, 2) * p0) + (2 * u * t * p1) + (Mathf.Pow(t, 2) * p2);

        return p;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector2(controlPoints[0].position.x, controlPoints[0].position.y),
            new Vector2(controlPoints[1].position.x, controlPoints[1].position.y));

        Gizmos.DrawLine(new Vector2(controlPoints[1].position.x, controlPoints[1].position.y),
            new Vector2(controlPoints[2].position.x, controlPoints[2].position.y));

        for (float t = 0; t <= 1; t += 0.05f)
        {
            Vector3 point = QuadraticBezierCalculation(t, controlPoints[0].position, controlPoints[1].position, controlPoints[2].position);
            Gizmos.DrawSphere(point, 0.1f);
        }
    }       

}
