using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrice : MonoBehaviour
{
    [SerializeField] private int _price = 100;
    public int GetPrice { get => _price; }
}
