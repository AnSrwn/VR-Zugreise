using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public SceneManager sceneManager;
    public AudioSource audioSource;
    public GameObject childPositionThirdScene;
    private Animator animator;

    private bool introSpaceDialoguePlayed = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        DialogueManager.messageActionAvailable += onMessageAction;

        if (sceneManager.sceneNumber == 0)
        {
            SittingHappyAnimation();
        } else if (sceneManager.sceneNumber == 2)
        {
            transform.position = childPositionThirdScene.transform.position;
            transform.rotation = childPositionThirdScene.transform.rotation;
            IdleAnimation();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                if (sceneManager.sceneNumber == 2)
                {
                    if (!introSpaceDialoguePlayed)
                    {
                        initiateConversation("IntroSpace");
                        introSpaceDialoguePlayed = true;
                    }
                }
                break;
        }
    }

    public void initiateConversation(string conversation)
    {
        List<Speaker> npcSpeakers = new List<Speaker>();
        npcSpeakers.Add(new Speaker("NpcMan", gameObject, audioSource));

        dialogueManager.initiateConversation(conversation, npcSpeakers);
    }

    private void onMessageAction()
    {
        if ((dialogueManager.activeConversation == "IntroSpace")
           && dialogueManager.messageAction != null
           && dialogueManager.actionSpeaker == "Child")
        {
            switch (dialogueManager.messageAction)
            {
                case ChildAnimationCondition.IS_IDLE:
                    print("Child " + ChildAnimationCondition.IS_IDLE);
                    IdleAnimation();
                    break;
                case ChildAnimationCondition.IS_TALKING1:
                    print("Child " + ChildAnimationCondition.IS_TALKING1);
                    animator.SetBool(ChildAnimationCondition.IS_IDLE, false);
                    animator.SetBool(ChildAnimationCondition.IS_TALKING1, true);
                    animator.SetBool(ChildAnimationCondition.IS_ANGRY, false);
                    animator.SetBool(ChildAnimationCondition.IS_SITTING_HAPPY, false);
                    animator.SetBool(ChildAnimationCondition.IS_VICTORY_JUMP, false);
                    animator.SetBool(ChildAnimationCondition.IS_POINTING_FORWARD, false);
                    break;
                case ChildAnimationCondition.IS_ANGRY:
                    print("Child " + ChildAnimationCondition.IS_ANGRY);
                    animator.SetBool(ChildAnimationCondition.IS_IDLE, false);
                    animator.SetBool(ChildAnimationCondition.IS_TALKING1, false);
                    animator.SetBool(ChildAnimationCondition.IS_ANGRY, true);
                    animator.SetBool(ChildAnimationCondition.IS_SITTING_HAPPY, false);
                    animator.SetBool(ChildAnimationCondition.IS_VICTORY_JUMP, false);
                    animator.SetBool(ChildAnimationCondition.IS_POINTING_FORWARD, false);
                    break;
                case ChildAnimationCondition.IS_SITTING_HAPPY:
                    print("Child " + ChildAnimationCondition.IS_SITTING_HAPPY);
                    SittingHappyAnimation();
                    break;
                case ChildAnimationCondition.IS_VICTORY_JUMP:
                    print("Child " + ChildAnimationCondition.IS_VICTORY_JUMP);
                    animator.SetBool(ChildAnimationCondition.IS_IDLE, false);
                    animator.SetBool(ChildAnimationCondition.IS_TALKING1, false);
                    animator.SetBool(ChildAnimationCondition.IS_ANGRY, false);
                    animator.SetBool(ChildAnimationCondition.IS_SITTING_HAPPY, false);
                    animator.SetBool(ChildAnimationCondition.IS_VICTORY_JUMP, true);
                    animator.SetBool(ChildAnimationCondition.IS_POINTING_FORWARD, false);
                    break;
                case ChildAnimationCondition.IS_POINTING_FORWARD:
                    print("Child " + ChildAnimationCondition.IS_POINTING_FORWARD);
                    animator.SetBool(ChildAnimationCondition.IS_IDLE, false);
                    animator.SetBool(ChildAnimationCondition.IS_TALKING1, false);
                    animator.SetBool(ChildAnimationCondition.IS_ANGRY, false);
                    animator.SetBool(ChildAnimationCondition.IS_SITTING_HAPPY, false);
                    animator.SetBool(ChildAnimationCondition.IS_VICTORY_JUMP, false);
                    animator.SetBool(ChildAnimationCondition.IS_POINTING_FORWARD, true);
                    break;
            }
        }
    }

    private void SittingHappyAnimation()
    {
        animator.SetBool(ChildAnimationCondition.IS_IDLE, false);
        animator.SetBool(ChildAnimationCondition.IS_TALKING1, false);
        animator.SetBool(ChildAnimationCondition.IS_ANGRY, false);
        animator.SetBool(ChildAnimationCondition.IS_SITTING_HAPPY, true);
        animator.SetBool(ChildAnimationCondition.IS_VICTORY_JUMP, false);
        animator.SetBool(ChildAnimationCondition.IS_POINTING_FORWARD, false);
    }

    private void IdleAnimation()
    {
        animator.SetBool(ChildAnimationCondition.IS_IDLE, true);
        animator.SetBool(ChildAnimationCondition.IS_TALKING1, false);
        animator.SetBool(ChildAnimationCondition.IS_ANGRY, false);
        animator.SetBool(ChildAnimationCondition.IS_SITTING_HAPPY, false);
        animator.SetBool(ChildAnimationCondition.IS_VICTORY_JUMP, false);
        animator.SetBool(ChildAnimationCondition.IS_POINTING_FORWARD, false);
    }
}

public static class ChildAnimationCondition
{
    public const string
        IS_IDLE = "isIdle",
        IS_TALKING1 = "isTalking1",
        IS_ANGRY = "isAngry",
        IS_SITTING_HAPPY = "isSittingHappy",
        IS_VICTORY_JUMP = "isVictoryJump",
        IS_POINTING_FORWARD = "isPointingForward";
}
