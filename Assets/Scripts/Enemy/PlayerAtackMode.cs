using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtackMode : AIMode
{
    [SerializeField] private EnemyAI enemyAI;
    private List<Transform> players;
    private Transform _activePlayer;

    private Vector2 _lastDirection;

    public override void Deactivate()
    {
        base.Deactivate();
        _lastDirection = Vector2.zero;
    }

    protected override void SetDirectionToTank()
    {
        if (_activePlayer == null) FindNearestPlayer();
        if (_activePlayer == null) { enemyAI.ChangeOutOfTurn(); return; }

        Vector2 toPlayer = (Vector2)_activePlayer.position - (Vector2)_tankMove.transform.position;

        Vector2 directionToBase = TrueDot.NormalizeAngleForVector(toPlayer);
        if (_lastDirection == directionToBase)
        {
            directionToBase = TrueDot.NormalizeAngleForVector(Random.insideUnitCircle);
        }
        _lastDirection = directionToBase;

        int i = 0;
        while (_tankMove.SetDirection(directionToBase) == false)
        {
            directionToBase = TrueDot.GetRandomNormolizeDirection();
            i++;
            if (i > 10) break;
        }

        UpdateTime();
    }

    private void FindNearestPlayer()
    {
        float minDistance = Mathf.Infinity;
        foreach (var item in FindObjectsOfType<Player>())
        {
            float distance = (_tankMove.transform.position - item.transform.position).magnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                _activePlayer = item.transform;
            }
        }
    }
}
