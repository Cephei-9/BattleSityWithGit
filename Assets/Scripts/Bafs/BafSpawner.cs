using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BafSpawner : MonoBehaviour
{
    [SerializeField] private float _chance = 7;
    [Header("GameFieldBoundaries")]
    [SerializeField] private Vector2Int _minPosition;
    [SerializeField] private Vector2Int _maxPosition;
    [Space]
    [SerializeField] private GameObject[] _bafs;
    [Space]
    [SerializeField] private Gird _gird;
    [SerializeField] private ForbiddenCell _forbiddenCell;
    [Space]
    [SerializeField] private BafTankAnimation animation;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) SpawnBaf(new EnemyAI());
    }

    public void OnNewEnemy(EnemyAI enemyAI)
    {
        if (Random.value * 100 > _chance) return;

        SpriteRenderer tankRendarer = enemyAI.transform.GetChild(0).GetComponentInChildren<SpriteRenderer>();
        animation.PlayAnimation(tankRendarer);
        enemyAI.GetComponentInChildren<Health>().DieEvent.AddListener(SpawnBaf);
    }

    public void SpawnBaf(EnemyAI enemyAI)
    {
        Vector3 positionToSpawn = (Vector2)GetRandomPosition() + Vector2.up + Vector2.right;
        GameObject newBaf = Instantiate(_bafs[Random.Range(0, _bafs.Length)], positionToSpawn, Quaternion.identity);

        SpriteRenderer bafRenderer = newBaf.GetComponentInChildren<SpriteRenderer>();
        System.Action action = () => { animation.PlayAnimation(bafRenderer, Color.clear); };
            StartCoroutine(StaticCoroutine.Wait(15, action));
        StartCoroutine(StaticCoroutine.Wait(20, () => { Destroy(newBaf); }));
    }

    public Vector2Int GetRandomPosition()
    {
        for (int i = 0; i < 50; i++)
        {
            Vector2Int PositionToSpawn;

            Vector2Int random = Vector2Int.zero;
            random.x = Random.Range(_minPosition.x + 1, _maxPosition.x);
            random.y = Random.Range(_minPosition.y + 1, _maxPosition.y);
            bool allGood = true;
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    Vector2Int additionalCell = random + Vector2Int.right * x + Vector2Int.up * y;

                    if (_gird.CheckIsFreeUnit(additionalCell) == false) print("Gird false" + random);

                    if (_forbiddenCell.CheckOnForbidden(additionalCell) ||
                        _gird.CheckIsFreeUnit(additionalCell) == false) { allGood = false;  break; }
                }
                if (allGood == false) break;
            }
            if (allGood)
            {
                print("Position: " + random);
                PositionToSpawn = random;
                return PositionToSpawn;
            } 
        }
        Vector2Int randomPosition = Vector2Int.zero;
        randomPosition.x = Random.Range(-_minPosition.x + 1, _maxPosition.x);
        randomPosition.y = Random.Range(-_minPosition.y + 1, _maxPosition.y);
        return randomPosition;
    }
}
