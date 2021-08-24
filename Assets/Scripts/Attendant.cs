using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attendant : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public GameObject destinationServeCoffee;
    public GameObject destinationBackOfTrain;
    private NavMeshAgent navMeshAgent;
    private GameObject currentDestination = null;
    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
        animator.SetBool("isIdle", true);

        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        DialogueManager.messageActionAvailable += onMessageAction;
    }

    private void Update() {
        if (currentDestination != null && Vector3.Distance(transform.position, currentDestination.transform.position) < 1.0f)
        {
            transform.rotation = currentDestination.transform.rotation;

            navMeshAgent.ResetPath();
            currentDestination = null;

            animator.SetBool("isIdle", true);
            animator.SetBool("isWalking", false);
            animator.SetBool("isWalkingWithCoffee", false);
        }
    }

    public void startWalkingToServeCoffee()
    {
        currentDestination = destinationServeCoffee;
        navMeshAgent.SetDestination(currentDestination.transform.position);
        animator.SetBool("isWalkingWithCoffee", true);
        animator.SetBool("isIdle", false);
    }

    private void onMessageAction()
    {
        if (dialogueManager.activeConversation == "IntroConversation" 
            && dialogueManager.messageAction != null
            && dialogueManager.actionSpeaker == "Attendant")
        {
            if (dialogueManager.messageAction == "isIdle")
            {
                print("Attendant isIdle");
                animator.SetBool("isIdle", true);
                animator.SetBool("isTalking1", false);
                animator.SetBool("isTalking2", false);
                animator.SetBool("isGiving", false);
                animator.SetBool("isWalkingWithCoffee", false);
                animator.SetBool("isDropping", false);
            }

            if (dialogueManager.messageAction == "isTalking1")
            {
                print("Attendant isTalking1");
                animator.SetBool("isIdle", false);
                animator.SetBool("isTalking1", true);
                animator.SetBool("isTalking2", false);
                animator.SetBool("isGiving", false);
                animator.SetBool("isWalkingWithCoffee", false);
                animator.SetBool("isDropping", false);
            }

            if (dialogueManager.messageAction == "isTalking2")
            {
                print("Attendant isTalking2");
                animator.SetBool("isIdle", false);
                animator.SetBool("isTalking1", false);
                animator.SetBool("isTalking2", true);
                animator.SetBool("isGiving", false);
                animator.SetBool("isWalkingWithCoffee", false);
                animator.SetBool("isDropping", false);
            }

            if (dialogueManager.messageAction == "isGiving")
            {
                print("Attendant isGiving");
                animator.SetBool("isIdle", false);
                animator.SetBool("isTalking1", false);
                animator.SetBool("isTalking2", false);
                animator.SetBool("isGiving", true);
                animator.SetBool("isWalkingWithCoffee", false);
                animator.SetBool("isDropping", false);
            }

            if (dialogueManager.messageAction == "isWalkingWithFood")
            {
                print("Attendant isWalkingWithFood");
                animator.SetBool("isIdle", false);
                animator.SetBool("isTalking1", false);
                animator.SetBool("isTalking2", false);
                animator.SetBool("isGiving", false);
                animator.SetBool("isWalkingWithCoffee", true);
                animator.SetBool("isDropping", false);
            }

            if (dialogueManager.messageAction == "isDropping")
            {
                print("Attendant isDropping");
                animator.SetBool("isTalking1", false);
                animator.SetBool("isTalking2", false);
                animator.SetBool("isGiving", false);
                animator.SetBool("isWalkingWithCoffee", false);
                animator.SetBool("isIdle", false);
                animator.SetBool("isDropping", true);

                StartCoroutine(IdleAnimation(2.0f));
            }

            if (dialogueManager.messageAction == "goToBackOfTrain")
            {
                print("Attendant goToBackOfTrain");

                currentDestination = destinationBackOfTrain;
                navMeshAgent.SetDestination(currentDestination.transform.position);

                animator.SetBool("isTalking1", false);
                animator.SetBool("isWalking", true);
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
        animator.SetBool("isWalkingWithCoffee", false);
        animator.SetBool("isDropping", false);
    }
}
