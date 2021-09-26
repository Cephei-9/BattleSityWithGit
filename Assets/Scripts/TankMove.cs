using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMove : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private Rigidbody2D _selfRb;

    public Vector3 NowDirection { get; private set; }
    public Vector3 LastDirection { get; private set; }
    public bool IsMove { get; private set; }

    public void SetDirection(Vector2 diretion)
    {
        IsMove = true;
        if (Vector2.Dot(NowDirection, diretion) == 0) Turn();
        NowDirection = diretion;

        transform.rotation = Quaternion.LookRotation(Vector3.forward, NowDirection);
        _selfRb.velocity = NowDirection * _speed;
    }

    public void Stop()
    {
        IsMove = false;
        _selfRb.velocity = Vector2.zero;
    }

    private void Turn()
    {
        print("Turn");
        float positionOnAxis = transform.position.x;
        if (NowDirection.x == 0) positionOnAxis = transform.position.y;

        positionOnAxis = Mathf.Round(positionOnAxis);

        Vector2 newPosition = new Vector2(positionOnAxis, transform.position.y);
        if (NowDirection.x == 0) newPosition = new Vector2(transform.position.x, positionOnAxis);
        //transform.position = newPosition;
        _selfRb.MovePosition(newPosition);
    }
}
