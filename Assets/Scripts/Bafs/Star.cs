using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public void UpdateCharactiristic(GameObject tank)
    {
        tank.GetComponentInChildren<TankUpdates>().UpdateLeavle();
    }
}
