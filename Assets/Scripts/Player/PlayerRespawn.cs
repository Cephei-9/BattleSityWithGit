using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private float _waitTime = 2;
    [SerializeField] private Transform _spawn;
    [Space]
    [SerializeField] private Health _health;
    [SerializeField] private Immortality _beginImmortality;

    public bool PlayerOnField = true;

    public void Spawn()
    {
        PlayerOnField = true;
        KillTankOnSpawnPlace();

        _health.transform.position = _spawn.position;
        _health.transform.rotation = Quaternion.identity;
        _beginImmortality.GiveImmortality(_health.gameObject);
    }

    public void KillTankOnSpawnPlace()
    {
        Collider2D collider = Physics2D.BoxCast(transform.position, Vector2.one * 1.98f, 0, Vector2.zero, 0).collider;
        if (collider != null && collider.TryGetComponent(out Health health)) 
        {
            print("Get health");
            health.TakeDamage(Mathf.RoundToInt(Mathf.Infinity));
        }
    }

    public void OnPlayerDie()
    {
        PlayerOnField = false;
        _health.transform.position = new Vector2(-100, -100);
        StartCoroutine(StaticCoroutine.Wait(_waitTime, 
            ()=> { Spawn(); _health.gameObject.SetActive(true); }));
    }
}
