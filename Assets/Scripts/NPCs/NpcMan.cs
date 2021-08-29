using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMan : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public AudioSource audioSource;
    public GameObject npcWoman;
    public AudioSource npcWomanAudioSource;
    private Animator animator;
    private bool firstDialoguePlayed = false;
    private bool secondDialoguePlayed = false;

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
                if (!firstDialoguePlayed)
                {
                    initiateConversation("NpcConversation01");
                    firstDialoguePlayed = true;
                } else if (!secondDialoguePlayed)
                {
                    initiateConversation("NpcConversation02");
                    secondDialoguePlayed = true;
                }
                break;
        }
    }

    public void initiateConversation(string conversation)
    {
        List<Speaker> npcSpeakers = new List<Speaker>();
        npcSpeakers.Add(new Speaker("NpcMan", gameObject, audioSource));
        npcSpeakers.Add(new Speaker("NpcWoman", npcWoman, npcWomanAudioSource));


        dialogueManager.initiateConversation(conversation, npcSpeakers);
        // animator.SetBool(DudeAnimationCondition.IS_TALKING, true);
    }

    private void onMessageAction()
    {
        if ((dialogueManager.activeConversation == "NpcConversation01"
           || dialogueManager.activeConversation == "NpcConversation02")
           && dialogueManager.messageAction != null
           && dialogueManager.actionSpeaker == "NpcMan")
        {
            switch (dialogueManager.messageAction)
            {
                case NpcManAnimationCondition.IS_IDLE:
                    print("NpcMan " + NpcManAnimationCondition.IS_IDLE);
                    animator.SetBool(NpcManAnimationCondition.IS_IDLE, true);
                    animator.SetBool(NpcManAnimationCondition.IS_TALKING, false);
                    animator.SetBool(NpcManAnimationCondition.IS_ANGRY, false);
                    animator.SetBool(NpcManAnimationCondition.IS_DISAPPROVAL, false);
                    break;

                case NpcManAnimationCondition.IS_TALKING:
                    print("NpcMan " + NpcManAnimationCondition.IS_TALKING);
                    animator.SetBool(NpcManAnimationCondition.IS_IDLE, false);
                    animator.SetBool(NpcManAnimationCondition.IS_TALKING, true);
                    animator.SetBool(NpcManAnimationCondition.IS_ANGRY, false);
                    animator.SetBool(NpcManAnimationCondition.IS_DISAPPROVAL, false);
                    break;
                case NpcManAnimationCondition.IS_ANGRY:
                    print("NpcMan " + NpcManAnimationCondition.IS_ANGRY);
                    animator.SetBool(NpcManAnimationCondition.IS_IDLE, false);
                    animator.SetBool(NpcManAnimationCondition.IS_TALKING, false);
                    animator.SetBool(NpcManAnimationCondition.IS_ANGRY, true);
                    animator.SetBool(NpcManAnimationCondition.IS_DISAPPROVAL, false);
                    break;
                case NpcManAnimationCondition.IS_DISAPPROVAL:
                    print("NpcMan " + NpcManAnimationCondition.IS_DISAPPROVAL);
                    animator.SetBool(NpcManAnimationCondition.IS_IDLE, false);
                    animator.SetBool(NpcManAnimationCondition.IS_TALKING, false);
                    animator.SetBool(NpcManAnimationCondition.IS_ANGRY, false);
                    animator.SetBool(NpcManAnimationCondition.IS_DISAPPROVAL, true);
                    break;
            }
        }
    }  
}

public static class NpcManAnimationCondition
{
    public const string
        IS_IDLE = "isIdle",
        IS_TALKING = "isTalking",
        IS_ANGRY = "isAngry",
        IS_DISAPPROVAL = "isDisapproval";
}
