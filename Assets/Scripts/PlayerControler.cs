using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private MoveButton[] Buttons;
    [SerializeField] private KeyCode _shootButton;
    [Space]
    [SerializeField] private TankMove _tank;
    [SerializeField] private TankGun _tankGun;

    public MoveButton nowButton;

    public bool IsMove { get; private set; }

    private void Update()
    {
        ChekPressed();
        CheckPressNewButton();

        if (Input.GetKeyDown(_shootButton)) _tankGun.Shoot();
    }

    private void ChekPressed()
    {
        if (Input.GetKeyUp(nowButton.KeyCode))
        {
            nowButton = new MoveButton();
            foreach (var button in Buttons)
            {
                if (Input.GetKey(button.KeyCode))
                {
                    nowButton = button;
                }
            }
            OnChangedButton();
        }
    }

    private void CheckPressNewButton()
    {
        foreach (var button in Buttons)
        {
            if (Input.GetKeyDown(button.KeyCode))
            {
                nowButton = button;
                OnChangedButton();
            }
        }
    }

    private void OnChangedButton()
    {
        if(nowButton.Direction == Vector2.zero)
        {
            _tank.Stop();
            return;
        }

        _tank.SetDirection(nowButton.Direction);
    }
}

[System.Serializable]
public class MoveButton
{
    public KeyCode KeyCode;
    public Vector2 Direction;
}

