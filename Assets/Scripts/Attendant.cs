using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attendant : MonoBehaviour
{
    public DialogueManager dialogueManager;

    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
        animator.SetBool("isIdle", true);
    }

    private void OnEnable()
    {
        DialogueManager.messageActionAvailable += onMessageAction;
    }

    private void onMessageAction()
    {
        if (dialogueManager.activeConversation == "IntroConversation" 
            && dialogueManager.messageAction != null
            && dialogueManager.actionSpeaker == "Attendant")
        {
            if (dialogueManager.messageAction == "isIdle")
            {
                print("isIdle");
                animator.SetBool("isIdle", true);
                animator.SetBool("isTalking1", false);
                animator.SetBool("isTalking2", false);
                animator.SetBool("isGiving", false);
                animator.SetBool("isWalkingWithFood", false);
                animator.SetBool("isDropping", false);
            }

            if (dialogueManager.messageAction == "isTalking1")
            {
                print("isTalking1");
                animator.SetBool("isIdle", false);
                animator.SetBool("isTalking1", true);
                animator.SetBool("isTalking2", false);
                animator.SetBool("isGiving", false);
                animator.SetBool("isWalkingWithFood", false);
                animator.SetBool("isDropping", false);
            }

            if (dialogueManager.messageAction == "isTalking2")
            {
                print("isTalking2");
                animator.SetBool("isIdle", false);
                animator.SetBool("isTalking1", false);
                animator.SetBool("isTalking2", true);
                animator.SetBool("isGiving", false);
                animator.SetBool("isWalkingWithFood", false);
                animator.SetBool("isDropping", false);
            }

            if (dialogueManager.messageAction == "isGiving")
            {
                print("isGiving");
                animator.SetBool("isIdle", false);
                animator.SetBool("isTalking1", false);
                animator.SetBool("isTalking2", false);
                animator.SetBool("isGiving", true);
                animator.SetBool("isWalkingWithFood", false);
                animator.SetBool("isDropping", false);
            }

            if (dialogueManager.messageAction == "isWalkingWithFood")
            {
                print("isWalkingWithFood");
                animator.SetBool("isIdle", false);
                animator.SetBool("isTalking1", false);
                animator.SetBool("isTalking2", false);
                animator.SetBool("isGiving", false);
                animator.SetBool("isWalkingWithFood", true);
                animator.SetBool("isDropping", false);
            }

            if (dialogueManager.messageAction == "isDropping")
            {
                print("isDropping");
                animator.SetBool("isIdle", false);
                animator.SetBool("isTalking1", false);
                animator.SetBool("isTalking2", false);
                animator.SetBool("isGiving", false);
                animator.SetBool("isWalkingWithFood", false);
                animator.SetBool("isDropping", true);

                StartCoroutine(IdleAnimation(2.0f));
            }
        }
    }

    private IEnumerator IdleAnimation(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        animator.SetBool("isIdle", true);
        animator.SetBool("isTalking1", false);
        animator.SetBool("isTalking2", false);
        animator.SetBool("isGiving", false);
        animator.SetBool("isWalkingWithFood", false);
        animator.SetBool("isDropping", false);
    }
}
