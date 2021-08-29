using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public SceneManager sceneManager;
    public AudioSource audioSource;

    public GameObject child;
    public AudioSource childAudioSource;
    public GameObject window;
    private Animator animator;
    private bool conductorChildConversationPlayed = false;
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
                if (sceneManager.sceneNumber == 0 && !conductorChildConversationPlayed)
                {
                    initiateConversation("ConductorChildConversation");
                    conductorChildConversationPlayed = true;
                }

                if (sceneManager.sceneNumber == 1 && !askedForTicket)
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

        if (conversation == "ConductorChildConversation")
        {
            npcSpeakers.Add(new Speaker("Child", child, childAudioSource));
        }

        dialogueManager.initiateConversation(conversation, npcSpeakers);
        // animator.SetBool(DudeAnimationCondition.IS_TALKING, true);
    }

    private void onMessageAction()
    {
    }
}
