using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public void Boom()
    {
        EnemyAI[] enemies = FindObjectOfType<SpawnSystem>().Enemies.ToArray();
        foreach (var item in enemies)
        {
            Destroy(item.gameObject);
        }
    }
}
