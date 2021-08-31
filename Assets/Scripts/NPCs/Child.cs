using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public SceneManager sceneManager;
    public SpaceManager spaceManager;
    public AudioSource audioSource;
    public GameObject childPositionThirdScene;
    private Animator animator;

    private bool introSpaceDialoguePlayed = false;
    private bool toysPlayed = false;
    private bool rocketPlayed = false;
    private bool cowPlayed = false;
    private bool aliensPlayed = false;
    private bool astronautPlayed = false;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (sceneManager.sceneNumber == 0)
        {
            SittingHappyAnimation();
        }
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
                GameObject player = other.gameObject;
                if (sceneManager.sceneNumber == 2)
                {
                    if (!introSpaceDialoguePlayed)
                    {
                        introSpaceDialoguePlayed = true;
                        Vector3 delta = new Vector3(player.transform.position.x - transform.position.x, 0.0f, player.transform.position.z - transform.position.z);
                        transform.rotation = Quaternion.LookRotation(delta);
                        initiateConversation("IntroSpace");
                    }
                }
                break;
        }
    }

    public void initiateConversation(string conversation)
    {
        List<Speaker> npcSpeakers = new List<Speaker>();
        npcSpeakers.Add(new Speaker("Child", gameObject, audioSource));

        dialogueManager.initiateConversation(conversation, npcSpeakers);
    }

    private IEnumerator initiateConversationWithDelay(float delayTime, string conversation)
    {
        yield return new WaitForSeconds(delayTime);
        initiateConversation(conversation);
    }

    private void onMessageAction()
    {
        // if ((dialogueManager.activeConversation == "IntroSpace"
        //     || dialogueManager.activeConversation == "Rocket"
        //     || dialogueManager.activeConversation == "Cow"
        //     ||  dialogueManager.activeConversation == "Aliens"
        //     || dialogueManager.activeConversation == "Astronaut")
        //    && dialogueManager.messageAction != null
        //    && dialogueManager.actionSpeaker == "Child")
        if (dialogueManager.messageAction != null
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
                    Talking1Animation();
                    break;
                case ChildAnimationCondition.IS_ANGRY:
                    print("Child " + ChildAnimationCondition.IS_ANGRY);
                    AngryAnimation();
                    break;
                case ChildAnimationCondition.IS_SITTING_HAPPY:
                    print("Child " + ChildAnimationCondition.IS_SITTING_HAPPY);
                    SittingHappyAnimation();
                    break;
                case ChildAnimationCondition.IS_VICTORY_JUMP:
                    print("Child " + ChildAnimationCondition.IS_VICTORY_JUMP);
                    VictoryJumpAnimation();
                    break;
                case ChildAnimationCondition.IS_POINTING_FORWARD:
                    print("Child " + ChildAnimationCondition.IS_POINTING_FORWARD);
                    animator.SetBool(ChildAnimationCondition.IS_IDLE, false);
                    animator.SetBool(ChildAnimationCondition.IS_TALKING1, false);
                    animator.SetBool(ChildAnimationCondition.IS_ANGRY, false);
                    animator.SetBool(ChildAnimationCondition.IS_SITTING_HAPPY, false);
                    animator.SetBool(ChildAnimationCondition.IS_VICTORY_JUMP, false);
                    animator.SetBool(ChildAnimationCondition.IS_POINTING_FORWARD, true);
                    animator.SetBool(ChildAnimationCondition.IS_DANCING, false);
                    break;
                case ChildAnimationCondition.IS_DANCING:
                    print("Child " + ChildAnimationCondition.IS_DANCING);
                    DancingAnimation();
                    break;
                case "endIntroSpace":
                    if (!toysPlayed)
                    {
                        toysPlayed = true;
                        IdleAnimation();

                        if (sceneManager.toyMissing)
                        {
                            StartCoroutine(initiateConversationWithDelay(1.0f, "ToysGone"));
                        } else{
                            StartCoroutine(initiateConversationWithDelay(1.0f, "ToysPresent"));
                        }
                        
                    }
                    break;
                case "endToys":
                    if (!rocketPlayed)
                    {
                        rocketPlayed = true;
                    
                        IdleAnimation();
                        spaceManager.StartRocket();
                        StartCoroutine(initiateConversationWithDelay(7.0f, "Rocket"));
                    }
                    break;
                case "positiveRocketAnswer":
                    VictoryJumpAnimation();
                    break;
                case "negativeRocketAnswer":
                    AngryAnimation();
                    spaceManager.DestroyRocket();
                    break;
                case "endRocketDialogue":
                    if (!cowPlayed)
                    {
                        cowPlayed = true;
                        IdleAnimation();
                        spaceManager.StartCow();
                        StartCoroutine(initiateConversationWithDelay(7.0f, "Cow"));
                    }
                    break;
                case "positiveCowAnswer":
                    Talking1Animation();
                    break;
                case "negativeCowAnswer":
                    AngryAnimation();
                    spaceManager.DestroyCow();
                    break;
                case "endCowDialogue":
                    if (!aliensPlayed)
                    {
                        aliensPlayed = true;
                        IdleAnimation();
                        spaceManager.StartAliens();
                        StartCoroutine(initiateConversationWithDelay(7.0f, "Aliens"));
                    }
                    break;
                case "positiveAliensAnswer":
                    DancingAnimation();
                    break;
                case "negativeAliensAnswer":
                    AngryAnimation();
                    spaceManager.DestroyAliens();
                    break;
                case "endAliensDialogue":
                    if (!astronautPlayed)
                    {
                        astronautPlayed = true;
                        IdleAnimation();
                        spaceManager.StartAstronaut();
                        StartCoroutine(initiateConversationWithDelay(7.0f, "Astronaut"));
                    }
                    break;
                case "positiveAstronautAnswer":
                    Talking1Animation();
                    break;
                case "negativeAstronautAnswer":
                    AngryAnimation();
                    spaceManager.DestroyAstronaut();
                    break;
                case "endAstronautDialogue":
                    IdleAnimation();
                    break;
            }
        }
    }

    private void DancingAnimation()
    {
        animator.SetBool(ChildAnimationCondition.IS_IDLE, false);
        animator.SetBool(ChildAnimationCondition.IS_TALKING1, false);
        animator.SetBool(ChildAnimationCondition.IS_ANGRY, false);
        animator.SetBool(ChildAnimationCondition.IS_SITTING_HAPPY, false);
        animator.SetBool(ChildAnimationCondition.IS_VICTORY_JUMP, false);
        animator.SetBool(ChildAnimationCondition.IS_POINTING_FORWARD, false);
        animator.SetBool(ChildAnimationCondition.IS_DANCING, true);
    }

    private void Talking1Animation()
    {
        animator.SetBool(ChildAnimationCondition.IS_IDLE, false);
        animator.SetBool(ChildAnimationCondition.IS_TALKING1, true);
        animator.SetBool(ChildAnimationCondition.IS_ANGRY, false);
        animator.SetBool(ChildAnimationCondition.IS_SITTING_HAPPY, false);
        animator.SetBool(ChildAnimationCondition.IS_VICTORY_JUMP, false);
        animator.SetBool(ChildAnimationCondition.IS_POINTING_FORWARD, false);
        animator.SetBool(ChildAnimationCondition.IS_DANCING, false);
    }

    private void AngryAnimation()
    {
        animator.SetBool(ChildAnimationCondition.IS_IDLE, false);
        animator.SetBool(ChildAnimationCondition.IS_TALKING1, false);
        animator.SetBool(ChildAnimationCondition.IS_ANGRY, true);
        animator.SetBool(ChildAnimationCondition.IS_SITTING_HAPPY, false);
        animator.SetBool(ChildAnimationCondition.IS_VICTORY_JUMP, false);
        animator.SetBool(ChildAnimationCondition.IS_POINTING_FORWARD, false);
        animator.SetBool(ChildAnimationCondition.IS_DANCING, false);
    }

    private void VictoryJumpAnimation()
    {
        animator.SetBool(ChildAnimationCondition.IS_IDLE, false);
        animator.SetBool(ChildAnimationCondition.IS_TALKING1, false);
        animator.SetBool(ChildAnimationCondition.IS_ANGRY, false);
        animator.SetBool(ChildAnimationCondition.IS_SITTING_HAPPY, false);
        animator.SetBool(ChildAnimationCondition.IS_VICTORY_JUMP, true);
        animator.SetBool(ChildAnimationCondition.IS_POINTING_FORWARD, false);
        animator.SetBool(ChildAnimationCondition.IS_DANCING, false);
    }

    private void SittingHappyAnimation()
    {
        animator.SetBool(ChildAnimationCondition.IS_IDLE, false);
        animator.SetBool(ChildAnimationCondition.IS_TALKING1, false);
        animator.SetBool(ChildAnimationCondition.IS_ANGRY, false);
        animator.SetBool(ChildAnimationCondition.IS_SITTING_HAPPY, true);
        animator.SetBool(ChildAnimationCondition.IS_VICTORY_JUMP, false);
        animator.SetBool(ChildAnimationCondition.IS_POINTING_FORWARD, false);
        animator.SetBool(ChildAnimationCondition.IS_DANCING, false);
    }

    private void IdleAnimation()
    {
        animator.SetBool(ChildAnimationCondition.IS_IDLE, true);
        animator.SetBool(ChildAnimationCondition.IS_TALKING1, false);
        animator.SetBool(ChildAnimationCondition.IS_ANGRY, false);
        animator.SetBool(ChildAnimationCondition.IS_SITTING_HAPPY, false);
        animator.SetBool(ChildAnimationCondition.IS_VICTORY_JUMP, false);
        animator.SetBool(ChildAnimationCondition.IS_POINTING_FORWARD, false);
        animator.SetBool(ChildAnimationCondition.IS_DANCING, false);
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
        IS_POINTING_FORWARD = "isPointingForward",
        IS_DANCING = "isDancing";
}
