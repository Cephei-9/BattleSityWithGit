using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kursor : MonoBehaviour
{
    [SerializeField] private Sprite _defoltKursor;
    [Space]
    [SerializeField] private SpriteRenderer _renderer;

    public void ChangeOnThisSprite(Sprite sprite)
    {
        _renderer.sprite = sprite;
    }

    public void SetActiveDefoltKursor(bool active)
    {
        Cursor.visible = active;
    }
}
