using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCrio : MonoBehaviour
{
    [SerializeField] private float _timeWork = 5;

    private Dictionary<Transform, Vector2> EnemiesPosition = new Dictionary<Transform, Vector2>();
    private bool _beginWork;

    public void StartWork()
    {
        _beginWork = true;
        SpawnSystem spawnSystem = FindObjectOfType<SpawnSystem>();
        spawnSystem.OnNewEnemyEvent.AddListener(OnAddEnemy);

        foreach (var enemy in spawnSystem.Enemies)
        {
            Transform enemyTransform = enemy.transform.GetChild(0).transform; 
            EnemiesPosition.Add(enemyTransform, enemyTransform.position);
        }
    }

    private void LateUpdate()
    {
        if (_beginWork == false) return;
        FrizeEnemy();
    }

    private void OnAddEnemy(EnemyAI enemy)
    {
        Transform enemyTransform = enemy.transform;
        EnemiesPosition.Add(enemyTransform, enemyTransform.position);
    }

    private void FrizeEnemy()
    {
        foreach (var keyValue in EnemiesPosition)
        {
            if(keyValue.Key == null)
            {
                EnemiesPosition.Remove(keyValue.Key);
                FrizeEnemy();
                return;
            }
            keyValue.Key.position = keyValue.Value;
            print("TransP: " + keyValue.Key.position + " Vector: " + keyValue.Value);
        }
    }
}
