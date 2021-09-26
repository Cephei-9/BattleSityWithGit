using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionHandler2D : MonoBehaviour
{
    [SerializeField] private GameObject _selfGameObj;

    public UnityEvent<Collision2D, GameObject> OnCollisionEnterEvent;
    public UnityEvent<Collision2D, GameObject> OnCollisionStayEvent;
    public UnityEvent<Collision2D, GameObject> OnCollisionExitEvent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollisionEnterEvent.Invoke(collision, _selfGameObj); 
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollisionStayEvent.Invoke(collision, _selfGameObj);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        OnCollisionExitEvent.Invoke(collision, _selfGameObj);
    }
}
