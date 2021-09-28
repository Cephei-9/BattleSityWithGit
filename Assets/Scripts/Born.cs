using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{
    [SerializeField] private Collider2D _selfCollider;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private TankMove _tankMove;
    [SerializeField] private DistanceChecker _backDistanceChecker;

    private Color oldColor;
    
    private void Start()
    {
        _selfCollider.enabled = false;
        oldColor = _sprite.color;
        _sprite.color = Color.white;
    }

    private void Update()
    {
        if (_tankMove.IsCollision == false && _backDistanceChecker.CheckDistance() > 1)
        {
            _selfCollider.enabled = true;
            _sprite.color = oldColor;
            Destroy(gameObject);
        }
    }
}
