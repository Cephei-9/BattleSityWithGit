using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTransparent : MonoBehaviour
{
    public SpriteRenderer[] SpriteRenderers;
    public float transp = 0.5f;

    private void Start()
    {
        SetTransparent();
    }

    public void SetTransparent()
    {
        foreach (var item in SpriteRenderers)
        {
            item.color = new Color(item.color.r, item.color.g, item.color.b, transp);
        }
    }
}
