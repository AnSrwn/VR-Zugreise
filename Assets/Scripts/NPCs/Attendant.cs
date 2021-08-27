using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attendant : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public GameObject player;
    public AudioSource audioSource;

    public GameObject destinationServeCoffee;
    public GameObject destinationBackOfTrain;
    public GameObject coffeCup;
    public SkyManager skyManager;
    public SceneManager sceneManager;
    private NavMeshAgent navMeshAgent;
    private GameObject currentDestination = null;
    private Animator animator;

    private bool isIdleDialog = false;

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

                isIdleDialog = true;
            }

            navMeshAgent.ResetPath();
            currentDestination = null;
        }

 
        if (isIdleDialog && Vector3.Distance(transform.position, player.transform.position) < 1.5f)
        {
            List<Speaker> npcSpeakers = new List<Speaker>();
            npcSpeakers.Add(new Speaker("Attendant", gameObject, audioSource));

            dialogueManager.initiateConversation("AttendantIdleConversation", npcSpeakers);
            isIdleDialog = false;
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
                case AttendantAnimationCondition.POSITIVE_REACTION:
                    skyManager.SetSky(SkyManager.Type.Purple, 5);
                    sceneManager.friendlyToAttendant = true;
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
                case AttendantAnimationCondition.NEGATIVE_REACTION:
                    skyManager.SetSky(SkyManager.Type.Red, 5);
                    sceneManager.friendlyToAttendant = false;
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
        IS_WALKING = "isWalking",
        POSITIVE_REACTION = "positiveReaction",
        NEGATIVE_REACTION = "negativeReaction";
}
