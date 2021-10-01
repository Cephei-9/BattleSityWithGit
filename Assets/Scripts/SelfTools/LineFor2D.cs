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
        newLine.localScale = new Vector3(newLine.localScale.x, 5, newLine.localScale.z);
    }

    public void CreateLine(Vector2 position, Vector2 direction, Color color, bool withOriginalLength)
    {
        Transform newLine = Instantiate(_linePrefab, position, Quaternion.LookRotation(Vector3.forward, direction), transform);
        newLine.GetComponentInChildren<SpriteRenderer>().color = color;
        if (withOriginalLength) newLine.localScale = new Vector3(newLine.localScale.x, direction.magnitude, newLine.localScale.z); 
    }
}
