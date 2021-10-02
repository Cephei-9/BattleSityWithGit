using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScaleAnimation : MonoBehaviour
{
    [SerializeField] private float _minScale = 1;
    [SerializeField] private float _maxScale = 1.2f;
    [SerializeField] private float _time = 1;
    [Space]
    [SerializeField] private Transform button;

    private Coroutine coroutine;

    public void Incrace()
    {
        button.localScale = Vector3.one * _maxScale;
    }

    public void Decrace()
    {
        button.localScale = Vector3.one * _minScale;
    }

    public void PlayAnimation()
    {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(PlayAnimationCaroutin());
    }

    private IEnumerator PlayAnimationCaroutin()
    {
        for (float t = 0; t < 1; t += Time.deltaTime / _time)
        {
            button.localScale = Vector3.one * Mathf.Lerp(_minScale, _maxScale, Mathf.Sin(t * Mathf.PI));
            yield return null;
        }
    }
}
