using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    //Dot работает через жопу надо написать свой правельный дот
    [SerializeField] private bool _isBeton = false;
    [Space]
    [SerializeField] private Transform _spritesTransform;
    [SerializeField] private GameObject _gameObjToDie;

    private bool _isInjury;

    private void Start()
    {
        _gameObjToDie.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
    }

    public void TakeBullet(BulletCollisionInfo bulletInfo)
    {
        if (_isBeton && bulletInfo.BulletType != BulletType.BetonCrusher) return;
        if (_isInjury) { Destroy(_gameObjToDie); return; }

        Vector2 toBullet = bulletInfo.lastPosition - (Vector2)transform.position;
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

        ChangeSprite(directionDamage);
    }

    private void ChangeSprite(Vector2 damageDirection)
    {
        _spritesTransform.position += -(Vector3)damageDirection * 0.25f;

        Vector2 subtractioScale = new Vector2(0.5f, 0);
        if (damageDirection.x == 0) subtractioScale = new Vector2(0, 0.5f);

        _spritesTransform.localScale -= (Vector3)subtractioScale;
        _isInjury = true;
    }
}
