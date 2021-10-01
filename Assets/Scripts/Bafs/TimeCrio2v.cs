using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCrio2v : MonoBehaviour
{
    [SerializeField] private float _timeWork = 5;
    [SerializeField] private bool _destroyAfterDie = true;

    private List<Transform> _tanks = new List<Transform>();

    public void StartWork()
    {
        SpawnSystem spawnSystem = FindObjectOfType<SpawnSystem>();
        spawnSystem.OnNewEnemyEvent.AddListener(OnAddEnemy);

        foreach (var enemyAI in spawnSystem.Enemies)
        {
            Transform tank = enemyAI.transform.GetChild(0);
            SetActiveTankComponent(false, tank);
            _tanks.Add(tank); 
        }
        StartCoroutine(Wait());
    }

    private void OnAddEnemy(EnemyAI enemyAI)
    {
        System.Action action = () =>
        {
            Transform newTank = enemyAI.transform.GetChild(0);
            _tanks.Add(newTank);
            SetActiveTankComponent(false, newTank);
        };
        StartCoroutine(WaitNextFrame(action));
    }

    private void SetActiveTankComponent(bool active, Transform tank)
    {
        tank.GetComponentInParent<EnemyAI>().CrioTime(active);
        tank.GetComponent<TankMove>().enabled = active;
        tank.GetComponent<TankGun>().enabled = active;
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(_timeWork);

        foreach (var tank in _tanks)
        {
            if (tank == null) continue;
            SetActiveTankComponent(true, tank);
        }
        if(_destroyAfterDie) Destroy(gameObject);
    }

    private IEnumerator WaitNextFrame(System.Action action)
    {
        yield return null;
        action.Invoke();
    }
}
