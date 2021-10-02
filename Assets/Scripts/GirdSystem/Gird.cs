using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gird : MonoBehaviour
{
    [SerializeField] private SpawnSystem _spawnSystem;

    public Dictionary<Vector2Int, GirdObj> GirdUnit { get; private set; } = new Dictionary<Vector2Int, GirdObj>();

    private void Awake()
    {
        for (int x = -14; x < 14; x++)
        {
            for (int y = -14; y < 14; y++)
            {
                GirdUnit.Add(new Vector2Int(x, y), null);
            }
        }
    }

    public void SetUnit(Vector2Int position, GirdObj girdObj)
    {
        GirdUnit[position] = girdObj;
    }

    public GirdObj GetUnit(Vector2Int position)
    {
        return GirdUnit[position];
    }

    public bool CheckIsFreeUnit(Vector2Int position)
    {
        if (GirdUnit[position] || CheckTankOnUnit(position)) return false;
        return true;
    }

    private bool CheckTankOnUnit(Vector2 position)
    {
        foreach (var enemyAI in _spawnSystem.Enemies)
        {
            Vector2 tankPos = enemyAI.GetComponentInChildren<TankMove>().transform.position;
            Vector2 toTankFromPosition = tankPos - (position + Vector2.right * 0.5f + Vector2.up * 0.5f);
            Vector2 normolizeDirection = TrueDot.NormalizeAngleForVector(toTankFromPosition);
            Vector2 projectOnNormal = Vector3.Project(toTankFromPosition, normolizeDirection);

            if (projectOnNormal.magnitude < 1.5f) return true;
        }
        return false;
    }
}
