using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PausaForEditMode : MonoBehaviour
{
    [SerializeField] private int _scoreToPause;
    [Space]
    [SerializeField] private SpawnSystem _spawnSystem;
    [Space]
    [SerializeField] private EnterInEditMode player1;
    [SerializeField] private EnterInEditMode player2;
    [Space]
    [SerializeField] private GameObject _toEditButtonP1;
    [SerializeField] private GameObject _toEditButtonP2;
    [Space]
    [SerializeField] private GameObject _playButtons;
    [SerializeField] private GameObject _EditTime;

    public bool PausaIsActive { get; private set; }

    public UnityEvent OnStartPauseEvent;
    public UnityEvent OnEndPauseEvent;

    private int _lastPauseScore;

    private bool _scoreEnough;
    private bool _noneEnemy;

    private void Update()
    {
        if (PausaIsActive)
        {
            if (_spawnSystem.Enemies.Count > 0)
            {
                foreach (var item in _spawnSystem.Enemies) Destroy(item);

                StartCoroutine(CleanEnemiesOnNextFrame(_spawnSystem));
                _spawnSystem.Stop();
            }
            return;
        }

        _noneEnemy = false;
        if (_spawnSystem.Enemies.Count == 0) _noneEnemy = true;

        if (_scoreEnough && _noneEnemy) ActivePausa();
    }

    public void SetScoreToPause(int score)
    {
        _scoreToPause = score;
    }

    public void ActivePausa()
    {
        print("ActivePause");
        player1.ToEdit();
        if(OneOrTwoPlayer.SingleTone.IsOnePlayer == false) player2.ToEdit();
        _toEditButtonP1.SetActive(false);
        _toEditButtonP2.SetActive(false);

        _playButtons.SetActive(true);
        _EditTime.SetActive(true);
        StartCoroutine(StaticCoroutine.Wait(3, () => { _EditTime.SetActive(false); }));

        PausaIsActive = true;

        OnStartPauseEvent.Invoke();
    }

    public void ExitFromPause()
    {
        _spawnSystem.StartGame();
        _playButtons.SetActive(false);

        _toEditButtonP1.SetActive(true);
        _toEditButtonP2.SetActive(true);
        player1.Respawn();
        if (OneOrTwoPlayer.SingleTone.IsOnePlayer == false) player2.Respawn();

        PausaIsActive = false;
        _scoreEnough = false;

        OnEndPauseEvent.Invoke();
    }

    public void OnScoreUpdate(int score, int money)
    {
        if (score - _lastPauseScore < _scoreToPause) return;

        print("Score is enough");
        _lastPauseScore = score;
        _spawnSystem.Stop();
        _scoreEnough = true;
    }

    public IEnumerator CleanEnemiesOnNextFrame(SpawnSystem spawnSystem)
    {
        yield return new WaitForEndOfFrame();
        spawnSystem.CleanEnemiesArrByNull();
    }
}
