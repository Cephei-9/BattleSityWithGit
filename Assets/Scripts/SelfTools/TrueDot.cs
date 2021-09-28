using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TrueDot
{
    public static float Dot(Vector2 from, Vector2 to)
    {
        float angle = Vector2.Angle(from, to);
        if (angle > 90)
        {
            return -1 * Mathf.InverseLerp(90, 180, angle);
        }
        return Mathf.InverseLerp(90, 0, angle);
    }

    public static Vector2 NormalizeAngleForVector(Vector2 vector)
    {
        float DotFromRight = Dot(vector, Vector2.right);
        float DotFromUp = Dot(vector, Vector2.up);

        Vector2 direction = Vector2.up * Mathf.Sign(DotFromUp);
        if (Mathf.Abs(DotFromRight) > Mathf.Abs(DotFromUp))
        {
            direction = Vector2.right * Mathf.Sign(DotFromRight);
        }
        return direction;
    }

    public static Vector2 GetRandomNormolizeDirection()
    {
        float randomSign = Mathf.Sign(Random.Range(-1, 1));
        float randomX = Random.value;
        float randomY = Random.value;

        Vector2 direction = Vector2.right;
        if (randomY > randomX) direction = Vector3.up;
        return direction * randomSign;
    }
}
