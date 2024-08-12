using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quadratic_Bezier_Curves : MonoBehaviour
{
    [SerializeField]
    Transform[] controlPoints;
    [SerializeField]
    Transform visualPoint;
    [SerializeField]
    float speed ;

    private float t = 0.0f;

    void Start()
    {
        
    }

    void Update()
    {
        while ( t < 1.0f )
        {
            t += Time.deltaTime;

            visualPoint.position = QuadraticBezierCalculation(t, controlPoints[0].position, controlPoints[1].position, controlPoints[2].position);
        }

        t = 0.0f;
    }

    Vector3 QuadraticBezierCalculation(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        // B(t) = (1-t)^2P0 + 2(1-t)tP1 + t^2P2 , 0 < t < 1
        //          u            u        tt   
        //    uu * p0 + 2 u * t * p1 + tt * p2

        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = (uu * p0) + (2 * u * t * p1) + (tt * p2);

        return p;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector2(controlPoints[0].position.x, controlPoints[0].position.y),
            new Vector2(controlPoints[1].position.x, controlPoints[1].position.y));

        Gizmos.DrawLine(new Vector2(controlPoints[1].position.x, controlPoints[1].position.y),
            new Vector2(controlPoints[2].position.x, controlPoints[2].position.y));
    }       

}
