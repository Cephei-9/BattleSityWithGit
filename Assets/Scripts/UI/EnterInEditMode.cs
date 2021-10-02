using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnterInEditMode : MonoBehaviour
{
    [SerializeField] private float _timeToRespawn = 5;
    [SerializeField] private EnterInEditMode _otherEditMode;
    [SerializeField] private PlayerRespawn _selfRespawn;
    [Space]
    [SerializeField] private GameObject _ToEditButton;
    [SerializeField] private GameObject _respawnButton;
    [Space]
    [SerializeField] private RespawnTimeAnimation animation;

    public bool PlayerOnField = true;
    public bool CanRespawn = true;

    public float TimerToRespawn;

    private void Update()
    {
        if (PlayerOnField) return;

        TimerToRespawn += Time.deltaTime;
        if (_respawnButton.activeSelf == false && TimerToRespawn >= _timeToRespawn)
        {
            _respawnButton.SetActive(true);
        }
    }

    public void ToEdit()
    {
        SetActiveButton(false, true);
        _selfRespawn.HidePlayer();

        PlayerOnField = false;
        TimerToRespawn = _timeToRespawn;
    }

    public void Respawn()
    {
        _selfRespawn.Spawn();
        SetActiveButton(true, false);
        PlayerOnField = true;

        if (TimerToRespawn < _timeToRespawn) _otherEditMode.CancelOportunityRespawn();
    }

    public void OnPlayerDie()
    {
        _selfRespawn.HidePlayer();
        TimerToRespawn = 0;
        SetActiveButton(false, false);
        PlayerOnField = false;
        animation.PlayAnimation(_timeToRespawn);

        if (_otherEditMode.PlayerOnField || _otherEditMode.TimerToRespawn > _timeToRespawn) return;

        DoubleChanceRespawn();
        _otherEditMode.DoubleChanceRespawn();
    }

    public void DoubleChanceRespawn()
    {
        CanRespawn = true;
        _respawnButton.SetActive(true);
        animation.StopAnimation();
    }

    public void CancelOportunityRespawn()
    {
        CanRespawn = false;
        _respawnButton.SetActive(false);
        animation.PlayAnimation(_timeToRespawn - TimerToRespawn);
    }

    private void SetActiveButton(bool toEditActive, bool respawnActive)
    {
        _ToEditButton.SetActive(toEditActive);
        _respawnButton.SetActive(respawnActive);
    }
}
