using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kursor : MonoBehaviour
{
    [SerializeField] private Sprite _defoltKursor;
    [Space]
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private SpriteRenderer _falseRenderer;

    public void ChangeOnThisSprite(Sprite sprite)
    {
        _renderer.sprite = sprite;
    }

    public void ActiveFalseSprite(bool active)
    {
        _falseRenderer.gameObject.SetActive(active);
    }

    public void SetActiveDefoltKursor(bool active)
    {
        Cursor.visible = active;
    }
}
