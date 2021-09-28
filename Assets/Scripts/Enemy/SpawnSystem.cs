using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] int _maxEnemyCount = 4;
    [SerializeField] float _spawnPeriod = 4;

    [SerializeField] Transform[] _spawnPoint;
    [SerializeField] EnemyAI _enemy;

    public UnityEvent<EnemyAI> OnNewEnemyEvent;

    public bool CanSpawn { get => _maxEnemyCount > Enemies.Count; }
    public List<EnemyAI> Enemies { get; private set; } = new List<EnemyAI>();

    private Coroutine _spawnEnemy;
    private int nextSpawn;

    private void Start()
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
        yield return new WaitForSeconds(_spawnPeriod);

        Vector3 position = _spawnPoint[nextSpawn].position;
        nextSpawn++;
        if (nextSpawn == _spawnPoint.Length) nextSpawn = 0;

        EnemyAI enemy = Instantiate(_enemy, position, Quaternion.identity);
        Enemies.Add(enemy);
        OnNewEnemyEvent.Invoke(enemy);
        enemy.GetComponentInChildren<Health>().DieEvent.AddListener(OnTankDeath);

        _spawnEnemy = null;
        StartSpawnEnemyCoroutine();
    }

    private void StartSpawnEnemyCoroutine()
    {
        if (CanSpawn && _spawnEnemy == null) _spawnEnemy = StartCoroutine(SpawnEnemy());
    }

    // Если умирает враг то ждется полноценный спавн период
}
