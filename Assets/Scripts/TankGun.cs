using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankGun : MonoBehaviour
{
    [SerializeField] private float _speed =5;
    [SerializeField] private float _shootPeriod = 0.5f;
    [SerializeField] private Bullet _bullet;
    [Space]
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Collider2D _selfCollider;
    [SerializeField] private Fraction _selfFraction;
    [SerializeField] private BulletType _bulletType;

    private bool canShoot = true; 

    public void Shoot()
    {
        if (canShoot == false) return;
        Bullet newBullet = Instantiate(_bullet, _shootPoint.position, _shootPoint.rotation);
        newBullet.StartMove(_shootPoint.up, _speed, _selfFraction, _bulletType);

        Physics2D.IgnoreCollision(_selfCollider, newBullet.SelfCollider);
        canShoot = false;
        StartCoroutine(StaticCoroutine.Wait(_shootPeriod, () => { canShoot = true; }));
    }

    public void UpdateCharacteristic(float speed, float shootPeriod, BulletType bulletType)
    {
        _speed = speed;
        _shootPeriod = shootPeriod;
        _bulletType = bulletType;
    }
}
