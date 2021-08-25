using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attendant : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public GameObject destinationServeCoffee;
    public GameObject destinationBackOfTrain;
    public GameObject coffeCup;
    private NavMeshAgent navMeshAgent;
    private GameObject currentDestination = null;
    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
        animator.SetBool(AttendantAnimationCondition.IS_IDLE_WITH_COFFEE, true);

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

            if (currentDestination == destinationServeCoffee)
            {
                animator.SetBool(AttendantAnimationCondition.IS_IDLE_WITH_COFFEE, true);
                animator.SetBool(AttendantAnimationCondition.IS_WALKING_WITH_COFFEE, false);
            } else if (currentDestination == destinationBackOfTrain)
            {
                animator.SetBool(AttendantAnimationCondition.IS_IDLE, true);
                animator.SetBool(AttendantAnimationCondition.IS_WALKING, false);
            }

            navMeshAgent.ResetPath();
            currentDestination = null;
        }
    }

    public void startWalkingToServeCoffee()
    {
        currentDestination = destinationServeCoffee;
        navMeshAgent.SetDestination(currentDestination.transform.position);
        animator.SetBool(AttendantAnimationCondition.IS_WALKING_WITH_COFFEE, true);
        animator.SetBool(AttendantAnimationCondition.IS_IDLE, false);
        animator.SetBool(AttendantAnimationCondition.IS_IDLE_WITH_COFFEE, false);
    }

    private void onMessageAction()
    {
        if (dialogueManager.activeConversation == "IntroConversation" 
            && dialogueManager.messageAction != null
            && dialogueManager.actionSpeaker == "Attendant")
        {
            switch(dialogueManager.messageAction)
            {
                case AttendantAnimationCondition.IS_IDLE:
                    print("Attendant " + AttendantAnimationCondition.IS_IDLE);
                    animator.SetBool(AttendantAnimationCondition.IS_IDLE, true);
                    animator.SetBool(AttendantAnimationCondition.IS_IDLE_WITH_COFFEE, false);
                    animator.SetBool(AttendantAnimationCondition.IS_TALKING_WITH_COFFEE, false);
                    animator.SetBool(AttendantAnimationCondition.IS_TALKING_1, false);
                    animator.SetBool(AttendantAnimationCondition.IS_TALKING_2, false);
                    animator.SetBool(AttendantAnimationCondition.IS_GIVING, false);
                    animator.SetBool(AttendantAnimationCondition.IS_WALKING_WITH_COFFEE, false);
                    animator.SetBool(AttendantAnimationCondition.IS_DROPPING, false);
                    animator.SetBool(AttendantAnimationCondition.IS_WALKING, false);
                    break;

                case AttendantAnimationCondition.IS_IDLE_WITH_COFFEE:
                    print("Attendant " + AttendantAnimationCondition.IS_IDLE_WITH_COFFEE);
                    animator.SetBool(AttendantAnimationCondition.IS_IDLE, false);
                    animator.SetBool(AttendantAnimationCondition.IS_IDLE_WITH_COFFEE, true);
                    animator.SetBool(AttendantAnimationCondition.IS_TALKING_WITH_COFFEE, false);
                    animator.SetBool(AttendantAnimationCondition.IS_TALKING_1, false);
                    animator.SetBool(AttendantAnimationCondition.IS_TALKING_2, false);
                    animator.SetBool(AttendantAnimationCondition.IS_GIVING, false);
                    animator.SetBool(AttendantAnimationCondition.IS_WALKING_WITH_COFFEE, false);
                    animator.SetBool(AttendantAnimationCondition.IS_DROPPING, false);
                    animator.SetBool(AttendantAnimationCondition.IS_WALKING, false);
                    break;
                case AttendantAnimationCondition.IS_TALKING_1:
                    print("Attendant " + AttendantAnimationCondition.IS_TALKING_1);
                    animator.SetBool(AttendantAnimationCondition.IS_IDLE, false);
                    animator.SetBool(AttendantAnimationCondition.IS_IDLE_WITH_COFFEE, false);
                    animator.SetBool(AttendantAnimationCondition.IS_TALKING_WITH_COFFEE, false);
                    animator.SetBool(AttendantAnimationCondition.IS_TALKING_1, true);
                    animator.SetBool(AttendantAnimationCondition.IS_TALKING_2, false);
                    animator.SetBool(AttendantAnimationCondition.IS_GIVING, false);
                    animator.SetBool(AttendantAnimationCondition.IS_WALKING_WITH_COFFEE, false);
                    animator.SetBool(AttendantAnimationCondition.IS_DROPPING, false);
                    animator.SetBool(AttendantAnimationCondition.IS_WALKING, false);
                    break;
                case AttendantAnimationCondition.IS_TALKING_2:
                    print("Attendant " + AttendantAnimationCondition.IS_TALKING_2);
                    animator.SetBool(AttendantAnimationCondition.IS_IDLE, false);
                    animator.SetBool(AttendantAnimationCondition.IS_IDLE_WITH_COFFEE, false);
                    animator.SetBool(AttendantAnimationCondition.IS_TALKING_WITH_COFFEE, false);
                    animator.SetBool(AttendantAnimationCondition.IS_TALKING_1, false);
                    animator.SetBool(AttendantAnimationCondition.IS_TALKING_2, true);
                    animator.SetBool(AttendantAnimationCondition.IS_GIVING, false);
                    animator.SetBool(AttendantAnimationCondition.IS_WALKING_WITH_COFFEE, false);
                    animator.SetBool(AttendantAnimationCondition.IS_DROPPING, false);
                    animator.SetBool(AttendantAnimationCondition.IS_WALKING, false);
                    break;
                case AttendantAnimationCondition.IS_TALKING_WITH_COFFEE:
                    print("Attendant " + AttendantAnimationCondition.IS_TALKING_WITH_COFFEE);
                    animator.SetBool(AttendantAnimationCondition.IS_IDLE, false);
                    animator.SetBool(AttendantAnimationCondition.IS_IDLE_WITH_COFFEE, false);
                    animator.SetBool(AttendantAnimationCondition.IS_TALKING_WITH_COFFEE, true);
                    animator.SetBool(AttendantAnimationCondition.IS_TALKING_1, false);
                    animator.SetBool(AttendantAnimationCondition.IS_TALKING_2, false);
                    animator.SetBool(AttendantAnimationCondition.IS_GIVING, false);
                    animator.SetBool(AttendantAnimationCondition.IS_WALKING_WITH_COFFEE, false);
                    animator.SetBool(AttendantAnimationCondition.IS_DROPPING, false);
                    animator.SetBool(AttendantAnimationCondition.IS_WALKING, false);
                    break;
                case AttendantAnimationCondition.IS_GIVING:
                    print("Attendant " + AttendantAnimationCondition.IS_GIVING);
                    animator.SetBool(AttendantAnimationCondition.IS_IDLE, false);
                    animator.SetBool(AttendantAnimationCondition.IS_IDLE_WITH_COFFEE, false);
                    animator.SetBool(AttendantAnimationCondition.IS_TALKING_WITH_COFFEE, true);
                    animator.SetBool(AttendantAnimationCondition.IS_TALKING_1, false);
                    animator.SetBool(AttendantAnimationCondition.IS_TALKING_2, false);
                    animator.SetBool(AttendantAnimationCondition.IS_GIVING, false);
                    animator.SetBool(AttendantAnimationCondition.IS_WALKING_WITH_COFFEE, false);
                    animator.SetBool(AttendantAnimationCondition.IS_DROPPING, false);
                    animator.SetBool(AttendantAnimationCondition.IS_WALKING, false);

                    StartCoroutine(GiveAnimation(3.0f));
                    break;
                case AttendantAnimationCondition.IS_DROPPING:
                    print("Attendant " + AttendantAnimationCondition.IS_DROPPING);
                    animator.SetBool(AttendantAnimationCondition.IS_IDLE, false);
                    animator.SetBool(AttendantAnimationCondition.IS_IDLE_WITH_COFFEE, false);
                    animator.SetBool(AttendantAnimationCondition.IS_TALKING_WITH_COFFEE, false);
                    animator.SetBool(AttendantAnimationCondition.IS_TALKING_1, false);
                    animator.SetBool(AttendantAnimationCondition.IS_TALKING_2, false);
                    animator.SetBool(AttendantAnimationCondition.IS_GIVING, false);
                    animator.SetBool(AttendantAnimationCondition.IS_WALKING_WITH_COFFEE, false);
                    animator.SetBool(AttendantAnimationCondition.IS_DROPPING, true);
                    animator.SetBool(AttendantAnimationCondition.IS_WALKING, false);

                    StartCoroutine(IdleAnimation(2.0f));
                    break;
                case "goToBackOfTrain":
                    print("Attendant goToBackOfTrain");
                    currentDestination = destinationBackOfTrain;
                    navMeshAgent.SetDestination(currentDestination.transform.position);

                    animator.SetBool(AttendantAnimationCondition.IS_IDLE, false);
                    animator.SetBool(AttendantAnimationCondition.IS_IDLE_WITH_COFFEE, false);
                    animator.SetBool(AttendantAnimationCondition.IS_TALKING_WITH_COFFEE, false);
                    animator.SetBool(AttendantAnimationCondition.IS_TALKING_1, false);
                    animator.SetBool(AttendantAnimationCondition.IS_TALKING_2, false);
                    animator.SetBool(AttendantAnimationCondition.IS_GIVING, false);
                    animator.SetBool(AttendantAnimationCondition.IS_WALKING_WITH_COFFEE, false);
                    animator.SetBool(AttendantAnimationCondition.IS_DROPPING, false);
                    animator.SetBool(AttendantAnimationCondition.IS_WALKING, true);
                    break;
            }
        }
    }

    private IEnumerator IdleAnimation(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        print("Attendant " + AttendantAnimationCondition.IS_IDLE);
        animator.SetBool(AttendantAnimationCondition.IS_IDLE, true);
        animator.SetBool(AttendantAnimationCondition.IS_IDLE_WITH_COFFEE, false);
        animator.SetBool(AttendantAnimationCondition.IS_TALKING_WITH_COFFEE, false);
        animator.SetBool(AttendantAnimationCondition.IS_TALKING_1, false);
        animator.SetBool(AttendantAnimationCondition.IS_TALKING_2, false);
        animator.SetBool(AttendantAnimationCondition.IS_GIVING, false);
        animator.SetBool(AttendantAnimationCondition.IS_WALKING_WITH_COFFEE, false);
        animator.SetBool(AttendantAnimationCondition.IS_DROPPING, false);
        animator.SetBool(AttendantAnimationCondition.IS_WALKING, false);
    }

    private IEnumerator GiveAnimation(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        print("Attendant " + AttendantAnimationCondition.IS_GIVING);
        animator.SetBool(AttendantAnimationCondition.IS_IDLE, false);
        animator.SetBool(AttendantAnimationCondition.IS_IDLE_WITH_COFFEE, false);
        animator.SetBool(AttendantAnimationCondition.IS_TALKING_WITH_COFFEE, false);
        animator.SetBool(AttendantAnimationCondition.IS_TALKING_1, false);
        animator.SetBool(AttendantAnimationCondition.IS_TALKING_2, false);
        animator.SetBool(AttendantAnimationCondition.IS_GIVING, true);
        animator.SetBool(AttendantAnimationCondition.IS_WALKING_WITH_COFFEE, false);
        animator.SetBool(AttendantAnimationCondition.IS_DROPPING, false);
        animator.SetBool(AttendantAnimationCondition.IS_WALKING, false);
    }

    public void DropCoffee()
    {
        print("drop coffee");
        coffeCup.GetComponent<CoffeeCup>().DropCoffee();
    }

    public void FinishDropping()
    {
        print("Attendant " + AttendantAnimationCondition.IS_IDLE);
        animator.SetBool(AttendantAnimationCondition.IS_IDLE, true);
        animator.SetBool(AttendantAnimationCondition.IS_IDLE_WITH_COFFEE, false);
        animator.SetBool(AttendantAnimationCondition.IS_TALKING_WITH_COFFEE, false);
        animator.SetBool(AttendantAnimationCondition.IS_TALKING_1, false);
        animator.SetBool(AttendantAnimationCondition.IS_TALKING_2, false);
        animator.SetBool(AttendantAnimationCondition.IS_GIVING, false);
        animator.SetBool(AttendantAnimationCondition.IS_WALKING_WITH_COFFEE, false);
        animator.SetBool(AttendantAnimationCondition.IS_DROPPING, false);
        animator.SetBool(AttendantAnimationCondition.IS_WALKING, false);
    }
}

public static class AttendantAnimationCondition
{
    public const string
        IS_IDLE_WITH_COFFEE = "isIdleWithCoffee",
        IS_TALKING_WITH_COFFEE = "isTalkingWithCoffee",
        IS_WALKING_WITH_COFFEE = "isWalkingWithCoffee",
        IS_GIVING = "isGiving",
        IS_DROPPING = "isDropping",
        IS_IDLE = "isIdle",
        IS_TALKING_1 = "isTalking1",
        IS_TALKING_2 = "isTalking2",
        IS_WALKING = "isWalking";   
}
