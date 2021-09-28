using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceChecker : MonoBehaviour
{
    [SerializeField] float _offsetToRight = 0.5f;
    [Space]
    [SerializeField] Collider2D _selfCollider;

    public float CheckDistance()
    {
        bool ColliderEnabled = _selfCollider.enabled;
        SetActiveCollider(false);
        float rightRay = CastRayAndCheckDistance(transform.position, transform.right);
        float leftRay = CastRayAndCheckDistance(transform.position, - transform.right);
        float minDistance = Mathf.Min(rightRay, leftRay);
        SetActiveCollider(ColliderEnabled);
        return minDistance;
    }

    public float CheckDistance(Vector2 position, Vector2 direction)
    {
        bool ColliderEnabled = _selfCollider.enabled;
        SetActiveCollider(false);
        float distance = CastRay(position, direction);

        SetActiveCollider(ColliderEnabled);
        return distance;
    }

    private float CastRayAndCheckDistance(Vector3 origin, Vector3 directionOffset)
    {
        Vector2 position = origin + directionOffset * _offsetToRight;
        RaycastHit2D raycastHit = Physics2D.Raycast(position, transform.up);
        return raycastHit.distance;
    }

    private float CastRay(Vector3 origin, Vector3 direction)
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(origin, direction);
        return raycastHit.distance;
    }

    private void SetActiveCollider(bool active)
    {
        _selfCollider.enabled = active;
    }
}
