using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirdObj : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<Gird>().SetUnit(Vector2Int.RoundToInt(transform.position), this);
    }
}
