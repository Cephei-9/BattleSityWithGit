using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCreator : GameFieldChanger
{
    [SerializeField] private GirdObj _wallPrefub;
    [SerializeField] private Transform _wallsParant;

    private bool _canBild;
    private Vector2Int _nowPointerPosition;

    private void Update()
    {
        if (_canBild && Input.GetMouseButton(0)) 
        {
            if (ScoreAndMoney.SingleTone.TryToBye(_price) == false) return;

            CreateBlock(_nowPointerPosition);
            _canBild = false;
        }
    }

    public override void OnPointerExitGameField() => _canBild = false;

    public override void OnPositionChange(Vector2Int newPosition)
    {
        _nowPointerPosition = newPosition;

        if (_gird.CheckIsFreeUnit(newPosition) == false || ForbiddenCell.SingleTone.CheckOnForbidden(newPosition))
        { 
            _kursor.ChangeOnThisSprite(_falseSprite);
            _canBild = false; 
            return; 
        }

        _kursor.ChangeOnThisSprite(_luckySprite);
        _canBild = true;
    }

    private void CreateBlock(Vector2Int position)
    {
        GirdObj girdObj = Instantiate(_wallPrefub, (Vector3Int)position, Quaternion.identity, _wallsParant);
        _gird.SetUnit(position, girdObj);
    }
}
