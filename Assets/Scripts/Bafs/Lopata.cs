using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lopata : MonoBehaviour
{
    [SerializeField] private float _timeWork = 10;
    [SerializeField] private GameObject _prefabWall;
    [SerializeField] private GameObject _prefabBetonWall;
    [Space]
    [SerializeField] private Transform _transformOfBase;
    [SerializeField] private Transform[] _curentWalls;
    [Space]
    [SerializeField] private Transform[] _bloksPosition;
    [SerializeField] private Vector2[] _bloksPositions;

    private Coroutine _wait;

    public void StartWork()
    {
        if (_wait != null) StopCoroutine(_wait);

        ChangeOnThis(_prefabBetonWall);
        _wait = StartCoroutine(StaticCoroutine.Wait(_timeWork, () => { ChangeOnThis(_prefabWall); }));
    }

    [ContextMenu("TransformToVector")]
    public void TransformToVector()
    {
        _bloksPositions = new Vector2[_bloksPosition.Length];
        for (int i = 0; i < _bloksPosition.Length; i++)
        {
            _bloksPositions[i] = (Vector2)_bloksPosition[i].position;
        }
    }

    private void ChangeOnThis(GameObject nextWall)
    {
        for (int i = 0; i < _bloksPositions.Length; i++)
        {
            if (_curentWalls[i] != null) Destroy(_curentWalls[i].gameObject);

            GameObject newWall = Instantiate(nextWall, _transformOfBase);
            newWall.transform.position = _bloksPositions[i];
            _curentWalls[i] = newWall.transform;
        }
    }
}
