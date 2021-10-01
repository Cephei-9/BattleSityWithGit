using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestroyer : GameFieldChanger
{
    private bool _canDestroy;
    private Vector2Int _nowPointerPosition;

    private void Update()
    {
        if (_canDestroy && Input.GetMouseButton(0))
        {
            DestroyBlock(_nowPointerPosition);
            _canDestroy = false;
        }
    }

    public override void OnPositionChange(Vector2Int newPosition)
    {
        _nowPointerPosition = newPosition;

        _kursor.ChangeOnThisSprite(_falseSprite);
        if (_gird.CheckIsFreeUnit(newPosition)) { _canDestroy = false; return; }

        _kursor.ChangeOnThisSprite(_luckySprite);
        _canDestroy = true;
    }

    private void DestroyBlock(Vector2Int position)
    {
        Destroy(_gird.GetUnit(position).gameObject);
    }
}