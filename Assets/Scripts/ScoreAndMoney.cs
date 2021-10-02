using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAndMoney : MonoBehaviour
{
    [SerializeField] private int _startMoney = 2000;
    [Space]
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _moneyText;
    [SerializeField] private ButtonScaleAnimation _animation;

    public int Score { get; private set; }
    public int Money { get; private set; }

    public static ScoreAndMoney SingleTone;

    private void Start()
    {
        if (SingleTone != null) Debug.LogError("SingleTone Exeption");
        SingleTone = this;
        Money = _startMoney;
        UpdateInfo();
    }

    public bool TryToBye(int money)
    {
        if (Money < money)
        {
            _animation.PlayAnimation();
            return false;
        }

        Money -= money;
        UpdateInfo();
        return true;
    }

    public void OnBornNewEnemy(EnemyAI enemyAI)
    {
        enemyAI.GetComponentInChildren<Health>().DieEvent.AddListener(OnEnemyDie);
    }

    public void OnEnemyDie(EnemyAI enemyAI)
    {
        Score += 100;
        Money += 100;
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        _scoreText.text = Score.ToString();
        _moneyText.text = Money.ToString();
    }
}
