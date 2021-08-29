using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public AudioSource audioSource;
    public GameObject window;
    private Animator animator;
    private bool askedForTicket = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        DialogueManager.messageActionAvailable += onMessageAction;
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                if (!askedForTicket)
                {
                    initiateConversation("AskForTicket");
                    window.SetActive(true);
                    askedForTicket = true;
                }
                break;

            case "Bird":
                break;

            case "Ticket":
                break;
        }
    }
    
    public void initiateConversation(string conversation)
    {
        List<Speaker> npcSpeakers = new List<Speaker>();
        npcSpeakers.Add(new Speaker("Conductor", gameObject, audioSource));

        dialogueManager.initiateConversation(conversation, npcSpeakers);
        // animator.SetBool(DudeAnimationCondition.IS_TALKING, true);
    }

    private void onMessageAction()
    {
    }
}
