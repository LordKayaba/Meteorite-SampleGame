using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform meteorite;

    bool SetPos = false;
    void FixedUpdate()
    {
        if(!SetPos && Vector2.Distance(transform.position, meteorite.position) > 0.5f)
        {
            Vector3 vector = Vector2.Lerp(transform.position, meteorite.position, Time.deltaTime * 3);
            vector.z = -10;
            transform.position = vector;
        }
    }

    public void SetPosition(Vector3 Point)
    {
        SetPos = true;
        Point.z = -10;
        transform.position = Point;
        SetPos = false;
    }
}
