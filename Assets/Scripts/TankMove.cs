using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMove : MonoBehaviour
{
    public float Speed = 1;
    [SerializeField] private Rigidbody2D _selfRb;
    [SerializeField] private DistanceChecker distanceCheck;

    public Vector3 NowDirection { get; private set; }
    public Vector3 LastDirection { get; private set; }
    public bool IsMove { get; private set; }
    public bool IsCollision { get; private set; }

    private void Update()
    {
        if (IsMove)
        {
            IsCollision = false;
            float velosityOnThisFrame = Speed * Time.deltaTime;
            float distanceToNerhestObj = distanceCheck.CheckDistance() - 1;
            float min = Mathf.Min(velosityOnThisFrame, distanceToNerhestObj);
            if (distanceToNerhestObj < velosityOnThisFrame) IsCollision = true;
            if (min < 0) return;
            transform.position += min * NowDirection;
        }
    }

    public bool SetDirection(Vector2 newDiretion)
    {
        IsMove = true;
        if (Vector2.Dot(NowDirection, newDiretion) == 0) 
        { 
            if (Turn(newDiretion) == false) return false; 
        }
        NowDirection = newDiretion;

        transform.rotation = Quaternion.LookRotation(Vector3.forward, NowDirection);
        return true;
    }

    public void Stop()
    {
        IsMove = false;
    }

    private bool Turn(Vector2 newDirection)
    {
        float positionOnAxis = transform.position.x;
        if (NowDirection.x == 0) positionOnAxis = transform.position.y;

        positionOnAxis = Mathf.Round(positionOnAxis);

        Vector2 newPosition = new Vector2(positionOnAxis, transform.position.y);
        if (NowDirection.x == 0) newPosition = new Vector2(transform.position.x, positionOnAxis);

        print("DistanseAfterTurn: " + System.Math.Round(distanceCheck.CheckDistance(transform.position, newDirection), 4));
        if (System.Math.Round(distanceCheck.CheckDistance(transform.position, newDirection), 4) < 1) 
        {
            print("Name tank: " + transform.parent.name);
            //Debug.Break();
            return false;
        }

        transform.position = newPosition;
        return true;
    }
}
