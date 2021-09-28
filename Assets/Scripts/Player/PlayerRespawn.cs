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

    public void Spawn()
    {
        _health.transform.position = _spawn.position;
        _health.transform.rotation = Quaternion.identity;
        _beginImmortality.GiveImmortality(_health.gameObject);
    }

    public void OnPlayerDie()
    {
        _health.transform.position = new Vector2(-100, -100);
        StartCoroutine(StaticCoroutine.Wait(_waitTime, 
            ()=> { Spawn(); _health.gameObject.SetActive(true); }));
    }
}

interface IResatble
{
    void OnReset();
}
