using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] Transform[] _spawnPoint;
    [SerializeField] EnemyAI _enemy;

    [SerializeField] int _maxEnemyCount = 4;
    [SerializeField] float _spawnPeriod = 4;

    public bool CanSpawn { get => _maxEnemyCount > EnemyOnFild; }
    public int EnemyOnFild { get; private set; }

    private Coroutine _spawnEnemy;

    public void OnTankDeath()
    {
        EnemyOnFild--;
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(_spawnPeriod);

        Vector3 position = _spawnPoint[Random.Range(0, _spawnPoint.Length)].position;
        Instantiate(_enemy, position, Quaternion.identity);
        //Подписаться на его смерть

        if (CanSpawn) StartCoroutine(SpawnEnemy());
    }

    // Если умирает враг то ждется полноценный спавн период
}
