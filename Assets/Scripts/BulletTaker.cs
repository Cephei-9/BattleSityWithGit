using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletTaker : MonoBehaviour
{
    public UnityEvent<Vector2> OnBulletColliionEvent;

    public void TakeBullet(Vector2 bulletPosition)
    {
        OnBulletColliionEvent.Invoke(bulletPosition);
    }
}
