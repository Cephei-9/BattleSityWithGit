using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject _pause;
    [SerializeField] private GameObject _mainMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _mainMenu.activeSelf == false) ActivePause();
    }

    public void ActivePause()
    {
        Time.timeScale = 0;
        _pause.SetActive(true);
    }

    public void Continue()
    {
        Time.timeScale = 1;
        _pause.SetActive(false);
    }
    
    public void EnterToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
