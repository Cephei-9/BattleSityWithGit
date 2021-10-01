using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForbiddenCell : MonoBehaviour
{
    [SerializeField] private Transform[] _forbiddenCell;
    public Vector2Int[] _positions;

    public static ForbiddenCell SingleTone { get; private set; }

    private void Start()
    {
        //_positions = new Vector2Int[_forbiddenCell.Length];
        //for (int i = 0; i < _forbiddenCell.Length; i++)
        //{
        //    _positions[i] = Vector2Int.RoundToInt(_forbiddenCell[i].position);
        //}
        _positions = GetChaildCell();

        SingleTone = this;
    }

    public bool CheckOnForbidden(Vector2Int position)
    {
        foreach (var item in _positions)
        {
            if (position == item) return true;
        }
        print("Forbidden false");
        return false;
    }

    private Vector2Int[] GetChaildCell()
    {
        List<Vector2Int> positions = new List<Vector2Int>();
        foreach (var squre in transform.GetComponentsInChildren<Transform>())
        {
            foreach (var cell in squre.GetComponentsInChildren<Transform>())
            {
                positions.Add(Vector2Int.RoundToInt(cell.position));
            }
        }
        return positions.ToArray();
    }
}
