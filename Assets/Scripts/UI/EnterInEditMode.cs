using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterInEditMode : MonoBehaviour
{
    [SerializeField] private float _timeToRespawn = 5;
    [SerializeField] private EnterInEditMode _otherEditMode;
    [SerializeField] private PlayerRespawn _selfRespawn;
    [Space]
    [SerializeField] private GameObject _ToEditButton;
    [SerializeField] private GameObject _respawnButton;

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
        print("ToEdit");
        SetActiveButton(false, true);
        _selfRespawn.HidePlayer();

        PlayerOnField = false;
        TimerToRespawn = _timeToRespawn;
    }

    public void Respawn()
    {
        print("Respawn");
        _selfRespawn.Spawn();
        SetActiveButton(true, false);
        PlayerOnField = true;

        if (TimerToRespawn < _timeToRespawn) _otherEditMode.CancelOportunityRespawn();
    }

    public void OnPlayerDie()
    {
        print("OnPlayerDie");
        _selfRespawn.HidePlayer();
        TimerToRespawn = 0;
        SetActiveButton(false, false);
        PlayerOnField = false;

        if (_otherEditMode.PlayerOnField || _otherEditMode.TimerToRespawn > _timeToRespawn) return;

        DoubleChanceRespawn();
        _otherEditMode.DoubleChanceRespawn();
    }

    public void DoubleChanceRespawn()
    {
        CanRespawn = true;
        _respawnButton.SetActive(true);
    }

    public void CancelOportunityRespawn()
    {
        CanRespawn = false;
        _respawnButton.SetActive(false);
    }

    private void SetActiveButton(bool toEditActive, bool respawnActive)
    {
        _ToEditButton.SetActive(toEditActive);
        _respawnButton.SetActive(respawnActive);
    }
}
