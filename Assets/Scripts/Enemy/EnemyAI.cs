using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Time to change direction")]
    [SerializeField] private float _minTime = 1;
    [SerializeField] private float _maxTime = 2;
    [Space]
    [SerializeField] private float _shootPeriod = 2;
    [Space]
    [SerializeField] private TankMove _tankMove;
    [SerializeField] private TankGun _tankGun;

    private float _timerForDirection;
    private float _timerForShoot;
    private float _timeToNextChangeDirection;

    private bool _isCollision;

    private void Start()
    {
        SetRandomDirectionToTank();
    }

    private void Update()
    {
        _timerForDirection += Time.deltaTime;
        _timerForShoot += Time.deltaTime;

        if (_timerForDirection > _timeToNextChangeDirection)
        {
            _timerForDirection = 0;
            SetRandomDirectionToTank();
        }

        if (_timerForShoot > _shootPeriod)
        {
            _tankGun.Shoot();
            _timerForShoot = 0;
        }
    }

    public void AcceptCollision()
    {
        if (_isCollision == false)
        {
            _timeToNextChangeDirection = _minTime;
            _isCollision = true; 
        }
    }

    private void SetRandomDirectionToTank()
    {
        float randomSign = Mathf.Sign(Random.Range(-1, 1));
        float randomX = Random.value;
        float randomY = Random.value;

        Vector2 direction = Vector2.right;
        if (randomY > randomX) direction = Vector3.up;
        direction *= randomSign;
        _tankMove.SetDirection(direction);
        UpdateTime();

        _isCollision = false;
    }

    private void UpdateTime()
    {
        _timeToNextChangeDirection = Random.Range(_minTime, _maxTime);
    }
}

[System.Serializable]
public class DirectionPriority 
{
    public Vector2 Direction;
    public float priority;
}
