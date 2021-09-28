using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _selfRb;
    [SerializeField] private Collider2D _selfCollider;

    public Fraction SelfFraction { get; private set; }
    public Collider2D SelfCollider { get => _selfCollider; }

    private Vector3 _direction;
    private float _speed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out BulletTaker bulletTaker))
        {
            BulletCollisionInfo bulletInfo = new BulletCollisionInfo(
                transform.position, SelfFraction, collision);
            bulletTaker.TakeBullet(bulletInfo);
        }
        Destroy(gameObject);
    }

    private void Update()
    {
        //transform.position += _direction * _speed * Time.deltaTime;
    }

    public void StartMove(Vector3 direction, float speed, Fraction fraction)
    {
        _direction = direction;
        _speed = speed;
        _selfRb.velocity = direction * speed;
        SelfFraction = fraction;
    }
}

public class BulletCollisionInfo
{
    public readonly Vector2 lastPosition;
    public readonly Fraction Fraction;
    public readonly Collision2D Collision2D;

    public BulletCollisionInfo(Vector2 lastPosition, Fraction fraction, Collision2D collision2D)
    {
        this.lastPosition = lastPosition;
        Fraction = fraction;
        Collision2D = collision2D;
    }
}
