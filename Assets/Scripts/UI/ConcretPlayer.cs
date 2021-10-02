using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcretPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _selfTank;
    [SerializeField] private GameObject _otherTank;
    [Space]
    [SerializeField] private Immortality _immortality;

    public bool IsOnePlayer = true;

    public void ActiveImmortality()
    {
        if (CheckTankOnField(_selfTank))
        {
            _immortality.GiveImmortality(_selfTank);
            return;
        }
        if (IsOnePlayer) return;

        if (CheckTankOnField(_otherTank)) _immortality.GiveImmortality(_otherTank);
    }

    public void UpdateTank()
    {
        if (CheckTankOnField(_selfTank)) 
        {
            _selfTank.GetComponentInChildren<TankUpdates>().UpdateLeavle();
            return;
        }
        if (IsOnePlayer) return;

        if (CheckTankOnField(_otherTank)) _otherTank.GetComponentInChildren<TankUpdates>().UpdateLeavle();
    }

    private bool CheckTankOnField(GameObject tank)
    {
        if (tank.transform.parent.GetComponent<PlayerRespawn>().PlayerOnField) return true;

        return false;
    }
}
