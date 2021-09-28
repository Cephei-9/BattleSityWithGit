using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankGun : MonoBehaviour
{
    [SerializeField] private float _speed =5;
    [SerializeField] private Bullet _bullet;
    [Space]
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Collider2D _selfCollider;
    [SerializeField] private Fraction _selfFraction;

    public void Shoot()
    {
        Bullet newBullet = Instantiate(_bullet, _shootPoint.position, _shootPoint.rotation);
        newBullet.StartMove(_shootPoint.up, _speed, _selfFraction);

        Physics2D.IgnoreCollision(_selfCollider, newBullet.SelfCollider);
    }
}
