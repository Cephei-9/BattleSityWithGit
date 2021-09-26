using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    //Dot работает через жопу надо написать свой правельный дот
    [SerializeField] private Transform _transformOfCollider;
    [SerializeField] private GameObject _gameObjToDie;

    private bool _isInjury;

    private void Start()
    {
        _gameObjToDie.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
    }

    public void TakeBullet(Vector2 bulletPos)
    {
        if (_isInjury) { Destroy(_gameObjToDie); return; }

        Vector2 toBullet = bulletPos - (Vector2)transform.position;
        CalculateDirection(toBullet);
    }

    private void CalculateDirection(Vector2 toBullet)
    {
        float DotFromRight = TrueDot.Dot(toBullet, Vector2.right);
        float DotFromUp = TrueDot.Dot(toBullet, Vector2.up);

        Vector2 directionDamage = Vector2.up * Mathf.Sign(DotFromUp);
        if (Mathf.Abs(DotFromRight) > Mathf.Abs(DotFromUp))
        {
            directionDamage = Vector2.right * Mathf.Sign(DotFromRight);
        }

        ChangeCollider(directionDamage);
    }

    private void ChangeCollider(Vector2 damageDirection)
    {
        _transformOfCollider.position += -(Vector3)damageDirection * 0.25f;

        Vector2 subtractioScale = new Vector2(0.5f, 0);
        if (damageDirection.x == 0) subtractioScale = new Vector2(0, 0.5f);

        _transformOfCollider.localScale -= (Vector3)subtractioScale;
        _isInjury = true;
    }
}
