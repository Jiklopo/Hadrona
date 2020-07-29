using System;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private Activateable activateable;
    [SerializeField] private bool deactivateOnLeave;

    private void OnTriggerEnter2D(Collider2D other)
    {
        activateable.Activate();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(deactivateOnLeave)
            activateable.Deactivate();
    }
}
