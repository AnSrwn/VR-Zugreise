using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TicketPosition : MonoBehaviour
{
    public BreakDoor doorScript;

    private bool ticketTaken = false;

    void OnTriggerExit(Collider collider)
    {
        if(!ticketTaken && collider.gameObject.tag == "Ticket")
        {
            ticketTaken = true;
            doorScript.enabled = true;
        }
    }
}
