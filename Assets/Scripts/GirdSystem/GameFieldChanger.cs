using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameFieldChanger : MonoBehaviour
{
    [SerializeField] protected int _price = 1;
    [SerializeField] protected Sprite _luckySprite;
    [SerializeField] protected Sprite _falseSprite;
    [Space]
    [SerializeField] protected Gird _gird;
    [SerializeField] protected Kursor _kursor;

    public abstract void OnPositionChange(Vector2Int newPosition);

    public abstract void OnPointerExitGameField();
}
