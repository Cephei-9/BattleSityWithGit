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
}
