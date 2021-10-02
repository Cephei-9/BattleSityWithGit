using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _startHealth = 1;
    [SerializeField] private bool _destroyOnDie = true;
    [Space]
    [SerializeField] private Fraction _selfFraction;
    [SerializeField] private GameObject _objToDestroy;

    public int _health { get; private set; }
    public UnityEvent<EnemyAI> DieEvent;
    public UnityEvent TakeDamageEvent;

    public bool IsImmortality = false;
    private Coroutine _immortality;

    private void Start()
    {
        _health = _startHealth;
    }

    public void OnBullet(BulletCollisionInfo bulletInfo)
    {
        if (bulletInfo.Fraction == _selfFraction) return;
        TakeDamage(1);
    }

    public void TakeImmortality(float time)
    {
        if (_immortality != null) StopCoroutine(_immortality);
        _health = Mathf.FloorToInt(Mathf.Infinity);

        GetComponentInChildren<SpriteRenderer>().color = new Color(1, 0.6f, 0.6f);
        System.Action action = () => {
            _health = _startHealth;
            GetComponentInChildren<SpriteRenderer>().color = Color.white;
        };
        _immortality = StartCoroutine(StaticCoroutine.Wait(time, action));
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        TakeDamageEvent.Invoke();
        if (_health <= 0) Die();
    }

    private void Die()
    {
        if(_destroyOnDie) Destroy(_objToDestroy);
        DieEvent.Invoke(GetComponentInParent<EnemyAI>());
    }
}
