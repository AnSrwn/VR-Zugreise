using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcWoman : MonoBehaviour
{
    public DialogueManager dialogueManager;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        DialogueManager.messageActionAvailable += onMessageAction;
    }

    private void onMessageAction()
    {
        if ((dialogueManager.activeConversation == "NpcConversation01"
            || dialogueManager.activeConversation == "NpcConversation02")
            && dialogueManager.messageAction != null
            && dialogueManager.actionSpeaker == "NpcWoman")
        {
            switch (dialogueManager.messageAction)
            {
                case NpcWomanAnimationCondition.IS_IDLE:
                    print("NpcWoman " + NpcWomanAnimationCondition.IS_IDLE);
                    animator.SetBool(NpcWomanAnimationCondition.IS_IDLE, true);
                    animator.SetBool(NpcWomanAnimationCondition.IS_TALKING, false);
                    animator.SetBool(NpcWomanAnimationCondition.IS_ANGRY, false);
                    animator.SetBool(NpcWomanAnimationCondition.IS_SAD, false);
                    break;

                case NpcWomanAnimationCondition.IS_TALKING:
                    print("NpcWoman " + NpcWomanAnimationCondition.IS_TALKING);
                    animator.SetBool(NpcWomanAnimationCondition.IS_IDLE, false);
                    animator.SetBool(NpcWomanAnimationCondition.IS_TALKING, true);
                    animator.SetBool(NpcWomanAnimationCondition.IS_ANGRY, false);
                    animator.SetBool(NpcWomanAnimationCondition.IS_SAD, false);
                    break;
                case NpcWomanAnimationCondition.IS_ANGRY:
                    print("NpcWoman " + NpcWomanAnimationCondition.IS_ANGRY);
                    animator.SetBool(NpcWomanAnimationCondition.IS_IDLE, false);
                    animator.SetBool(NpcWomanAnimationCondition.IS_TALKING, false);
                    animator.SetBool(NpcWomanAnimationCondition.IS_ANGRY, true);
                    animator.SetBool(NpcWomanAnimationCondition.IS_SAD, false);
                    break;
                case NpcWomanAnimationCondition.IS_SAD:
                    print("NpcWoman " + NpcWomanAnimationCondition.IS_SAD);
                    animator.SetBool(NpcWomanAnimationCondition.IS_IDLE, false);
                    animator.SetBool(NpcWomanAnimationCondition.IS_TALKING, false);
                    animator.SetBool(NpcWomanAnimationCondition.IS_ANGRY, false);
                    animator.SetBool(NpcWomanAnimationCondition.IS_SAD, true);
                    break;
            }
        }
    }
}

public static class NpcWomanAnimationCondition
{
    public const string
        IS_IDLE = "isIdle",
        IS_TALKING = "isTalking",
        IS_ANGRY = "isAngry",
        IS_SAD = "isSad";
}
