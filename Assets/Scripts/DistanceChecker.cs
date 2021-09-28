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
        SetActiveCollider(false);
        float rightRay = CastRayAndCheckDistance(transform.right);
        float leftRay = CastRayAndCheckDistance(-transform.right);
        float minDistance = Mathf.Min(rightRay, leftRay);
        SetActiveCollider(true);
        return minDistance;
    }

    private float CastRayAndCheckDistance(Vector3 directionOffset)
    {
        Vector2 position = transform.position + directionOffset * _offsetToRight;
        RaycastHit2D raycastHit = Physics2D.Raycast(position, transform.up);
        return raycastHit.distance;
    }

    private void SetActiveCollider(bool active)
    {
        _selfCollider.enabled = active;
    }
}
