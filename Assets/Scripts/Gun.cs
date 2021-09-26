using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Collider2D _selfCollider;

    public void Shoot()
    {
        Bullet newBullet = Instantiate(_bullet, _shootPoint.position, _shootPoint.rotation);
        newBullet.StartMove(_shootPoint.up, _speed);

        Physics2D.IgnoreCollision(_selfCollider, newBullet.SelfCollider);
    }
}
