using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public AudioSource audioSource;
    public GameObject player;
    public GameObject window;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (Vector3.Distance(transform.position, player.transform.position) < 2.0f)
        {
            initiateConversation("AskForTicket");
            window.SetActive(true);
        }
    }

    private void OnEnable()
    {
        DialogueManager.messageActionAvailable += onMessageAction;
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
