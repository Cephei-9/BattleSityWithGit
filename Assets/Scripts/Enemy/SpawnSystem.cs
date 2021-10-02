using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] private ChalengeSystem _chalengeSystem;
    [SerializeField] int _maxEnemyCount = 4;
    [SerializeField] float _spawnPeriod = 4;

    [SerializeField] Transform[] _spawnPoint;
    [SerializeField] EnemyAI _enemy;
    [SerializeField] SpawnAnimation animation;

    public UnityEvent<EnemyAI> OnNewEnemyEvent;

    public bool CanSpawn = true;
    public List<EnemyAI> Enemies { get; private set; } = new List<EnemyAI>();

    private Coroutine _spawnEnemy;
    private int _nextSpawn;

    public void StartGame()
    {
        CanSpawn = true;
        StartSpawnEnemyCoroutine();
    }

    public void Stop()
    {
        if (_spawnEnemy != null) StopCoroutine(_spawnEnemy);
        _spawnEnemy = null;
        CanSpawn = false;
        CleanEnemiesArrByNull();
    }

    public void OnTankDeath(EnemyAI enemy)
    {
        Enemies.Remove(enemy);
        StartSpawnEnemyCoroutine();
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(_chalengeSystem.NextSpawnPeriod);

        Vector3 position = _spawnPoint[_nextSpawn].position;
        _nextSpawn++;
        if (_nextSpawn == _spawnPoint.Length) _nextSpawn = 0;

        EnemyAI enemy = Instantiate(_chalengeSystem.GetNextEnemy(), position, Quaternion.identity);

        Enemies.Add(enemy);
        OnNewEnemyEvent.Invoke(enemy);
        enemy.GetComponentInChildren<Health>().DieEvent.AddListener(OnTankDeath);

        _spawnEnemy = null;
        StartSpawnEnemyCoroutine();
    }

    private void StartSpawnEnemyCoroutine()
    {
        if (CanSpawn == false) 
        {
            animation.StopAnimation();
            return;
        }
        if (_chalengeSystem.MaxEnemyCount > Enemies.Count && _spawnEnemy == null)
        {
            _spawnEnemy = StartCoroutine(SpawnEnemy());
            animation.PlayAnimation();
            animation.ChangePosition(_spawnPoint[_nextSpawn].position);
        }

        if (_spawnEnemy == null) animation.StopAnimation();
    }

    public void CleanEnemiesArrByNull()
    {
        foreach (var item in Enemies)
        {
            if(item == null)
            {
                Enemies.Remove(item);
                CleanEnemiesArrByNull();
                return;
            }
        }
        CheckNull();
    }

    [ContextMenu("CheckNull")]
    public void CheckNull()
    {
        foreach (var item in Enemies)
        {
            if (item == null) print("In arr, this Null");
        }
    }

    // Если умирает враг то ждется полноценный спавн период
}

