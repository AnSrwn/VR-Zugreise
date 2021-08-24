using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDude : MonoBehaviour
{
    public DialogueManager dialogueManager;

    public AudioSource audioSource;
    public GameObject attendant;
    public AudioSource attendantAudioSource;

    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        DialogueManager.messageActionAvailable += onMessageAction;
    }
    public void initiateConversation()
    {
        List<Speaker> npcSpeakers = new List<Speaker>();
        npcSpeakers.Add(new Speaker("Dude", gameObject, audioSource));
        npcSpeakers.Add(new Speaker("Attendant", attendant, attendantAudioSource));

        dialogueManager.initiateConversation("IntroConversation", npcSpeakers);
        animator.SetBool("isTalking", true);
    }

    private void onMessageAction()
    {
        if (dialogueManager.activeConversation == "IntroConversation" 
            && dialogueManager.messageAction != null 
            && dialogueManager.actionSpeaker == "Dude")
        {
            if (dialogueManager.messageAction == "isIdle")
            {
                print("Dude isIdle");
                animator.SetBool("isIdle", true);
                animator.SetBool("isTalking", false);
                animator.SetBool("isAngry", false);
                animator.SetBool("isDisapproval", false);
            }

            if (dialogueManager.messageAction == "isTalking")
            {
                print("Dude isTalking");
                animator.SetBool("isIdle", false);
                animator.SetBool("isTalking", true);
                animator.SetBool("isAngry", false);
                animator.SetBool("isDisapproval", false);
            }

            if (dialogueManager.messageAction == "isAngry")
            {
                print("Dude isAngry");
                animator.SetBool("isIdle", false);
                animator.SetBool("isTalking", false);
                animator.SetBool("isAngry", true);
                animator.SetBool("isDisapproval", false);
            }

            if (dialogueManager.messageAction == "isDisapproval")
            {
                print("Dude isDisapproval");
                animator.SetBool("isIdle", false);
                animator.SetBool("isTalking", false);
                animator.SetBool("isAngry", false);
                animator.SetBool("isDisapproval", true);
            }

            if (dialogueManager.messageAction == "attendantStartWalkingToServeCoffee")
            {
                print("attendantStartWalkingToServeCoffee");
                attendant.GetComponent<Attendant>().startWalkingToServeCoffee();
            }
        }
    }
}
