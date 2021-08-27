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
        initiateConversation();
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
        animator.SetBool(DudeAnimationCondition.IS_TALKING, true);
    }

    private void onMessageAction()
    {
        if (dialogueManager.activeConversation == "IntroConversation" 
            && dialogueManager.messageAction != null 
            && dialogueManager.actionSpeaker == "Dude")
        {
            switch (dialogueManager.messageAction)
            {
                case DudeAnimationCondition.IS_IDLE:
                    print("Dude " + DudeAnimationCondition.IS_IDLE);
                    animator.SetBool(DudeAnimationCondition.IS_IDLE, true);
                    animator.SetBool(DudeAnimationCondition.IS_TALKING, false);
                    animator.SetBool(DudeAnimationCondition.IS_ANGRY, false);
                    animator.SetBool(DudeAnimationCondition.IS_DISAPPROVAL, false);
                    break;

                case DudeAnimationCondition.IS_TALKING:
                    print("Dude " + DudeAnimationCondition.IS_TALKING);
                    animator.SetBool(DudeAnimationCondition.IS_IDLE, false);
                    animator.SetBool(DudeAnimationCondition.IS_TALKING, true);
                    animator.SetBool(DudeAnimationCondition.IS_ANGRY, false);
                    animator.SetBool(DudeAnimationCondition.IS_DISAPPROVAL, false);
                    break;
                case DudeAnimationCondition.IS_ANGRY:
                    print("Dude " + DudeAnimationCondition.IS_ANGRY);
                    animator.SetBool(DudeAnimationCondition.IS_IDLE, false);
                    animator.SetBool(DudeAnimationCondition.IS_TALKING, false);
                    animator.SetBool(DudeAnimationCondition.IS_ANGRY, true);
                    animator.SetBool(DudeAnimationCondition.IS_DISAPPROVAL, false);
                    break;
                case DudeAnimationCondition.IS_DISAPPROVAL:
                    print("Dude " + DudeAnimationCondition.IS_DISAPPROVAL);
                    animator.SetBool(DudeAnimationCondition.IS_IDLE, false);
                    animator.SetBool(DudeAnimationCondition.IS_TALKING, false);
                    animator.SetBool(DudeAnimationCondition.IS_ANGRY, false);
                    animator.SetBool(DudeAnimationCondition.IS_DISAPPROVAL, true);
                    break;
                case "attendantStartWalkingToServeCoffee":
                    print("attendantStartWalkingToServeCoffee");
                    attendant.GetComponent<Attendant>().startWalkingToServeCoffee();
                    break;
            }
        }
    }
}

public static class DudeAnimationCondition
{
    public const string
        IS_IDLE = "isIdle",
        IS_TALKING = "isTalking",
        IS_ANGRY = "isAngry",
        IS_DISAPPROVAL = "isDisapproval";
}
