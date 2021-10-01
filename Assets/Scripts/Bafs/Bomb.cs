using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public bool DestroyAfterBoom = true;

    public void Boom()
    {
        SpawnSystem spawnSystem = FindObjectOfType<SpawnSystem>();
        EnemyAI[] enemies = spawnSystem.Enemies.ToArray();
        foreach (var item in enemies)
        {
            Destroy(item.gameObject);
        }
        StartCoroutine(CleanEnemiesOnNextFrame(spawnSystem));
    }

    public IEnumerator CleanEnemiesOnNextFrame(SpawnSystem spawnSystem)
    {
        //yield return null;
        yield return new WaitForEndOfFrame();
        spawnSystem.CleanEnemiesArrByNull();
    }
}
