using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private AIMode[] _aIModes;
    [SerializeField] private float[] _chanceOfReciving;
    [SerializeField] private AIMode _startModes;

    private AIMode _activeMode;

    private void Start()
    {
        foreach (var item in _aIModes)
        {
            item.Deactivate();
        }
        _startModes.Activate();
        _activeMode = _startModes;
        StartCoroutine(StaticCoroutine.Wait(_activeMode.TimeWork, ChangeMode));
    }

    public void CrioTime(bool active)
    {
        if (active) 
        {
            ChangeMode();
            return; 
        }
        StopAllCoroutines();
        _activeMode.Deactivate();
    }

    private void ChangeMode()
    {
        AIMode newMode = GetRandomMode();
        _activeMode.Deactivate();
        newMode.Activate();
        _activeMode = newMode;
        StartCoroutine(StaticCoroutine.Wait(_activeMode.TimeWork, ChangeMode));
    }

    public void ChangeOutOfTurn()
    {
        StopAllCoroutines();
        ChangeMode();
    }

    private AIMode GetRandomMode()
    {
        Dictionary<AIMode, float> AiModChance = new Dictionary<AIMode, float>();
        for (int i = 0; i < _aIModes.Length; i++)
        {
            if (_aIModes[i] == _activeMode) continue;
            AiModChance.Add(_aIModes[i], Random.value * _chanceOfReciving[i]);
        }
        AIMode mode = null;
        float maxChance = 0;
        foreach (var keyValue in AiModChance)
        {
            if (keyValue.Value > maxChance)
            {
                maxChance = keyValue.Value;
                mode = keyValue.Key;
            }
        }
        return mode;
    }
}

[System.Serializable]
public class DirectionPriority
{
    public Vector2 Direction;
    public float priority;
}

public abstract class AIMode : MonoBehaviour
{
    public float TimeWork = 10;
    [Header("Time to change direction")]
    [SerializeField] protected float _minTime = 1;
    [SerializeField] protected float _maxTime = 2;
    [SerializeField] protected float _timeToTurnAfterCollision = 2;
    [Space]
    [SerializeField] protected float _shootPeriod = 2;
    [Space]
    [SerializeField] protected TankMove _tankMove;
    [SerializeField] protected TankGun _tankGun;

    private float _timerForDirection;
    private float _timerForShoot;
    private float _timeToNextChangeDirection;

    private void Update()
    {
        AcceptCollision();

        _timerForDirection += Time.deltaTime;
        _timerForShoot += Time.deltaTime;

        if (_timerForDirection > _timeToNextChangeDirection)
        {
            _timerForDirection = 0;
            SetDirectionToTank();
        }

        if (_timerForShoot > _shootPeriod)
        {
            _tankGun.Shoot();
            _timerForShoot = 0;
        }
    }

    public virtual void Activate()
    {
        enabled = true;
        SetDirectionToTank();
        UpdateTime();
    }

    public virtual void Deactivate()
    {
        enabled = false;
    }

    protected abstract void SetDirectionToTank();

    public virtual void AcceptCollision()
    {
        if (_tankMove.IsCollision)
        {
            _timeToNextChangeDirection = _timeToTurnAfterCollision;
        }
    }

    protected virtual void UpdateTime()
    {
        _timeToNextChangeDirection = Random.Range(_minTime, _maxTime);
    }
}
