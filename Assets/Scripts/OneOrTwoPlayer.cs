using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneOrTwoPlayer : MonoBehaviour
{
    [SerializeField] private ConcretPlayer[] _concretPlayers;
    [SerializeField] private ChalengeSystem _chalengeSystem;

    public bool IsOnePlayer = true;
    public static OneOrTwoPlayer SingleTone { get; private set; }

    private void Start()
    {
        SingleTone = this;
    }

    public void SetOnOnePlayer()
    {
        foreach (var item in _concretPlayers) item.IsOnePlayer = true;
        _chalengeSystem.SetParametrsOnOnePlayer(true);
    }

    public void SetOnTwoPlayer()
    {
        foreach (var item in _concretPlayers) item.IsOnePlayer = false;
        _chalengeSystem.SetParametrsOnOnePlayer(false);
        IsOnePlayer = false;
    }
}
