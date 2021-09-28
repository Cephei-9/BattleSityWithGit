using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionControler : MonoBehaviour
{
    [SerializeField] float _offsetToRight = 0.5f;
    [Space]
    [SerializeField] Collider2D _selfCollider;

    public UnityEvent OnCollision; 

    private void LateUpdate()
    {
        SetActiveCollider(false);
        float rightRay = CastRayAndCheckDistance(transform.right);
        float leftRay = CastRayAndCheckDistance(-transform.right);
        float minDistance = Mathf.Min(rightRay, leftRay);

        if (minDistance < 1)
        {
            OnCollision.Invoke();
            float offset = 1 - minDistance;
            transform.position -= transform.up * offset;
        }
        SetActiveCollider(true);
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