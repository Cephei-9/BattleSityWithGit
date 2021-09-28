using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Immortality : MonoBehaviour
{
    [SerializeField] private float _timeWork = 5;

    public void GiveImmortality(GameObject tank)
    {
        tank.GetComponent<Health>().TakeImmortality(_timeWork);
    }
}
