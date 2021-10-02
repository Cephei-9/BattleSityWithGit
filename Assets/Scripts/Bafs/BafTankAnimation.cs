using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BafTankAnimation : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Color red;

    public void PlayAnimation(SpriteRenderer spriteRenderer)
    {
        StartCoroutine(Animation(spriteRenderer, red));
    }

    public void PlayAnimation(SpriteRenderer spriteRenderer, Color color)
    {
        StartCoroutine(Animation(spriteRenderer, color));
    }

    public IEnumerator Animation(SpriteRenderer renderer, Color color)
    {
        while (renderer != null)
        {
            float lerpKoif = Mathf.PingPong(Time.time * _speed, 1);
            renderer.color = Color.Lerp(Color.white, color, lerpKoif);
            yield return null;
        }
    }
}
