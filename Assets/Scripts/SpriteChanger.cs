using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprite;
    [Space]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public int Index = 0;

    public void ChangeSpriteOnNext()
    {
        _spriteRenderer.sprite = _sprite[Index];
        Index++;
        if (Index == _sprite.Length) Index = 0;
    }

    public void ChangeSpriteByIndex(int index)
    {
        _spriteRenderer.sprite = _sprite[index];
    }

    public void ChangeSpriteOnThis(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }
}
