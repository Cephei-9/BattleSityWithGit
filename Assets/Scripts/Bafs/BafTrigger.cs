using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BafTrigger : MonoBehaviour
{
    public UnityEvent<GameObject> OnTakingByPlayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            print("Player on trigger");
            OnTakingByPlayer.Invoke(other.gameObject);
            Destroy(gameObject);
        }
    }
}
