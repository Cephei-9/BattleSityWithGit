using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : AIMode
{
    [SerializeField] private Vector2 _playerBasePosition;

    private Vector2 _lastDirection;

    public override void Deactivate()
    {
        base.Deactivate();
        _lastDirection = Vector2.zero;
    }

    protected override void SetDirectionToTank()
    {
        print("AttackBase");
        Vector2 toBase = _playerBasePosition - (Vector2)_tankMove.transform.position;

        Vector2 directionToBase = TrueDot.NormalizeAngleForVector(toBase);
        if (_lastDirection == directionToBase)
        {
            directionToBase = TrueDot.GetRandomNormolizeDirection();
        }
        _lastDirection = directionToBase;

        int i = 0;
        while(_tankMove.SetDirection(directionToBase) == false)
        {
            directionToBase = TrueDot.GetRandomNormolizeDirection();
            i++;
            if (i > 10) break;
        }

        UpdateTime();
    }
}
