using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform targetToFollow;

    bool follow = false;
    void FixedUpdate()
    {
        if (follow && targetToFollow && Vector2.Distance(transform.position, targetToFollow.position) > 0.5f)
        {
            Vector3 vector = Vector3.Lerp(transform.position, targetToFollow.position, Time.deltaTime * 3);
            vector.z = -10;
            transform.position = vector;
        }
    }

    public void SetPosition(Vector3 Point)
    {
        follow = false;
        Point.z = -10;
        transform.position = Point;
        follow = true;
    }

    public void SetTargetToFollow(Transform target)
    {
        targetToFollow = target;
        SetPosition(target.transform.position);
    }
}
