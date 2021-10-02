using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnimation : MonoBehaviour
{
    // Пока карутина не ровна налл анимация работает у некст спавна

    [SerializeField] private float _speed = 1;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private SpriteRenderer _rendarer;

    private Coroutine _coroutine;

    public void PlayAnimation()
    {
        _rendarer.enabled = true;
        if(_coroutine == null) _coroutine = StartCoroutine(Animation());
    }

    public void StopAnimation()
    {
        if (_coroutine == null) return;
        _rendarer.enabled = false;
        StopCoroutine(_coroutine);
        _coroutine = null;
    }

    public void ChangePosition(Vector2 position)
    {
        _rendarer.transform.position = position;
    }

    IEnumerator Animation()
    {
        float timer;
        while (true)
        {
            timer = Mathf.PingPong(Time.time * _speed, 1) * (_sprites.Length - 1);
            _rendarer.sprite = _sprites[Mathf.RoundToInt(timer)];
            yield return null;
        }
    }
}
