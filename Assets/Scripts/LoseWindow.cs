using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseWindow : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private GameObject LoseObject;

    public void Lose()
    {
        _text.text = ScoreAndMoney.SingleTone.Score.ToString();
        StartCoroutine(StaticCoroutine.Wait(2, () => { LoseObject.SetActive(true); }));
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
