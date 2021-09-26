using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _selfRb;
    [SerializeField] private Collider2D _selfCollider;

    public Collider2D SelfCollider { get => _selfCollider; }

    private Vector3 _direction;
    private float _speed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out BulletTaker bulletTaker))
        {
            bulletTaker.TakeBullet(transform.position);
        }
        Destroy(gameObject);
    }

    private void Update()
    {
        //transform.position += _direction * _speed * Time.deltaTime;
    }

    public void StartMove(Vector3 direction, float speed)
    {
        _direction = direction;
        _speed = speed;
        _selfRb.velocity = direction * speed;
    }
}
