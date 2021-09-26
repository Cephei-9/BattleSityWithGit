using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFor2D : MonoBehaviour
{
    [SerializeField] private Transform _linePrefab;
    public static LineFor2D SingleTone { get; private set; }

    private void Start()
    {
        if (SingleTone != null) Debug.LogError("SingleTone eror");
        SingleTone = this;
    }

    public void CreateLine(Vector2 position, Vector2 direction, Color color)
    {
        Transform newLine = Instantiate(_linePrefab, position, Quaternion.LookRotation(Vector3.forward, direction), transform);
        newLine.GetComponentInChildren<SpriteRenderer>().color = color;
    }
}
