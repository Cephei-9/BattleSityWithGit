using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnTimeAnimation : MonoBehaviour
{
    [SerializeField] private float _time = 5;
    [SerializeField] private Text _text;
    
    private Coroutine _coroutine;

    public void PlayAnimation(float time)
    {
        _text.enabled = true;
        _coroutine = StartCoroutine(Animation(time));
    }

    public void StopAnimation()
    {
        _text.enabled = false;
        StopCoroutine(_coroutine);
    }

    IEnumerator Animation(float time)
    {
        print("Routin is null: " + (_coroutine == null));
        float timer = time;
        while (timer > 0)
        {
            print("Routin is null: " + _coroutine == null);
            _text.text = Mathf.CeilToInt(timer).ToString();
            timer -= Time.deltaTime;
            yield return null;
        }
        StopAnimation();
    }
}
