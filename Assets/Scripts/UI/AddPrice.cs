using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddPrice : MonoBehaviour
{
    [SerializeField] private string _string = "AAA";
    [SerializeField] private float _distance = 100;
    [SerializeField] private Vector3 _direction =  Vector3.down;
    [SerializeField] private Transform _origin;
    [SerializeField] private Transform _textTransform;
    [SerializeField] private Text _text;

    private void Start()
    {
        _textTransform.position = _origin.position + _direction * _distance;
        _text.text = _string;
    }
}
