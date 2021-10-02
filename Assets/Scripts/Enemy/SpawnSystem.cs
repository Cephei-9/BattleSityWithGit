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

    public UnityEvent<EnemyAI> OnNewEnemyEvent;

    public bool CanSpawn { get => _maxEnemyCount > Enemies.Count; }
    public List<EnemyAI> Enemies { get; private set; } = new List<EnemyAI>();

    private Coroutine _spawnEnemy;
    private int _nextSpawn;

    public void StartGame()
    {
        StartSpawnEnemyCoroutine();
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
        if (_chalengeSystem.MaxEnemyCount > Enemies.Count && _spawnEnemy == null) 
            _spawnEnemy = StartCoroutine(SpawnEnemy());
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

