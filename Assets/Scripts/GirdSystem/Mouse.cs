using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mouse : MonoBehaviour
{
    [Header("GameFieldBoundaries")]
    [SerializeField] private Vector2Int _minPosition;
    [SerializeField] private Vector2Int _maxPosition;
    [Space]
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private Transform pointer;
    [SerializeField] private GameFieldChanger _activeInstrument;
    [Space]
    public UnityEvent OnEnterGameField;
    public UnityEvent OnExitGameField;

    public bool PointerIsInsideField { get; private set; }

    private Vector2Int _lastPosition;

    private void Start()
    {
        Vector2Int newPositionInt = Vector2Int.FloorToInt(GetMousePosition());
        PointerIsInsideField = CheckPointerInsideField(newPositionInt);
        if (PointerIsInsideField)
        {
            OnEnterGameField.Invoke();
            return;
        }
        OnExitGameField.Invoke();
    }

    private void Update()
    {
        Vector2 newPosition = GetMousePosition();
        pointer.position = newPosition;
        Vector2Int newPositionInt = Vector2Int.FloorToInt(newPosition);

        if (CheckPointerInsideField(newPositionInt) == false)
        {
            if (PointerIsInsideField)
            {
                OnExitGameField.Invoke();
                PointerIsInsideField = false;
            }
            return;
        }
        if (PointerIsInsideField == false)
        {
            PointerIsInsideField = true;
            OnEnterGameField.Invoke();
        }
        pointer.position = (Vector3Int)newPositionInt;

        if (_lastPosition == newPositionInt) return;
        _lastPosition = newPositionInt;
        _activeInstrument.OnPositionChange(newPositionInt);
    }

    public void SetInstrument(GameFieldChanger gameField)
    {
        _activeInstrument.enabled = false;
        _activeInstrument = gameField;
        _activeInstrument.enabled = true;
    }

    private Vector2 GetMousePosition()
    {
        Ray ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(-Vector3.forward, Vector3.zero);
        plane.Raycast(ray, out float distance);
        return ray.GetPoint(distance);
    }

    private bool CheckPointerInsideField(Vector2Int newPosition)
    {
        if (newPosition.x >= _minPosition.x && newPosition.y >= _minPosition.y &&
            newPosition.x <= _maxPosition.x && newPosition.y <= _maxPosition.y)
        {
            return true;
        }
        return false;
    }
}
