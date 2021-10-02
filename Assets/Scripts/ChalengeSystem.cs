using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChalengeSystem : MonoBehaviour
{
    [SerializeField] private float _timeScale = 1;
    [SerializeField] private ChalengeParametrs _forOnePlayer;
    [SerializeField] private ChalengeParametrs _forTwoPlayer;
    [Space]
    [SerializeField] private EnemyChance[] _enemysChance;

    public int MaxEnemyCount { get => Mathf.RoundToInt(_activeParametrs.MaxEnemyCount.Evaluate(TimeOnStart)); }
    public float NextSpawnPeriod { get => _activeParametrs.SpawnPeriod.Evaluate(TimeOnStart); }
    public float TimeOnStart { get => (Time.time - _timeOnStart) * _timeScale; }

    private float _timeOnStart;
    public ChalengeParametrs _activeParametrs;

    public void ChackStartGameTime()
    {
        _timeOnStart = Time.time;
    }

    public EnemyAI GetNextEnemy()
    {
        EnemyAI enemyAI = null;
        float maxChance = 0;
        foreach (var item in _enemysChance)
        {
            float itemChance = item.GetChance(Time.time * _timeScale);
            if (itemChance > maxChance)
            {
                maxChance = itemChance;
                enemyAI = item.EnemyPrefab;
            }
        }
        return enemyAI;
    }

    public void SetParametrsOnOnePlayer(bool forOnePlayer)
    {
        if (forOnePlayer)
        {
            _activeParametrs = _forOnePlayer;
            return;
        }
        _activeParametrs = _forTwoPlayer;
        print("Max enemy count: " + _activeParametrs.MaxEnemyCount.Evaluate(0));
    }
}

[System.Serializable]
public class EnemyChance
{
    public EnemyAI EnemyPrefab;
    public AnimationCurve DropChance;

    public float GetChance(float time) { return DropChance.Evaluate(time) * Random.value; }
}

[System.Serializable]
public class ChalengeParametrs
{
    public AnimationCurve SpawnPeriod;
    public AnimationCurve MaxEnemyCount;
}