using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletTaker : MonoBehaviour
{
    public UnityEvent<BulletCollisionInfo> OnBulletColliionEvent;

    public void TakeBullet(BulletCollisionInfo bulletInfo)
    {
        OnBulletColliionEvent.Invoke(bulletInfo);
    }
}
